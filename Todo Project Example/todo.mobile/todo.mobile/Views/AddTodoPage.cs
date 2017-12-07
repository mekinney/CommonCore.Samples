using System;
using FFImageLoading.Forms;
using todo.mobile.Converters;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AddTodoPage : CorePage<TodoViewModel>
    {
        public AddTodoPage()
        {
            this.Title = "Add Todo Item";
            var marginEdge = 20;

            var img = new CachedImage()
            {
                Source = "featherimage.png",
                Margin = 5
            };
            var txtTitle = new Label()
            {
                Text = "Create a new item and specify the date for completion.",
                FontSize = 22,
                Margin = 5,
                TextColor = Color.DarkGray
            };

            var topPanel = new StackLayout()
            {
                Margin = new Thickness(20, 25, 20, 25),
                Orientation = StackOrientation.Horizontal,
                Children = { img, txtTitle }
            };

            var lblDescription = new Label()
            {
                Text = "Description",
                Style = AppStyles.LabelHeader,
            };
            var txtDescription = new CoreUnderlineEntry()
            {
                Style = AppStyles.TodoEntryStyle
            };
            txtDescription.SetBinding(CoreUnderlineEntry.TextProperty, "CurrentItem.Description");

            var lblDueDate = new Label()
            {
                Text = "Due Date",
                Style = AppStyles.LabelHeader,
            };
            var pickerDueDate = new CoreDatePicker()
            {
                Style = AppStyles.TodoPickerStyle
            };
            pickerDueDate.SetBinding(CoreDatePicker.DateProperty, new Binding(path: "CurrentItem.CompleteByDate", mode: BindingMode.TwoWay, converter: AppConverters.DateLong));

            var spacer = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var btnSave = new CoreButton()
            {
                Text = "SAVE",
                Style = AppStyles.LightOrangeButton,
                Margin = new Thickness(marginEdge, 10, marginEdge, 25),
            };
            btnSave.SetBinding(CoreButton.CommandProperty, "SaveCurrentItem");

            Content = new StackLayout()
            {
                Children ={
                    topPanel,
                    lblDescription,
                    txtDescription,
                    lblDueDate,
                    pickerDueDate,
                    spacer,
                    btnSave
                }
            };
        }
    }
}
