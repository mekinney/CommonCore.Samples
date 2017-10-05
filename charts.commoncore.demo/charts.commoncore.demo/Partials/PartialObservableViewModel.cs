using System;
using charts.commoncore.demo;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        public RandomUserLogic AppLogic
        {
            get
            {
                return InjectionManager.GetBusinessLayer<RandomUserLogic>();
            }
        }
    }
}
