using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using Google.MobileAds;
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

            MobileAds.Configure(CoreSettings.Config.Admob.IOSApp);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
