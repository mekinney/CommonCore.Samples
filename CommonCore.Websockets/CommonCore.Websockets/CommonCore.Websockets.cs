using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
        }

        protected override void OnSleep()
        {
            MainPage.SizeChanged -= AppScreenSizeChanged;
        }

        protected override void OnResume()
        {
            MainPage.SizeChanged += AppScreenSizeChanged;

        }

        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }
    }
}
