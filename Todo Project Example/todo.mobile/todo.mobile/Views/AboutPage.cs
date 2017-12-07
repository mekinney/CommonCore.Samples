using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AboutPage : CorePage<AccountViewModel>
    {
        public AboutPage()
        {
            this.Title = "About";

            var img = new CachedImage()
            {
                Source = "paperStackImage.png",
                Margin = 5
            };
            var txtTitle = new Label()
            {
                Text = "Todo Manager.",
                FontSize = 32,
                Margin = 5,
                TextColor = Color.DarkGray
            };

            var topPanel = new StackLayout()
            {
                Margin = new Thickness(20, 25, 20, 25),
                Orientation = StackOrientation.Horizontal,
                Children = { img, txtTitle }
            };

            var aboutText = new FormattedString();
            aboutText.AddTextSpan("Version 1.0.0 \r");
            aboutText.AddTextSpan("Xamarin Meetup Demo \r");
            aboutText.AddTextSpan("by \r");
            aboutText.AddTextSpan("Les Brown \r");

            Content = new StackLayout()
            {
                Children ={
                    topPanel,
                    new Label()
                    {
                        TextColor = Color.DarkGray,
                        Margin = new Thickness(20),
                        FormattedText=aboutText,
                        FontSize = 32,
                        HorizontalTextAlignment = TextAlignment.Center
                            
                    }
                }
            };
        }
    }
}
