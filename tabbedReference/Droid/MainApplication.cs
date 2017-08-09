using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms.CommonCore;

namespace tabbedReference.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static Context AppContext;


        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

#if DEBUG
			AppSettings.CurrentBuid = "dev";
#elif QA
            AppSettings.CurrentBuid = "qa";
#elif RELEASE
            AppSettings.CurrentBuid = "prod";
#endif

			RegisterActivityLifecycleCallbacks(this);


			InitGlobalLibraries();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

		private void InitGlobalLibraries()
		{
			AppSettings.NavStyle = NavType.Tabbed;
			AppSettings.AppIcon = Resource.Drawable.icon;
			AppContext = this.ApplicationContext;
			LocalNotify.MainType = typeof(MainActivity);
			CachedImageRenderer.Init();

		}
    }
}