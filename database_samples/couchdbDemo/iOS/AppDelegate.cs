using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.Lite;
using Foundation;
using UIKit;

namespace couchdbDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

			//Couchbase.Lite.Storage.SystemSQLite.Plugin.Register();
			//var _db = Manager.SharedInstance.GetDatabase("couchdb-demo");

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
