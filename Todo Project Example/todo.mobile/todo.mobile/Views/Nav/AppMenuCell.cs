using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace todo.mobile
{
    public class AppMenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }

    public class AppMenuCell : ViewCell
    {
        private readonly CachedImage img;
        private readonly Label lbl;

        public AppMenuCell()
        {
            Height = 50;
            img = new CachedImage()
            {
                Margin = new Thickness(10, 0, 3, 5),
                HeightRequest = 32,
                WidthRequest = 32,
                DownsampleHeight = 32,
                DownsampleWidth = 32,
                Aspect = Aspect.AspectFit,
                CacheDuration = TimeSpan.FromDays(30),
                VerticalOptions = LayoutOptions.Center,
                DownsampleUseDipUnits = true
            };

            lbl = new Label()
            {
                Margin = 5,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 24
            };

            View = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { img, lbl }
            };
        }

        //On a listview that uses RecycleElement binding can be costly
        protected override void OnBindingContextChanged()
        {
            var item = (AppMenuItem)BindingContext;
            img.Source = item.IconSource;
            lbl.Text = item.Title;

            base.OnBindingContextChanged();
        }
    }
}
