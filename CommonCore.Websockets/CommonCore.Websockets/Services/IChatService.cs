using System;
using System.Threading.Tasks;

namespace CommonCore.Websockets
{
    public interface IChatService
    {
        Task ConnectToServerAsync();
        Task SendMessageAsync(string message, string userName);
    }
}
