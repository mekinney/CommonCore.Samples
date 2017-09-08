#if __IOS__
using Google.MobileAds;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

[assembly: Dependency(typeof(AdInterstitial))]
namespace Xamarin.Forms.CommonCore
{
    public class AdInterstitial : IAdInterstitial
    {
        Interstitial interstitial;

        public AdInterstitial()
        {
            LoadAd();
            interstitial.ScreenDismissed += (s, e) => LoadAd();
        }

        void LoadAd()
        {
            interstitial = new Interstitial(CoreSettings.Config.Admob.IOSInterstitial);
            var request = Request.GetDefaultRequest();
            interstitial.LoadRequest(request);
        }

        public void ShowAd()
        {
            if (interstitial.IsReady)
            {
                var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                interstitial.PresentFromRootViewController(viewController);
            }
        }
    }
}
#endif
