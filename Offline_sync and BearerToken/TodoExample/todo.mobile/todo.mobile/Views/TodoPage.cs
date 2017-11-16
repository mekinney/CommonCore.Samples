using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.CommonCore.MaterialDesign;

namespace todo.mobile
{
    public class TodoPage: AbsoluteLayoutPage<TodoViewModel>
    {
        public TodoPage()
        {
            this.Title = "Todo List";
            var fab = new FABControl()
            {
                Size = FABControlSize.Normal,
                ColorNormal = AppStyles.NavigationBarColor,
                ColorPressed = AppStyles.NavigationBarColor.MultiplyAlpha(0.4),
                ImageName = "plus.png"
            };

            fab.SetBinding(FABControl.CommandProperty, "FABClicked");
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(0.95f, 0.95f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            var lst = new CoreListView()
            {
                ItemTemplate = new DataTemplate(typeof(TodoPageCell))
            };
            lst.SetBinding(CoreListView.ItemsSourceProperty, "CurrentTodoList");

            Content = lst;

            AbsoluteLayer.Children.Add(fab);
        }
    }
}
