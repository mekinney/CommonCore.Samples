using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace couchdbDemo
{
    public class PageTwo: BoundPage<AppViewModel>
    {
        public PageTwo()
        {
            var lst = new ListView()
            {
                ItemTemplate = new DataTemplate(typeof(PageTwoCell))
            };
            lst.SetBinding(ListView.ItemsSourceProperty, "People");

            Content = new StackLayout()
            {
                Children = { lst }
            };
        }
    }
}
