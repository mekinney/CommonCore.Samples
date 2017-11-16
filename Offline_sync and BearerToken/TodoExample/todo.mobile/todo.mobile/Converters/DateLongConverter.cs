using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile.Converters
{
    public class AppConverters
    {
        public static DateLongConverter DateLong = new DateLongConverter();
    }
    public class DateLongConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = (long)value;
            if (num == default(long))
            {
                return DateTime.Now;
            }
            else
            {
                return num.ToLocalTime();
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime)value;
            return dt.ToUniversalTime().Ticks;
        }
    }
}
