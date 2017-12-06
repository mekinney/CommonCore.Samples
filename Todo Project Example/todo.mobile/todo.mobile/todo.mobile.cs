using System;

using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.PlatformConfiguration;

#if __IOS__
using FFImageLoading.Forms.Touch;
#else
using FFImageLoading.Forms.Droid;
#endif

namespace todo.mobile
{
    public class App : Application
    {
        public App()
        {
            InitRenderers();

            //MainPage = new AppMasterDetailPage();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {

            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }

        protected override void OnSleep()
        {
            MainPage.SizeChanged -= AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
            this.SaveViewModelState();
        }

        protected override void OnResume()
        {
            MainPage.SizeChanged += AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
            this.LoadViewModelState();
        }

        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
        {
            CoreSettings.IsConnected = args.IsConnected;
        }

        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }


        private void InitRenderers()
        {
            CachedImageRenderer.Init();
        }
    }
}
