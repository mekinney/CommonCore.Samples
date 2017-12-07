using System;
using todo.mobile;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        
        public TodoBusinessLogic TodoLogic
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<TodoBusinessLogic>();
            }
        }
        public UserBusinessLogic UserLogic
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<UserBusinessLogic>();
            }
        }

        public HubCommunicationLogic HubCommunication
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<HubCommunicationLogic>();
            }
        }

    }
}
