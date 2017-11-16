using System;
using LiteDB;
using litedbDemo;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        private ILiteNoSql liteNoSqlService;

		protected ILiteNoSql LiteNoSqlService
		{
			get { return liteNoSqlService ?? (liteNoSqlService = CoreDependencyService.GetService<ILiteNoSql, LiteNoSql>(true)); }
		}
    }
}
