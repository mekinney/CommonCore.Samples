using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;

namespace todo.mobile
{
    public class AppStyles : CoreStyles
    {
        public static Color NavigationBarColor { get; set; } = Color.FromHex("#AD9255");
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
                new Setter(){Property=CoreButton.FontSizeProperty ,Value=22},
                new Setter(){Property=CoreButton.ShadowRadiusProperty ,Value= CoreSettings.OnPlatform<float>(5f,8f,5f)},
                new Setter(){Property=CoreButton.CornerRadiusProperty ,Value= CoreSettings.OnPlatform<float>(5f,8f,5f)},
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

        public static Style LabelHeader { get; } = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter(){Property=Label.MarginProperty ,Value=new Thickness(30, 10, 30, 2)},
                new Setter(){Property=Label.FontSizeProperty ,Value=22},
            }
        };

        public static Style TodoEntryStyle { get; } = new Style(typeof(CoreUnderlineEntry))
        {
            Setters =
            {
                new Setter(){Property=CoreUnderlineEntry.ClearEntryEnabledProperty ,Value=true},
                new Setter(){Property=CoreUnderlineEntry.ClearEntryIconProperty ,Value="ic_clear_icon.png"},
                new Setter(){Property=CoreUnderlineEntry.MarginProperty ,Value=new Thickness(30, 2, 30, 25)},
            }
        };

        public static Style TodoPickerStyle { get; } = new Style(typeof(CoreDatePicker))
        {
            Setters =
            {
                new Setter(){Property=CoreDatePicker.MarginProperty ,Value=new Thickness(30, 2, 30, 25)},
            }
        };

    }
}
