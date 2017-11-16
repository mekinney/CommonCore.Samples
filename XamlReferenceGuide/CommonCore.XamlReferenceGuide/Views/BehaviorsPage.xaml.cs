using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.XamlReferenceGuide.Views
{
    public partial class BehaviorsPage : CorePage
    {
        public BehaviorsPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();
        }
    }
}
