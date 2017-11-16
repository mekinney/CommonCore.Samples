using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class AboutPage: CorePage<AppViewModel>
    {
        public AboutPage()
        {
            this.Title = "About";
            this.BackgroundColor = Color.White;

            Content = new StackLayout()
            {
                Padding = 20,
                Children = { new Label() { Text = "About Page" } }
            };
        }
    }
}
