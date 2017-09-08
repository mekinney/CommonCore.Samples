using System;
namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        public IAdInterstitial AdInterstitial
        {
            get { return DependencyService.Get<IAdInterstitial>(); }
        }
    }
}
