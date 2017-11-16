using System;
using couchdbDemo;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        private ICouchDb couchDb;

        protected ICouchDb CouchDb
        {
            get { return couchDb ?? (couchDb = CoreDependencyService.GetService<ICouchDb, CouchDb>(true)); }
        }
    }
}
