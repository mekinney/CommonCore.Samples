using System;
using charts.commoncore.demo;
using Xamarin.Forms.CommonCore;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        public RandomUserLogic AppLogic
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<RandomUserLogic>();
            }
        }
    }
}
