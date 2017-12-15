using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class AppViewModel : CoreViewModel
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public ICommand SendMessage { get; set; }
        public bool IsConnect { get; set; }
        public ICommand StartSession { get; set; }
        public string TextMessage { get; set; }
        public string FriendlyName { get; set; }
        public int MessageViewIndex { get; set; } = 0;

        public AppViewModel()
        {
            this.Chat.ConnectToServerAsync().ContinueOn();

            StartSession = new CoreCommand((obj) => {
                if (!string.IsNullOrEmpty(FriendlyName))
                    Navigation.PushNonAwaited<ChatPage>();
            });

            SendMessage = new CoreCommand(async (obj) => {
                await this.Chat.SendMessageAsync(TextMessage, FriendlyName);
                TextMessage = string.Empty;
            }, () => { return IsConnect && !string.IsNullOrEmpty(TextMessage); }, this);
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            switch(key){
                case CoreSettings.WebSocketStateChanged:
                    if (obj != null)
                    {
                        IsConnect = (bool)obj;
                    }
                    break;
                case CoreSettings.WebSocketMessageReceived:
                    if (obj != null)
                    {
                        Messages.Add((Message)obj);
                        MessageViewIndex = Messages.Count - 1;
                    }
                    break;
            }

        }
    }
}
