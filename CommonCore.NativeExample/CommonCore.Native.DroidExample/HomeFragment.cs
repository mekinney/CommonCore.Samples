using System;
using Android.OS;
using Android.Views;
using DroidTextView = Android.Widget.TextView;
using DroidEditText = Android.Widget.EditText;
using DroidButton = Android.Widget.Button;
using DroidView = Android.Views.View;
using CommonCore.Native.App;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.CommonCore.Native;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CommonCore.Native.DroidExample
{
    public class HomeFragment : Android.Support.V4.App.Fragment, IHandlers
    {
        private BindingManager<HomeFragment, SomeViewModel> bind;
        private DroidEditText txtBinding { get; set; }
        private DroidTextView lblDisplay { get; set; }
        private DroidButton btnNavigate { get; set; }

        public SomeViewModel VM { get { return CoreDependencyService.GetViewModel<SomeViewModel>(true); } }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override DroidView OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            bind = new BindingManager<HomeFragment, SomeViewModel>();
            var view = inflater.Inflate(Resource.Layout.home, container, false);
            this.CreateControls<HomeFragment>(view);

            bind.BindProperty(() => txtBinding.Text, nameof(txtBinding.TextChanged), () => VM.SomeText);
            bind.BindProperty(() => lblDisplay.Text, null, () => VM.SomeText);
            return view;
        }

        public override void OnResume()
        {
            bind.RegisterBindingEvents(this, VM);
            base.OnResume();
        }
        public override void OnPause()
        {
            bind.UnRegisterBindingEvents(this);
            base.OnPause();
        }

        public void ControlsHandler(object sender, EventArgs args)
        {
            if (sender is DroidView)
            {
                var id = ((DroidView)sender).Id;
                switch (id)
                {
                    case Resource.Id.btnNavigate:

                        var ft = this.Activity.SupportFragmentManager.BeginTransaction();
                        var xfPage = new SomePage().CreateSupportFragment(this.Activity);
                        ft.AddToBackStack(null);
                        ft.Replace(Resource.Id.mainlayout, xfPage, "someFragment");
                        ft.Commit();
                        break;
                }
            }
        }

        public void ViewEventHandler(object e, EventArgs args)
        {

        }

    }
}
