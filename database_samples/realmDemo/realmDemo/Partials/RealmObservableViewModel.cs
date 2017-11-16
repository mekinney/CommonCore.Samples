using System;
using Realms;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        private Realm realmDb;

        protected Realm RealmDb
        {
            get { return realmDb ?? (realmDb = Realm.GetInstance()); }
        }
    }
}
