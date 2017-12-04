using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class RegisterPage : CorePage<AccountViewModel>
    {
        public RegisterPage()
        {
            this.Title = "Login";
            var marginEdge = 30;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            this.BackgroundColor = AppStyles.LightOrangeBackground;

            var img = new Image()
            {
                Source = "registerheader.png",
                Aspect = Aspect.AspectFill
            };

            this.Padding = new Thickness(-9, -6, -9, -6);

            var lblHeader = new Label()
            {
                FontSize = 32,
                Text = "Create Account",
                Margin = new Thickness(marginEdge, 5, marginEdge, 20),
                HorizontalOptions = LayoutOptions.Center
            };

            var txtUserName = new CoreUnderlineEntry()
            {
                Placeholder = "User Name",
                Style = AppStyles.LoginEntryStyle,
            };
            txtUserName.SetBinding(CoreUnderlineEntry.TextProperty, "UserName");

            var txtPassword = new CoreUnderlineEntry()
            {
                Placeholder = "Password",
                IsPassword = true,
                Style = AppStyles.LoginEntryStyle,
            };
            txtPassword.SetBinding(CoreUnderlineEntry.TextProperty, "Password");

            var txtConfirmPassword = new CoreUnderlineEntry()
            {
                Placeholder = "Confirm Password",
                IsPassword = true,
                Style = AppStyles.LoginEntryStyle,
            };
            txtConfirmPassword.SetBinding(CoreUnderlineEntry.TextProperty, "ConfirmPassword");

            var spacer = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            var btnCreate = new CoreButton()
            {
                Text = "CREATE",
                Style = AppStyles.LightOrangeButton,
                Margin = new Thickness(marginEdge, 10, marginEdge, 25)
            };
            btnCreate.SetBinding(CoreButton.CommandProperty, "RegisterUser");

            var pageContent = new StackLayout()
            {

                Children = {
                    img,
                    lblHeader,
                    txtUserName,
                    txtPassword,
                    txtConfirmPassword,
                    spacer,
                    btnCreate
                }
            };

            Content = new ScrollView()
            {
                Padding = 0,
                Margin=0,
                Content = pageContent
            };
        }

    }
}
