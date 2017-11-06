using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore.Styles;

namespace todo.mobile
{
	public class AppStyles: CoreStyles
	{
        public static Color LightOrangeBackground { get; set; } = Color.FromHex("#EDEBDE");

        public static Style LightOrangeButton { get; } = new Style(typeof(CoreButton))
        {
            Setters =
            {
                new Setter(){Property=CoreButton.StartColorProperty ,Value=Color.FromHex("#AF9353")},
                new Setter(){Property=CoreButton.EndColorProperty ,Value=Color.FromHex("#AF9353")},
                new Setter(){Property=CoreButton.ShadowColorProperty ,Value=Color.Gray},
                new Setter(){Property=CoreButton.TextColorProperty ,Value=Color.White},
                new Setter(){Property=CoreButton.ShadowOffsetProperty ,Value=1},
                new Setter(){Property=CoreButton.ShadowOpacityProperty ,Value=1},
                new Setter(){Property=CoreButton.ShadowRadiusProperty ,Value=Device.RuntimePlatform.PlatformValue<float>(5f,8f,5f)},
                new Setter(){Property=CoreButton.CornerRadiusProperty ,Value=Device.RuntimePlatform.PlatformValue<float>(5f,8f,5f)},
            }
        };

        public static Style LoginEntryStyle { get; } = new Style(typeof(CoreUnderlineEntry))
        {
            Setters =
            {
                new Setter(){Property=CoreUnderlineEntry.ClearEntryEnabledProperty ,Value=true},
                new Setter(){Property=CoreUnderlineEntry.ClearEntryIconProperty ,Value="ic_clear_icon.png"},
                new Setter(){Property=CoreUnderlineEntry.MarginProperty ,Value=new Thickness(30, 10, 30, 25)},
            }
        };

	}
}
