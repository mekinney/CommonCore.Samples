using System;
using CommonCore.Native.App;
using UIKit;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.CommonCore.Native;
using Xamarin.Forms;

namespace CommonCore.Native.IOSExample
{
    public partial class ViewController : UIViewController, IHandlers
    {
        public SomeViewModel VM { get { return CoreDependencyService.GetViewModel<SomeViewModel>(true); } }
        private BindingManager<ViewController, SomeViewModel> bind;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            bind = new BindingManager<ViewController, SomeViewModel>();
            bind.BindProperty(() => txtBinding.Text, nameof(txtBinding.EditingChanged), () => VM.SomeText);
            bind.BindProperty(() => lblBinding.Text, null, () => VM.SomeText);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            bind.RegisterBindingEvents(this, VM);
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            bind.UnRegisterBindingEvents(this);
            base.ViewWillDisappear(animated);
        }

        public void ViewEventHandler(object e, EventArgs args)
        {
            //throw new NotImplementedException();
        }

        public void ControlsHandler(object sender, EventArgs args)
        {
            if (sender == btnShowForms)
            {
                var page = new SomePage().CreateViewController();
                this.NavigationController.PushViewController(page, true);
            }
        }
    }
}
