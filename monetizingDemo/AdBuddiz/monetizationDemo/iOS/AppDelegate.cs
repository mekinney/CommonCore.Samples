using AdBuddiz.Xamarin;
using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;
using Xamarin.Forms.CommonCore;

namespace monetizationDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication application)
        {
            AdBuddizHandler.Instance.SetPublisherKey(CoreSettings.Config.AdBuzzie.IOSPublisherKey);
            AdBuddizHandler.Instance.CacheAds();
        }
    }
}
