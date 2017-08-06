using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class FavoritesPage: BoundPage<AppViewModel>
    {
        public FavoritesPage()
        {
			var lstView = new ListControl(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(FavoritesCell)),
				AutomationId = "lstView",
			};
			lstView.SetBinding(ListControl.ItemsSourceProperty, "Favorites");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }
    }
}
