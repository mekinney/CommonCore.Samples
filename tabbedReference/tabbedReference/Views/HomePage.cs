using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class HomePage : CorePage<AppViewModel>
    {
        public HomePage()
        {
			this.Title = "Home Page";
            this.BackgroundColor = Color.White;
			var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(UsersCell)),
				AutomationId = "lstView",
                ItemClickCommand= new Command(async(obj) => {
                    await CoreSettings.AppNav.PushAsync(new HomeDetailPage());
                })
			};
			lstView.SetBinding(CoreListView.ItemsSourceProperty, "RandomUsers");
            lstView.SetBinding(CoreListView.SelectedItemProperty, "SelectedRandomUser");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if(Device.RuntimePlatform!="iOS")
                GC.Collect();
        }

    }
}
