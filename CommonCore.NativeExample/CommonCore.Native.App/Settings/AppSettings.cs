using System;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Native.App
{
    public class AppSettings : CoreSettings
    {
        public static SomeValueConverter UpperText
        {
            get
            {
                return CoreDependencyService.GetConverter<SomeValueConverter>();
            }
        }

    }
}
