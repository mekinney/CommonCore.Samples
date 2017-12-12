using System;
using CommonCore.Native.App;

namespace Xamarin.Forms.CommonCore
{
    /// <summary>
    /// This partial allows the application to extended configuration options
    /// </summary>
    public partial class CoreConfiguration
    {

    }

    /// <summary>
    /// This partial allows the base view model to be extended with additional properties
    /// </summary>
    public partial class CoreViewModel
    {
        public SomeBusinessLogic SomeLogic
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<SomeBusinessLogic>();
            }
        }
    }
}
