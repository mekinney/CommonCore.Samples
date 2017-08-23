
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
    public class PageOne : BoundPage<AppViewModel>
    {
        public PageOne()
        {
            var lblFName = new Label()
            {
                Margin = 5,
                Text = "First Name"
            };
            var txtFName = new Entry()
            {
                Margin = 5,
            };
            txtFName.SetBinding(Entry.TextProperty, "NewPerson.FirstName");
            var lblLName = new Label()
            {
                Margin = 5,
                Text = "Last Name"
            };
            var txtLName = new Entry()
            {
                Margin = 5,
            };
            txtLName.SetBinding(Entry.TextProperty, "NewPerson.LastName");
            var lblPhone = new Label()
            {
                Margin = 5,
                Text = "Phone Number"
            };
            var txtPhone = new Entry()
            {
                Margin = 5,
                Keyboard = Keyboard.Telephone
            };
            txtPhone.SetBinding(Entry.TextProperty, "NewPerson.PhoneNumber");
      
            var btnAdd = new Button()
            {
                Text="Add Person"
            };
            btnAdd.SetBinding(Button.CommandProperty, "AddPerson");

			var btnView = new Button()
			{
				Text = "View People"
			};
			btnView.SetBinding(Button.CommandProperty, "ViewPeople");

            Content = new StackLayout()
            {
                Padding = 10,
                Children = { lblFName, txtFName, lblLName, txtLName, lblPhone, txtPhone, btnAdd, btnView }
            };
        }

    }
}
