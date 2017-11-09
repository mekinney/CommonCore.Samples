using System;
using todo.mobile;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        public TodoBusinessLogic TodoLogic
        {
            get
            {
                return InjectionManager.GetBusinessLayer<TodoBusinessLogic>();
            }
        }
        public UserBusinessLogic UserLogic
        {
            get
            {
                return InjectionManager.GetBusinessLayer<UserBusinessLogic>();
            }
        }
    }
}
