using System;
using charts.commoncore.demo;
using Xamarin.Forms.CommonCore;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreSettings
    {
        public static BoolToImageConvert ImageConverter { get; set; } = new BoolToImageConvert();
    }
}
