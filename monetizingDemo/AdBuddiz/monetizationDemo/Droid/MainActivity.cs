
using AdBuddiz.Xamarin;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.InAppBilling;
using Xamarin.Forms.CommonCore;

namespace monetizationDemo.Droid
{
    [Activity(Label = "monetizationDemo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            AdBuddizHandler.Instance.SetPublisherKey(CoreSettings.Config.AdBuzzie.DroidPublisherKey);
            AdBuddizHandler.Instance.CacheAds(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }
    }
}
