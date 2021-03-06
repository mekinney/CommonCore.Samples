﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppMasterDetailPage : CoreMasterDetailPage<MasterDetailViewModel>
    {
        public static Page CurrentDetail { get; set; }
        public AppMasterDetailPage()
        {

            try
            {
                Master = new AppMenuPage();
                Detail = new NavigationPage(new TodoPage())
                {
                    BarBackgroundColor = CoreStyles.NavigationBarColor,
                    BarTextColor = Color.White
                };
                CoreSettings.AppNav = Detail.Navigation;
                AppMasterDetailPage.CurrentDetail = Detail;

            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            this.SetBinding(MasterDetailPage.IsPresentedProperty, new Binding("IsPresented", BindingMode.TwoWay));

        }
    }
}
