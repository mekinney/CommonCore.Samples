using System;
using couchdbDemo;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        private ICouchDb couchDb;

        protected ICouchDb CouchDb
        {
            get { return couchDb ?? (couchDb = InjectionManager.GetService<ICouchDb, CouchDb>(true)); }
        }
    }
}
