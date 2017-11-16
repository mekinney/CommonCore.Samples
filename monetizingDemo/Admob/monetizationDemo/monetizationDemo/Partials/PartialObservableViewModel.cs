using System;
namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        public IAdInterstitial AdInterstitial
        {
            get { return DependencyService.Get<IAdInterstitial>(); }
        }
    }
}
