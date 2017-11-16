using System;
using CommonCore.Websockets;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        public IChatService Chat
        {
            get
            {
                return CoreDependencyService.GetService<IChatService, ChatService>(true);
            }
        }
    }
}
