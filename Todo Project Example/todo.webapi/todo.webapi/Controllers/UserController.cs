using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using todo.webapi.Data;

namespace todo.webapi.Controllers
{
    public class OAuthParams
    {
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
        //public string client_id { get; set; }
        //public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class OAuthResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string meta_data { get; set; }
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;

        public UserController(IUserRepository userRepo, IConfiguration config, IMemoryCache cache)
        {
            _userRepo = userRepo;
            _config = config;
            _cache = cache;
        }

        [HttpGet]
        [Route("api/User/GetAll")]
        public async Task<List<User>> GetAll()
        {
            return await _userRepo.GetAll();
        }

        [HttpGet]
        [Route("api/User/GetById")]
        public async Task<User> GetById(int id)
        {
            return await _userRepo.GetById(id);
        }

        [HttpPost]
        [Route("api/User/AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] User obj)
        {
            var response = await _userRepo.AddOrUpdate(obj);

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);
        }

        [HttpGet]
        [Route("api/User/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userRepo.Delete(id);

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/CreateAccount")]
        public async Task<IActionResult> CreateAccount(OAuthParams oAuth)
        {
            var response = await _userRepo.AddOrUpdate(new Data.User()
            {
                UserName = oAuth.username,
                Password = oAuth.password,
                CorrelationID = Guid.NewGuid().ToString(),
                UTCTickStamp = DateTime.UtcNow.Ticks
            });

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);

        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/Authorize")]
        public async Task<IActionResult> Authorize( OAuthParams oAuth)
        {
            if(oAuth.grant_type=="password"){
                var auth = await GrantAuthorization(oAuth);
                if (auth != null)
                    return Ok(auth);
                else
                    return BadRequest("Could not create token");
            }
            else if(oAuth.grant_type=="refresh_token")
            {
                var auth = RefreshAuthorization(oAuth);
                if (auth != null)
                    return Ok(auth);
                else
                    return BadRequest("Could not create token");
            }
            else
            {
                return BadRequest(new ApplicationException("Invalid authorization request"));
            }

        }

        private async Task<OAuthResponse> GrantAuthorization(OAuthParams oAuth)
        {
            var obj = await _userRepo.GetByLogin(oAuth.username, oAuth.password);
            if (obj != null)
            {
                obj.Password = string.Empty;
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, oAuth.username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                      _config["Tokens:Issuer"],
                      claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: creds);
  
                var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty).Trim();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(3));
                _cache.Set(refreshToken, oAuth.username, cacheEntryOptions);

                return new OAuthResponse()
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires_in = (int)TimeSpan.FromMinutes(30).TotalSeconds,
                    refresh_token = refreshToken,
                    meta_data = JsonConvert.SerializeObject(obj)
                };

            }
            else{
                return null;
            }
        }
        private OAuthResponse RefreshAuthorization(OAuthParams oAuth)
        {
            string userName = null;
            if (_cache.TryGetValue(oAuth.refresh_token, out userName))
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                          _config["Tokens:Issuer"],
                          claims,
                          expires: DateTime.Now.AddMinutes(30),
                          signingCredentials: creds);

                    return new OAuthResponse()
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        expires_in = (int)TimeSpan.FromMinutes(30).TotalSeconds,
                        refresh_token = oAuth.refresh_token,
                    };

                }
            }

            return null;
        }
    }
}
