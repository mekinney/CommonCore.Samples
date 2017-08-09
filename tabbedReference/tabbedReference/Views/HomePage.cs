using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class HomePage : BoundPage<AppViewModel>
    {
        public HomePage()
        {
			this.Title = "Home Page";

			var lstView = new ListControl(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(UsersCell)),
				AutomationId = "lstView",
                ItemClickCommand= new Command(async(obj) => {
                    await AppSettings.AppNav.PushAsync(new HomeDetailPage());
                })
			};
			lstView.SetBinding(ListControl.ItemsSourceProperty, "RandomUsers");
            lstView.SetBinding(ListControl.SelectedItemProperty, "SelectedRandomUser");

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
