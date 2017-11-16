using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.XamlReferenceGuide.Views
{
    public partial class ListControlPage : CorePage
    {
        public ListControlPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();
        }
    }
}
