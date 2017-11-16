using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class FavoritesPage: CorePage<AppViewModel>
    {
        public FavoritesPage()
        {
            this.Title = "Favorites";
            this.BackgroundColor = Color.White;
			var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(FavoritesCell)),
				AutomationId = "lstView",
			};
			lstView.SetBinding(CoreListView.ItemsSourceProperty, "Favorites");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }
    }
}
