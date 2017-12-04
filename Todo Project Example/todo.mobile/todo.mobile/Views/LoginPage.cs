using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class LoginPage : CorePage<AccountViewModel>
    {
        public LoginPage()
        {
            this.Title = "Login";
            var marginEdge = 30;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            this.BackgroundColor = AppStyles.LightOrangeBackground;

            var img = new Image()
            {
                Source = "loginheader.png",
                Aspect = Aspect.AspectFill
            };

            this.Padding = new Thickness(-9, -6, -9, -6);

            var lblHeader = new Label()
            {
                FontSize = 32,
                Text="Task Manager",
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

            var spacer = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var btnLogin = new CoreButton()
            {
                Text="LOGIN",
                Style = AppStyles.LightOrangeButton,
                Margin = new Thickness(marginEdge, 10, marginEdge, 10)
            };
            btnLogin.SetBinding(CoreButton.CommandProperty, "LoginUser");

            var btnRegister = new CoreButton()
            {
                Text = "REGISTER",
                Style = AppStyles.LightOrangeButton,
                Margin = new Thickness(marginEdge, 10, marginEdge, 25),
                Command = new Command(async(obj) => {
                    await Navigation.PushAsync(new RegisterPage());
                })
            };

            Content = new StackLayout()
            {
                Padding=0,
                Spacing = 0,
                Children = { 
                    img,
                    lblHeader,
                    txtUserName,
                    txtPassword,
                    spacer,
                    btnLogin,
                    btnRegister
                }
            };
        }

    }
}
