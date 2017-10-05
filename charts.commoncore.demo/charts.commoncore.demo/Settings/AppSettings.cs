using System;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class AppSettings : CoreSettings
    {
        public static BoolToImageConvert ImageConverter { get; set; } = new BoolToImageConvert();
    }
}
