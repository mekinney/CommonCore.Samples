using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;
using Xamarin.Forms.CommonCore;

namespace tabbedReference.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
#if DEBUG
			CoreSettings.CurrentBuid = "dev";
#elif QA
            CoreSettings.CurrentBuid = "qa";
#elif RELEASE
            CoreSettings.CurrentBuid = "prod";
#endif

			global::Xamarin.Forms.Forms.Init();

            InitGlobalLibraries();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }


		private void InitGlobalLibraries()
		{
			CachedImageRenderer.Init();
		}
    }
}
