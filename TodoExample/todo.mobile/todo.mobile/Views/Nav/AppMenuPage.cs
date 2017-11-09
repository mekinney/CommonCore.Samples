using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppMenuPage : BoundPage<MasterDetailViewModel>
    {
        public AppMenuPage()
        {
          
            Icon = "hamburger.png";
            Title = "Reference Guide";
            BackgroundColor = AppStyles.NavigationBarColor;
            Padding = new Thickness(0, 40, 0, 0);

            var personImage = new CachedImage()
            {
                Margin = 5,
                Source = "personMenuPlaceholder.png"
            };
            var navTitle = new Label()
            {
                Text = "Common Core",
                TextColor = Color.White,
                Margin = 5
            };
            var navSubtitle = new Label()
            {
                Text = "Options Menu",
                TextColor = Color.White
            };

            var topPanel = new StackLayout()
            {
                Padding = new Thickness(10, 0, 10, 10),
                BackgroundColor = AppStyles.NavigationBarColor,
                Orientation = StackOrientation.Horizontal,
                Children = { personImage, new StackLayout() { Children = { navTitle, navSubtitle } } }
            };

            var listView = new CoreListView
            {
                BackgroundColor = AppStyles.LightOrangeBackground,
                ItemTemplate = new DataTemplate(typeof(AppMenuCell)),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None,
            };
            listView.SetBinding(CoreListView.ItemsSourceProperty, "MasterPageItems");
            listView.SetBinding(CoreListView.ItemClickCommandProperty, "NavClicked");


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = 
                {
                    topPanel,
                    listView
                }
            };

        }
    }
}
