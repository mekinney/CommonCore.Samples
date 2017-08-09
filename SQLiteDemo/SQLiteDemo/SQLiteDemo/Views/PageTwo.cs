using System;
using SQLiteDemo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace SQLiteDemo.Views
{
    public class PageTwo: BoundPage<AppViewModel>
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
