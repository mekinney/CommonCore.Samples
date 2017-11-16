using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class HomeDetailPage : CorePage<AppViewModel>
    {
        public HomeDetailPage()
        {
            this.Title = "Detail Page";
            this.BackgroundColor = Color.White;

            Content = new StackLayout()
            {
                Padding = 20,
                Children = { new Label() { Text = "Detail Page" } }
            };
        }
    }
}
