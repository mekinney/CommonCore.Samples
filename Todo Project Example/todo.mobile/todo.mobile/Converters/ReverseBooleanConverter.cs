using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class ReverseBooleanConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (bool)value;
            return !result;
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
