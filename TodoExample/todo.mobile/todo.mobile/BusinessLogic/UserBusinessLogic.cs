using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using Newtonsoft.Json;
using System.Linq;
namespace todo.mobile
{
    public class UserBusinessLogic : BusinessBase
    {
        private string UserBase
        {
            get { return AppSettings.Config.WebApi[AppSettings.UserAPIBase]; }
        }

        public async Task GetAllUsers()
        {
            Exception ex = null;
            var httpResult = await HttpService.Get<List<User>>($"{UserBase}/GetAll");
            if (httpResult.Success)
            {
                var lst = httpResult.Response;
            }
        }

        public async Task<bool> RegisterNewUser(string userName, string password)
        {
            var hashedPwd = this.EncryptionService.GetHashString(password);
            var url = $"{UserBase}/CreateAccount?username={userName}&password={hashedPwd}";
            var httpResult = await HttpService.GetRaw(url);
            var success = httpResult.Success;
            if (success)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResult.Response);
                var guid = dict.Keys.First();
                var id = dict[guid].ToString();

                CoreSettings.CurrentUser.UserId = id;
                CoreSettings.CurrentUser.UserName = userName;

                var obj = new User()
                {
                    CorrelationID = Guid.Parse(guid),
                    Id = int.Parse(id),
                    UTCTickStamp = DateTime.UtcNow.Ticks,
                    UserName = userName,
                    Password = hashedPwd
                };

                var sqlResult = await this.SqliteDb.AddOrUpdate<User>(obj);
                success = sqlResult.Success;
            }
            else
            {
                httpResult.Error?.ConsoleWrite();
                httpResult.Error?.LogException();
            }
            return success;
        }

        public async Task<bool> Login(string userName, string password)
        {
            var success = false;
            var usr = await this.AccountService.GetAccountStore<AppUser>(userName, password);
            if (usr.Success && usr.Response != null)
            {
                CoreSettings.CurrentUser = usr.Response;
                if (!CoreSettings.CurrentUser.TokenIsValid)
                {
                    success = await RefreshToken(userName, password);
                }
                else
                {
                    success = true;
                    this.HttpService.Client.AddTokenHeader(CoreSettings.CurrentUser.AuthToken.Token);
                }
            }
            else
            {
                var hashedPwd = this.EncryptionService.GetHashString(password);
                var url = $"{UserBase}/Authorize?grant_type=password&username={userName}&password={hashedPwd}";
                var httpResult = await HttpService.GetRaw(url);
                success = httpResult.Success;
                if (success)
                {
                    CoreSettings.CurrentUser.UserName = userName;
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResult.Response);
                    CoreSettings.CurrentUser.AuthToken = new AuthenticationToken()
                    {
                        Token = dict["access_token"].ToString(),
                        RefreshToken = dict["refresh_token"].ToString(),
                        ExpiresIn = int.Parse(dict["expires_in"].ToString())
                    };
                    CoreSettings.CurrentUser.AuthToken.UTCExpiration = DateTime.UtcNow.AddSeconds(CoreSettings.CurrentUser.AuthToken.ExpiresIn).Ticks;

                    this.HttpService.Client.AddTokenHeader(CoreSettings.CurrentUser.AuthToken.Token);
                    var acctResult = await this.AccountService.SaveAccountStore<AppUser>(userName, password, CoreSettings.CurrentUser);
                    success = acctResult.Success;
                }
            }
            return success;
        }

        private async Task<bool> RefreshToken(string userName, string password)
        {
            bool success = false;
            var url = $"{UserBase}/Authorize?grant_type=refresh_token&refresh_token={CoreSettings.CurrentUser.AuthToken.RefreshToken}";
            var httpResult = await HttpService.GetRaw(url);
            success = httpResult.Success;
            if (success)
            {
                CoreSettings.CurrentUser.AuthToken = JsonConvert.DeserializeObject<AuthenticationToken>(httpResult.Response);
                this.HttpService.Client.AddTokenHeader(CoreSettings.CurrentUser.AuthToken.Token);
                var acctResult = await this.AccountService.SaveAccountStore<AppUser>(userName, password, AppSettings.CurrentUser);
                success = acctResult.Success;
            }

            return success;
        }

    }
}