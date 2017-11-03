using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class LandingPage : BoundPage<AppViewModel>
    {
        public LandingPage()
        {
            this.Title = "Some Page";

            var lbl = new Label() { Text = "I am a label" };

            Content = new StackLayout()
            {
                Padding = 20,
                Children = { lbl }
            };
        }

    }
}