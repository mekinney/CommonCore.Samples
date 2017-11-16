using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
    public class PageTwo: CorePage<AppViewModel>
    {
        public PageTwo()
        {
            var lst = new ListView()
            {
                ItemTemplate = new DataTemplate(typeof(PageTwoCell)),
                ItemsSource = VM.People
            };
            lst.SetBinding(ListView.ItemsSourceProperty, "People");

            Content = new StackLayout()
            {
                Children = { lst }
            };
        }
    }
}
