using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms.CommonCore;
using SignalRConnection = Microsoft.AspNetCore.Sockets.Client.HttpConnection;

namespace todo.mobile
{
    public class HubCommunicationLogic : CoreBusiness
    {
        private string userBase = AppSettings.Config.WebApi[AppSettings.HubAPIBase];
        private HubConnection hub;

        public HubCommunicationLogic()
        {
            hub = new HubConnectionBuilder()
                .WithUrl(userBase)
                .WithConsoleLogger()
                .Build();
            
            hub.On<string>("Send", data =>
            {
                CoreDependencyService.SendViewModelMessage<TodoViewModel>(AppSettings.DataUpdated, "data");
            });

        }

        public async Task StartListening()
        {
            await hub.StartAsync();
            await hub.InvokeAsync("RegisterId",AppSettings.AppUser.Id.ToString());
        }

    }
}
