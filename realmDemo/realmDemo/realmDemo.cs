using System;
using realmDemo.Views;
using Xamarin.Forms;

namespace realmDemo
{
	/// <summary>
	/// Read the latest about Realm at:
	/// https://realm.io/docs/xamarin/latest/
	/// </summary>
	public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new PageOne());
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
