using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace CommonCore.Websockets
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime MessagDateTime { get; set; }
        public bool IsIncoming => UserId != CoreSettings.InstallationId;
        public string Name { get; set; }
        public string UserId { get; set; } = CoreSettings.InstallationId;
    }

    public class ChatService : IChatService
    {
        readonly ClientWebSocket client;
        readonly CancellationTokenSource cts;
        private string messageText;

        public ChatService()
        {
            client = new ClientWebSocket();
            cts = new CancellationTokenSource();
        }

        public async Task ConnectToServerAsync()
        {
            var uri = new Uri(CoreSettings.Config.WebApi["websocket"]);
            await client.ConnectAsync(uri, cts.Token);

#if __IOS__
            //await client.ConnectAsync(new Uri("ws://localhost:5001"), cts.Token);
#else
            //await client.ConnectAsync(new Uri("ws://10.0.2.2:5000"), cts.Token);
#endif

            UpdateClientState();

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    do
                    {

                        result = await client.ReceiveAsync(message, cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        string serialisedMessae = Encoding.UTF8.GetString(messageBytes);

                        try
                        {
                            var msg = JsonConvert.DeserializeObject<Message>(serialisedMessae);
                            CoreDependencyService.SendViewModelMessage(CoreSettings.WebSocketMessageReceived, msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Invalide message format. {ex.Message}");
                        }

                    } while (!result.EndOfMessage);
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            void UpdateClientState()
            {
                var isConnected = client.State == WebSocketState.Open ? true : false;
                CoreDependencyService.SendViewModelMessage(CoreSettings.WebSocketStateChanged, isConnected);
            }
        }

        public async Task SendMessageAsync(string message, string userName)
        {
            var msg = new Message
            {
                Name = userName,
                MessagDateTime = DateTime.Now,
                Text = message,
                UserId = CoreSettings.InstallationId
            };

            string serialisedMessage = JsonConvert.SerializeObject(msg);

            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
        }
    }
}
