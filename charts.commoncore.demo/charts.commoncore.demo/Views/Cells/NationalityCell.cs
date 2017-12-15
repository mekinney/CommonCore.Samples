using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class NationalityCell: ViewCell
    {
        public NationalityCell()
        {
            Height = 45;
            var img = new CachedImage()
            {
                Margin = new Thickness(8, 4, 4, 4),
                HeightRequest = 24,
                WidthRequest = 24,
                RetryCount = 0,
                RetryDelay = 250,
                LoadingPlaceholder = "unchecked.png",
                CacheDuration = TimeSpan.FromDays(10),
            };
            img.SetBinding(CachedImage.SourceProperty, new Binding(path: "IsSelected", converter: CoreSettings.ImageConverter));

            var lbl = new Label()
            {
                FontSize=18,
                VerticalOptions = LayoutOptions.Center
            };
            lbl.SetBinding(Label.TextProperty,"Description");

            View = new StackLayout()
            {
                Padding=10,
                Spacing=10,
                Orientation = StackOrientation.Horizontal,
                Children={img,lbl}
            };
        }
    }
}
