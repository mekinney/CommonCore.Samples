﻿using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class ProfilePage : CorePage<AccountViewModel>
    {
        public ProfilePage()
        {
            this.Title = "Profile";
            var marginEdge = 20;
            var img = new CachedImage()
            {
                Source = "featherimage.png",
                Margin = 5
            };
            var txtTitle = new Label()
            {
                Text = "Update your profile to include your image on the left.",
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

            var lblFName = new Label()
            {
                Text = "First Name",
                Style = CoreStyles.LabelHeader,
            };
            var txtFName = new CoreUnderlineEntry()
            {
                Style = CoreStyles.TodoEntryStyle
            };
            txtFName.SetBinding(CoreUnderlineEntry.TextProperty, "CurrentUser.FirstName");

            var lblLName = new Label()
            {
                Text = "Last Name",
                Style = CoreStyles.LabelHeader,
            };
            var txtLName = new CoreUnderlineEntry()
            {
                Style = CoreStyles.TodoEntryStyle
            };
            txtLName.SetBinding(CoreUnderlineEntry.TextProperty, "CurrentUser.LastName");

            var lblPassword = new Label()
            {
                Text = "Password",
                Style = CoreStyles.LabelHeader
            };
            var txtPassword = new CoreUnderlineEntry()
            {
                Style = CoreStyles.TodoEntryStyle,
                IsPassword=true
            };
            txtPassword.SetBinding(CoreUnderlineEntry.TextProperty, "Password");

            var lblConfirmPassword = new Label()
            {
                Text = "Confirm Password",
                Style = CoreStyles.LabelHeader,
            };
            var txtConfirmPassword = new CoreUnderlineEntry()
            {
                Style = CoreStyles.TodoEntryStyle
            };
            txtConfirmPassword.SetBinding(CoreUnderlineEntry.TextProperty, "ConfirmPassword");


            var btnSave = new CoreButton()
            {
                Text = "SAVE",
                Style = CoreStyles.LightOrangeButton,
                Margin = new Thickness(marginEdge, 10, marginEdge, 25),
            };
            btnSave.SetBinding(CoreButton.CommandProperty, "SaveProfile");

            var content = new StackLayout()
            {
                Children ={
                    topPanel,
                    lblFName,
                    txtFName,
                    lblLName,
                    txtLName,
                    lblPassword,
                    txtPassword,
                    lblConfirmPassword,
                    txtConfirmPassword,
                    btnSave
                }
            };

            Content = new ScrollView { Content = content };
        }
    }
}