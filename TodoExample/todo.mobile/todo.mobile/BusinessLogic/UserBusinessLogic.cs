using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using Newtonsoft.Json;

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


        public async Task<bool> Login(string userName, string password)
        {
            var success = false;
            var acct = await this.AccountService.GetAccountStore<AuthenticationToken>(userName, password);
            if(acct.Success && acct.Response!=null)
            {
                CoreSettings.TokenBearer = acct.Response;
                if(!CoreSettings.TokenIsValid)
                {
                    success = await RefreshToken(userName, password);
                }
                else
                {
                    success = true;
                    this.HttpService.Client.AddTokenHeader(CoreSettings.TokenBearer.Token);
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
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResult.Response);
                    CoreSettings.TokenBearer = new AuthenticationToken()
                    {
                        Token = dict["access_token"].ToString(),
                        RefreshToken = dict["refresh_token"].ToString(),
                        ExpiresIn = int.Parse(dict["expires_in"].ToString())
                    };
                    CoreSettings.TokenBearer.UTCExpiration = DateTime.UtcNow.AddSeconds(CoreSettings.TokenBearer.ExpiresIn).Ticks;

                    this.HttpService.Client.AddTokenHeader(CoreSettings.TokenBearer.Token);
                    var acctResult = await this.AccountService.SaveAccountStore<AuthenticationToken>(userName, password, AppSettings.TokenBearer);
                    success = acctResult.Success;
                }
            }
            return success;
        }

        private async Task<bool> RefreshToken(string userName, string password)
        {
            bool success = false;
            var url = $"{UserBase}/Authorize?grant_type=refresh_token&refresh_token={CoreSettings.TokenBearer.RefreshToken}";
            var httpResult = await HttpService.GetRaw(url);
            success = httpResult.Success;
            if (success)
            {
                CoreSettings.TokenBearer = JsonConvert.DeserializeObject<AuthenticationToken>(httpResult.Response);
                this.HttpService.Client.AddTokenHeader(CoreSettings.TokenBearer.Token);
                var acctResult = await this.AccountService.SaveAccountStore<AuthenticationToken>(userName, password, AppSettings.TokenBearer);
                success = acctResult.Success;
            }

            return success;
        }
       
    }
}