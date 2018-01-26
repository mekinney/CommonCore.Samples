using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

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
            MainPage = new NavigationPage(new LoginPage());
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


        private void InitRenderers()
        {
            CachedImageRenderer.Init();
        }
    }
}
