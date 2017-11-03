using System;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppViewModel : ObservableViewModel
    {
        public AppViewModel()
        {
        }

        public override void LoadResources(string parameter = null)
        {
            Task.Run(async()=>{


                var acctResult = await this.AccountService.GetAccountStore<AuthenticationToken>("string", "string");
                if (acctResult.Success)
                {
                    CoreSettings.TokenBearer = acctResult.Response;
                    this.HttpService.Client.AddTokenHeader(CoreSettings.TokenBearer.Token);
                }

                //await this.UserLogic.Login(new User(){
                //    UserName="string",
                //    Password="string"
                //});



                await this.UserLogic.GetAllUsers();
                //var result = await this.TodoLogic.AddOrUpdateTodo(new Todo() { 
                //    Description = "Eat Eggs",
                //    CorrelationID = Guid.Parse("dce2c6cf-6d08-4f05-80db-23271ada0ebb")
                //});
                //var x = result;

                //await this.TodoLogic.SyncOfflineData();
            });
           
        }
    }
}
