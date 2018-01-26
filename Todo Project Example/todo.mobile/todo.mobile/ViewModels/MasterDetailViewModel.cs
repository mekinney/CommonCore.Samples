using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class MasterDetailViewModel : CoreViewModel
    {
        private Dictionary<string, NavigationPage> navPages { get; set; } = new Dictionary<string, NavigationPage>();

        public bool IsPresented { get; set; }
        public ObservableCollection<AppMenuItem> MasterPageItems { get; set; }

        public ICommand NavClicked { get; set; }

        public MasterDetailViewModel()
        {
            SetNavigation();
            NavClicked = new CoreCommand(NavClickedMethod);
        }

        private void NavClickedMethod(object obj)
        {
            var item = (AppMenuItem)obj;
            var page = (MasterDetailPage)Application.Current.MainPage;

            if(item.TargetType==null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            if (!navPages.ContainsKey(item.TargetType.Name))
            {
                var np = new NavigationPage((Page)Activator.CreateInstance(item.TargetType))
                {
                    BarBackgroundColor = CoreStyles.NavigationBarColor,
                    BarTextColor = Color.White
                };
                CoreSettings.AppNav = np.Navigation;
                navPages.Add(item.TargetType.Name, np);
            }
            page.Detail = navPages[item.TargetType.Name];

            page.IsPresented = false;
        }

        private void SetNavigation()
        {
            var lst = new List<AppMenuItem>();

            lst.Add(new AppMenuItem
            {
                Title = "List",
                IconSource = "todos.png",
                TargetType = typeof(TodoPage)
            });

            lst.Add(new AppMenuItem
            {
                Title = "Profile",
                IconSource = "profilemenuImage.png",
                TargetType = typeof(ProfilePage)
            });
            lst.Add(new AppMenuItem
            {
                Title = "About",
                IconSource = "aboutMenuImage.png",
                TargetType = typeof(AboutPage)
            });
            lst.Add(new AppMenuItem
            {
                Title = "Logout",
                IconSource = "logoutMenuImage.png",
                TargetType = null
            });

            MasterPageItems = lst.ToObservable();
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if (key == CoreSettings.MasterDetailIsPresented)
            {
                IsPresented = !IsPresented;
            }
        }
    }
}
