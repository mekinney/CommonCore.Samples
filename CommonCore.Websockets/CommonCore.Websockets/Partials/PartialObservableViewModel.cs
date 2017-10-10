using System;
using CommonCore.Websockets;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        public IChatService Chat
        {
            get
            {
                return InjectionManager.GetService<IChatService, ChatService>(true);
            }
        }
    }
}
