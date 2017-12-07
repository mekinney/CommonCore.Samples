using System;
using todo.mobile.Converters;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.CommonCore.MaterialDesign;

namespace todo.mobile
{
    public class TodoPage : CoreAbsoluteLayoutPage<TodoViewModel>
    {

        public TodoPage()
        {
            this.Title = "Todo List";

            var fab = new CoreFloatingActionButton()
            {
                Size = FABControlSize.Normal,
                ColorNormal = AppStyles.NavigationBarColor,
                ColorPressed = AppStyles.NavigationBarColor.MultiplyAlpha(0.4),
                ImageName = "plus.png",
            };

            fab.SetBinding(CoreFloatingActionButton.CommandProperty, "FABClicked");
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(0.95f, 0.95f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            var lst = new CoreListView()
            {
                IsPullToRefreshEnabled=true,
                ItemTemplate = new DataTemplate(typeof(TodoPageCell)),
                RowHeight = 65
            };
            lst.SetBinding(CoreListView.ItemsSourceProperty, "CurrentTodoList");
            lst.SetBinding(CoreListView.IsRefreshingProperty,"IsRefreshing");
            lst.SetBinding(CoreListView.RefreshCommandProperty,"RefreshData");
            lst.SetBinding(CoreListView.IsVisibleProperty, "DataExists");
            lst.Effects.Add(new HideListSeparatorEffect());
#if __IOS__
            lst.Effects.Add(new RemoveEmptyRowsEffect());
#endif


            var imgLabel = new Label()
            {
                FontSize = 75,
                TextColor = Color.LightGray,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            imgLabel.SetBinding(Label.TextProperty, "EmptyDataIcon.Unicode");
            imgLabel.SetBinding(Label.FontFamilyProperty, "EmptyDataIcon.FontFamily");

            var imgDescription = new Label()
            {
                TextColor = Color.LightGray,
                FontSize = 32,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment =  TextAlignment.Center,
                Text = "The dataset is empty. Try adding an item manually."
            };

            var emptyPanel = new StackLayout()
            {
                Padding = 30,
                Children = { imgLabel, imgDescription }
            };
            emptyPanel.SetBinding(StackLayout.IsVisibleProperty, 
                                  new Binding(path:"DataExists", converter:AppConverters.ReverseBoolean));

            Content = new StackLayout()
            {
                Children = { lst, emptyPanel }
            };

            AbsoluteLayer.Children.Add(fab);
        }
    }
}
