using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteDemo.Views;
using Xamarin.Forms;

namespace SQLiteDemo
{
	public partial class App : Application
	{
        public readonly NavigationPage _navigationPage = new NavigationPage(new PageOne());
		public App ()
		{
		    // subscribe to app wide unhandled exceptions so that we can log them.
		    AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
            InitializeComponent();

			MainPage = _navigationPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	    /// <summary>
	    /// When app-wide unhandled exceptions are hit, this will handle them. Be aware however, that typically
	    /// android will be destroying the process, so there's not a lot you can do on the android side of things,
	    /// but your xamarin code should still be able to work. so if you have a custom err logging manager or 
	    /// something, you can call that here. You _won't_ be able to call Android.Util.Log, because Dalvik
	    /// will destroy the java side of the process.
	    /// </summary>
	    protected void HandleUnhandledException(object sender, UnhandledExceptionEventArgs args)
	    {
	        Exception e = (Exception)args.ExceptionObject;

	        // log won't be available, because dalvik is destroying the process
	        //Log.Debug (logTag, "MyHandler caught : " + e.Message);
	        // instead, your err handling code shoudl be run:
	        Console.WriteLine("========= MyHandler caught : " + e.Message);
	    }
    }
}
