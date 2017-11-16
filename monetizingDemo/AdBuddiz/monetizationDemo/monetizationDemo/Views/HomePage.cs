using System;
using AdBuddiz.Xamarin;
using FFImageLoading.Forms;
using monetizationDemo.ViewModels;
using Plugin.InAppBilling.Abstractions;
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
                Children = { CreateHeaderSection() }
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

            var btnRemoveAds = new Button()
            {
                Text = "Pay to remove",
                Margin = new Thickness(20, 10, 20, 10)
            };
            btnRemoveAds.SetBinding(Button.CommandProperty, "RemoveAds");

            var topContent = new StackLayout()
            {
                Children = { tiggerImg, lbl, btnRemoveAds },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            return topContent;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var fileSrv = CoreDependencyService.GetService<IFileStore, FileStore>();
            fileSrv?.GetAsync<InAppBillingPurchase>("adsRemoved").ContinueWith((t) => {
                var result = t.Result;
                if(result.Error!=null){
                    if (AdBuddizHandler.Instance.IsReadyToShowAd)
                    {
                        AdBuddizHandler.Instance.ShowAd();
                    }
                }
            });


        }
    }
}
