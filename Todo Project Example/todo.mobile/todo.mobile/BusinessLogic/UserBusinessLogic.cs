using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using Newtonsoft.Json;
using System.Linq;
namespace todo.mobile
{
    public class UserBusinessLogic : CoreBusiness
    {
        private string userBase = CoreSettings.Config.WebApi[CoreSettings.UserAPIBase];

        public async Task GetAllUsers()
        {
            Exception ex = null;
            var httpResult = await HttpService.Get<List<User>>($"{userBase}/GetAll");
            if (httpResult.Success)
            {
                var lst = httpResult.Response;
            }
        }

        public async Task<(bool Success, Exception error)> UpdateProfile(Profile user)
        {
            var url = $"{userBase}/AddOrUpdate";
            var hashedPwd = this.EncryptionService.GetHashString(user.Password);
            user.Password = hashedPwd;
            var httpResult = await HttpService.Post<Dictionary<string, int>>(url, user);
            if(httpResult.Success)
            {
                return (true, null);
            }
            else
            {
                return (false, httpResult.Error);
            }
        }
        public async Task<(User user, bool Success, Exception error)> RegisterNewUser(string userName, string password)
        {
            User usr = null;
            var hashedPwd = this.EncryptionService.GetHashString(password);
            var url = $"{userBase}/CreateAccount?username={userName}&password={hashedPwd}";
            var httpResult = await HttpService.GetRaw(url);
            var success = httpResult.Success;
            if (success)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResult.Response);
                var guid = dict.Keys.First();
                var id = dict[guid].ToString();

                usr = new User()
                {
                    CorrelationID = Guid.Parse(guid),
                    Id = int.Parse(id),
                    UTCTickStamp = DateTime.UtcNow.Ticks,
                    UserName = userName,
                };

            }
            else
            {
                httpResult.Error?.ConsoleWrite();
                httpResult.Error?.LogException();
            }
            return (usr, success, httpResult.Error);
        }

        public async Task<bool> Login(string userName, string password)
        {
            var success = false;
            var usr = await this.AccountService.GetAccountStore<User>(userName, password);
            if (usr.Success && usr.Response != null)
            {
                CoreSettings.AppUser = usr.Response;
                if (!CoreSettings.AppUser.TokenIsValid)
                {
                    success = await RefreshToken(userName, password);
                }
                else
                {
                    success = true;
                    this.HttpService.AddTokenHeader(CoreSettings.AppUser.Token.Token);
                }
            }
            else
            {
                success = await AuthenticateUser(userName, password);
            }
            return success;
        }

        private async Task<bool> RefreshToken(string userName, string password)
        {
            
            bool success = false;
            var url = $"{userBase}/Authorize?grant_type=refresh_token&refresh_token={CoreSettings.AppUser.Token.RefreshToken}";
            var httpResult = await HttpService.GetRaw(url);
            success = httpResult.Success;
            if (success)
            {
                CoreSettings.AppUser.Token = JsonConvert.DeserializeObject<CoreAuthentication>(httpResult.Response);
                this.HttpService.AddTokenHeader(CoreSettings.AppUser.Token.Token);
                var acctResult = await this.AccountService.SaveAccountStore<User>(userName, password, CoreSettings.AppUser);
                success = acctResult.Success;
            }
            else{
                success = await AuthenticateUser(userName, password);
            }

            return success;
        }


        private async Task<bool> AuthenticateUser(string userName, string password)
        {
            var hashedPwd = this.EncryptionService.GetHashString(password);
            var url = $"{userBase}/Authorize?grant_type=password&username={userName}&password={hashedPwd}";
            var httpResult = await HttpService.GetRaw(url);
            var success = httpResult.Success;
            if (success)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResult.Response);
                var jsonObject = dict["meta_data"].ToString();
                var srvUser = JsonConvert.DeserializeObject<User>(jsonObject);

                CoreSettings.AppUser = new User()
                {
                    Id = srvUser.Id,
                    IsLocked = srvUser.IsLocked,
                    UserName = srvUser.UserName,
                    CorrelationID = srvUser.CorrelationID,
                    FirstName = srvUser.FirstName,
                    LastName = srvUser.LastName,
                    Token = new CoreAuthentication()
                    {
                        Token = dict["access_token"].ToString(),
                        RefreshToken = dict["refresh_token"].ToString(),
                        ExpiresIn = int.Parse(dict["expires_in"].ToString())
                    }
                };
  
                this.HttpService.AddTokenHeader(CoreSettings.AppUser.Token.Token);
                var acctResult = await this.AccountService.SaveAccountStore<User>(userName, password, CoreSettings.AppUser);
                success = acctResult.Success;
            }

            return success;
        }

    }
}