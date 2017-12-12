using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.CommonCore.Native;
using Xamarin.Forms.CommonCore;
using System;
using CommonCore.Native.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Support.V4.App;

namespace CommonCore.Native.DroidExample
{
    [Activity(Label = "CommonCore.Native.DroidExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            var ft = SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.mainlayout, new HomeFragment(), "homeFragment");
            ft.Commit();
        }
    }
}

