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
            this.Height = 65;
            title = new Label()
            {
                Margin = new Thickness(0,8,0,0),
                FontSize = 22
            };
            date = new Label()
            {
                FontSize = 14,
                TextColor = Color.DarkGray,
                Margin = new Thickness(0, 0, 0, 8),
            };

            var rightPanel = new StackLayout()
            {
                Children = { title, date }
            };

            var img = new CachedImage()
            {
                Margin = new Thickness(10, 0, 3, 0),
                HeightRequest = 55,
                WidthRequest = 55,
                DownsampleHeight = 55,
                DownsampleWidth = 55,
                Aspect = Aspect.AspectFit,
                CacheDuration = TimeSpan.FromDays(30),
                VerticalOptions = LayoutOptions.Center,
                DownsampleUseDipUnits = true,
                Source = "todorowimage.png"
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
