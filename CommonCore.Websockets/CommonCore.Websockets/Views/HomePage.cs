using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class HomePage : BoundPage<AppViewModel>
    {
        public HomePage()
        {
            this.Title = "Home Page";

            var lbl = new Label() { Text = "Enter User Name", Margin=5 };

            var entry = new Entry()
            {
                Margin = 5
            };
            entry.SetBinding(Entry.TextProperty, "FriendlyName");

            var btnStart = new Button()
            {
                Text="Begin"
            };
            btnStart.SetBinding(Button.CommandProperty,"StartSession");

            Content = new StackLayout()
            {
                Padding = 20,
                Children = { lbl, entry,btnStart }
            };
        }
    }
}