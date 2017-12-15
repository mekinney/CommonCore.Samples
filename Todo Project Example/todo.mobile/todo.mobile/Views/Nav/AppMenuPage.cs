using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppMenuPage : CorePage<MasterDetailViewModel>
    {
        public AppMenuPage()
        {
          
            Icon = "hamburger.png";
            Title = "Reference Guide";
            BackgroundColor = CoreStyles.NavigationBarColor;
            Padding = new Thickness(0, 40, 0, 0);

            var personImage = new CachedImage()
            {
                Margin = 5,
                Source = "personMenuPlaceholder.png"
            };
            var navTitle = new Label()
            {
                Text = "Jack Sparrow",
                TextColor = Color.White,
                FontSize = 24,
                Margin = new Thickness(5,15,5,0)
            };
            var navSubtitle = new Label()
            {
                Text = "Administrator",
                TextColor = Color.White,
                FontSize = 14,
                Margin = new Thickness(5, 0, 5, 0)
            };

            var topPanel = new StackLayout()
            {
                Padding = new Thickness(10, 0, 10, 10),
                BackgroundColor = CoreStyles.NavigationBarColor,
                Orientation = StackOrientation.Horizontal,
                Children = { personImage, new StackLayout() { Children = { navTitle, navSubtitle } } }
            };

            var listView = new CoreListView
            {
                BackgroundColor = CoreStyles.LightOrangeBackground,
                ItemTemplate = new DataTemplate(typeof(AppMenuCell)),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 50
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
