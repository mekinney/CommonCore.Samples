using System;
using monetizationDemo.Views;
using Xamarin.Forms;

namespace monetizationDemo
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new HomePage());
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
