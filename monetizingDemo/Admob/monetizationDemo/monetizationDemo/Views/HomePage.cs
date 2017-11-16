using System;
using FFImageLoading.Forms;
using monetizationDemo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace monetizationDemo.Views
{
    public class HomePage : CorePage<AppViewModel>
    {
        public HomePage()
        {
            this.Title = "Monetization";

            Content = new StackLayout()
            {
                Children = { CreateHeaderSection(), CreateFooterSection() }
            };
           
        }

        private StackLayout CreateHeaderSection()
        {
            var tiggerImg = new CachedImage()
            {
                WidthRequest = 150,
                HeightRequest = 150,
                Margin = new Thickness(20, 50, 20, 20),
                Source = "tigger.png"
            };

            var lbl = new Label()
            {
                Margin = new Thickness(20, 10, 20, 10),
                TextColor = Color.DarkGray,
                FontSize = 32,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Admob Example"
            };

            var btnFullBanner = new Button()
            {
                Text ="Show Interstitial",
                Margin = new Thickness(20, 10, 20, 10)
            };
            btnFullBanner.SetBinding(Button.CommandProperty, "ShowInterstitial");

            var btnRemoveAds = new Button()
            {
                Text = "Pay to remove",
                Margin = new Thickness(20, 10, 20, 10)
            };
            btnRemoveAds.SetBinding(Button.CommandProperty, "RemoveAds");

            var topContent = new StackLayout()
            {
                Children = { tiggerImg, lbl, btnFullBanner, btnRemoveAds },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            return topContent;
        }

        private StackLayout CreateFooterSection()
        {
            var ad = new AdBanner()
            {
                Size = AdBanner.Sizes.Standardbanner
            };

            var bottomContent = new StackLayout()
            {
                Children = { ad }
            };
            return bottomContent;
        }

    }
}
