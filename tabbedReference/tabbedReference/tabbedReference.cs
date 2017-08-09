using System;

using Xamarin.Forms;

namespace tabbedReference
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new MainTabPage())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.FromHex("#2196f3"),
                BarBackgroundColor = Color.FromHex("#2196f3"),
            };

            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
