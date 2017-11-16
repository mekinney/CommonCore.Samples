using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    
    public class MainTabPage: CoreTabbedPage
    {
        public MainTabPage()
        {
            this.UnSelectedForegroundColor = Color.FromHex("#80FFFFFF");
            this.SelectedForegroundColor = Color.FromHex("#FFFFFF");
            if (Device.RuntimePlatform == "iOS")
                this.TabBackgroundColor = Color.FromHex("#2196f3");

            //NavigationPage.SetHasBackButton(this, false);
			//NavigationPage.SetBackButtonTitle(this, "Back");
            //NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new HomePage(){Title="Home", Icon="home.png"});
            Children.Add(new FavoritesPage() { Title = "Favorites", Icon = "star.png" });
            Children.Add(new AboutPage() { Title = "About", Icon = "info.png" });
           
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            this.Title = this.CurrentPage.Title;

        }
    }
}
