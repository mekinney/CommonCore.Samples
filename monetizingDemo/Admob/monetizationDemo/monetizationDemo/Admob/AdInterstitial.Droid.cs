#if __ANDROID__
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using App = Android.App;

[assembly: Dependency(typeof(AdInterstitial))]
namespace Xamarin.Forms.CommonCore
{
    public class AdInterstitial : IAdInterstitial
    {
        InterstitialAd interstitialAd;

        public AdInterstitial()
        {
            interstitialAd = new InterstitialAd(App.Application.Context);
            interstitialAd.AdUnitId = CoreSettings.Config.Admob.DroidInterstitial;
            LoadAd();
        }

        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            interstitialAd.LoadAd(requestbuilder.Build());
        }

        public void ShowAd()
        {
            if (interstitialAd.IsLoaded)
                interstitialAd.Show();

            LoadAd();
        }
    }
}
#endif
