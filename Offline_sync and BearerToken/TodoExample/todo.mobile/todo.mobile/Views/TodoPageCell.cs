using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class TodoPageCell : ViewCell
    {
        private Label title;
        private Label date;

        public TodoPageCell()
        {
            title = new Label()
            {
                FontSize = 22
            };
            date = new Label()
            {
                FontSize = 18,
            };

            var rightPanel = new StackLayout()
            {
                Children = { title, date }
            };

            var img = new CachedImage()
            {
                Margin = new Thickness(10, 0, 3, 5),
                HeightRequest = 32,
                WidthRequest = 32,
                DownsampleHeight = 32,
                DownsampleWidth = 32,
                Aspect = Aspect.AspectFit,
                CacheDuration = TimeSpan.FromDays(30),
                VerticalOptions = LayoutOptions.Center,
                DownsampleUseDipUnits = true,
                Source = ""
            };

            View = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { img, rightPanel }
            };

        }

        //On a listview that uses RecycleElement binding can be costly
        protected override void OnBindingContextChanged()
        {
            var item = (Todo)BindingContext;
            title.Text = item.Description;
            date.Text = item.CompleteByDate.ToLocalTime().ToShortDateString();

            base.OnBindingContextChanged();
        }
    }
}
