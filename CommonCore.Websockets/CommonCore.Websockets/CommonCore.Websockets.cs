using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class App : Application
    {
        public App()
        {
            CoreSettings.CurrentUser.UserId = Guid.NewGuid().ToString();
            MainPage = new NavigationPage(new HomePage());
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
    }
}
