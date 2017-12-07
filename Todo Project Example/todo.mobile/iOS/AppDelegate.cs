using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms.CommonCore;

namespace todo.mobile.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            SetMinimumBackgroundFetchInterval();
            return base.FinishedLaunching(app, options);
        }

        private const double MINIMUM_BACKGROUND_FETCH_INTERVAL = 900;

        private void SetMinimumBackgroundFetchInterval()
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
        }

        public override async void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            
            Console.WriteLine("PerformFetch called...");
            var result = UIBackgroundFetchResult.NoData;

            try
            {
                var logic = CoreDependencyService.GetBusinessLayer<TodoBusinessLogic>();
                var data = await logic.GetAllByCurrentUser();
                result = UIBackgroundFetchResult.NewData;
            }
            catch(Exception ex)
            {
                result = UIBackgroundFetchResult.Failed;
            }
            finally
            {
                completionHandler(result);
            }

        }

    }
}
