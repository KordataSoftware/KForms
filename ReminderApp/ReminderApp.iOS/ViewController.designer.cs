// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ReminderApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView formDataTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton launchReminderFormButton { get; set; }

        [Action ("OnLaunchFormPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnLaunchFormPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (formDataTextView != null) {
                formDataTextView.Dispose ();
                formDataTextView = null;
            }

            if (launchReminderFormButton != null) {
                launchReminderFormButton.Dispose ();
                launchReminderFormButton = null;
            }
        }
    }
}