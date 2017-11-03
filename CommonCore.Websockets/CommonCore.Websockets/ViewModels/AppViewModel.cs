using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class AppViewModel : ObservableViewModel
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

            StartSession = new RelayCommand((obj) => {
                if (!string.IsNullOrEmpty(FriendlyName))
                    Navigation.PushNonAwaited<ChatPage>();
            });

            SendMessage = new RelayCommand(async (obj) => {
                await this.Chat.SendMessageAsync(TextMessage, FriendlyName);
                TextMessage = string.Empty;
            }, () => { return IsConnect && !string.IsNullOrEmpty(TextMessage); }, this);
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if(key== AppSettings.WebSocketStateChanged)
            {
                if(obj!=null)
                {
                    IsConnect = (bool)obj;
                }
            }
            if(key== AppSettings.WebSocketMessageReceived)
            {
                if (obj != null)
                {
                    Messages.Add((Message)obj);
                    MessageViewIndex = Messages.Count - 1;
                }
            }
        }
    }
}
