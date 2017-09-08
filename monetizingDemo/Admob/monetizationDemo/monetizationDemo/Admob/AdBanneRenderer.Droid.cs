#if __ANDROID__
using System;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.CommonCore;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanneRenderer))]
namespace Xamarin.Forms.CommonCore
{
    public class AdBanneRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var adView = new AdView(Context);

                switch ((Element as AdBanner).Size)
                {
                    case AdBanner.Sizes.Standardbanner:
                        adView.AdSize = AdSize.Banner;
                        break;
                    case AdBanner.Sizes.LargeBanner:
                        adView.AdSize = AdSize.LargeBanner;
                        break;
                    case AdBanner.Sizes.MediumRectangle:
                        adView.AdSize = AdSize.MediumRectangle;
                        break;
                    case AdBanner.Sizes.FullBanner:
                        adView.AdSize = AdSize.FullBanner;
                        break;
                    case AdBanner.Sizes.Leaderboard:
                        adView.AdSize = AdSize.Leaderboard;
                        break;
                    case AdBanner.Sizes.SmartBannerPortrait:
                        adView.AdSize = AdSize.SmartBanner;
                        break;
                    default:
                        adView.AdSize = AdSize.Banner;
                        break;
                }

                // TODO: change this id to your admob id
                adView.AdUnitId = CoreSettings.Config.Admob.DroidBanner;

                var requestbuilder = new AdRequest.Builder();
                adView.LoadAd(requestbuilder.Build());

                SetNativeControl(adView);
            }
        }
    }
}
#endif
