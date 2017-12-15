using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms.CommonCore;
using SignalRConnection = Microsoft.AspNetCore.Sockets.Client.HttpConnection;

namespace todo.mobile
{
    public class HubCommunicationLogic : CoreBusiness
    {
        private string userBase = CoreSettings.Config.WebApi[CoreSettings.HubAPIBase];
        private HubConnection hub;

        public HubCommunicationLogic()
        {
            hub = new HubConnectionBuilder()
                .WithUrl(userBase)
                .WithConsoleLogger()
                .Build();
            
            hub.On<string>("Send", data =>
            {
                CoreDependencyService.SendViewModelMessage<TodoViewModel>(CoreSettings.DataUpdated, "data");
            });

        }

        public async Task StartListening()
        {
            await hub.StartAsync();
            await hub.InvokeAsync("RegisterId",CoreSettings.AppUser.Id.ToString());
        }

    }
}
