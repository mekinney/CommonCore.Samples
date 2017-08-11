using System;
using LiteDB;
using litedbDemo;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        private ILiteNoSql liteNoSqlService;

		protected ILiteNoSql LiteNoSqlService
		{
			get { return liteNoSqlService ?? (liteNoSqlService = InjectionManager.GetService<ILiteNoSql, LiteNoSql>(true)); }
		}
    }
}
