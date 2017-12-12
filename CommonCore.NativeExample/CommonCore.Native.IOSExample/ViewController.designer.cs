// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CommonCore.Native.IOSExample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnShowForms { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBinding { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtBinding { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnShowForms != null) {
                btnShowForms.Dispose ();
                btnShowForms = null;
            }

            if (lblBinding != null) {
                lblBinding.Dispose ();
                lblBinding = null;
            }

            if (txtBinding != null) {
                txtBinding.Dispose ();
                txtBinding = null;
            }
        }
    }
}