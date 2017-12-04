using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile.Converters
{
    public class AppConverters
    {
        public static DateLongConverter DateLong
        {
            get
            {
                return CoreDependencyService.GetConverter<DateLongConverter>();
            }
        }
    }
}
