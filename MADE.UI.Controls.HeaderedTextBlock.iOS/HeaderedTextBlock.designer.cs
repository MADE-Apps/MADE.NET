// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MADE.UI.Controls
{
    [Register ("HeaderedTextBlock")]
    partial class HeaderedTextBlock
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView ContainerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HeaderUiLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView RootView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TextUiLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContainerView != null) {
                ContainerView.Dispose ();
                ContainerView = null;
            }

            if (HeaderUiLabel != null) {
                HeaderUiLabel.Dispose ();
                HeaderUiLabel = null;
            }

            if (RootView != null) {
                RootView.Dispose ();
                RootView = null;
            }

            if (TextUiLabel != null) {
                TextUiLabel.Dispose ();
                TextUiLabel = null;
            }
        }
    }
}