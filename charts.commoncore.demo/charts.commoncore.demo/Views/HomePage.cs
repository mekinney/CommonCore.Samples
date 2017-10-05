using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class HomePage : BoundPage<AppViewModel>
    {
        public HomePage()
        {
            this.Title = "Home Page";

            this.ToolbarItems.Add(new ToolbarItem("Charts", null, 
                () =>{
                    Navigation.PushNonAwaited<ChartsPage>();
                })
            {
                AutomationId = "Charts"
            });

            var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
                AutomationId = "lstView"
            };
            lstView.SetBinding(CoreListView.ItemsSourceProperty, "Users");


            Content = new StackLayout()
            {
                Children = { lstView }
            };
        }
    }
}
