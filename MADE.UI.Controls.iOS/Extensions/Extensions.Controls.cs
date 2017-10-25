// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Controls.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for iOS controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;
    using System.Linq;

    using Foundation;

    using UIKit;

    /// <summary>
    /// Defines a collection of extensions for iOS controls.
    /// </summary>
    public static partial class Extensions
    {
        private static readonly NSString VisibilityKey = new NSString("VisibilityKey");

        private static readonly NSString DeactivatedConstraintRight = new NSString("DeactivatedConstraintRight");

        private static readonly NSString DeactivatedConstraintBottom = new NSString("DeactivatedConstraintBottom");

        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible(this UIView view, bool isVisible)
        {
            if (view?.Superview == null)
            {
                return;
            }

            if (isVisible)
            {
                if (!view.IsVisible())
                {
                    RestoreConstraint(view, NSLayoutAttribute.Height);
                    RestoreConstraint(view, NSLayoutAttribute.Width);
                    RestoreMargins(view);
                }

                view.Hidden = false;
            }
            else
            {
                if (view.IsVisible())
                {
                    NSLayoutConstraint heightConstraint = GetZeroConstraint(view, NSLayoutAttribute.Height);
                    NSLayoutConstraint widthConstraint = GetZeroConstraint(view, NSLayoutAttribute.Width);

                    ClearMargins(view);

                    view.Superview.AddConstraint(heightConstraint);
                    view.Superview.AddConstraint(widthConstraint);
                }
            }

            view.SetAssociatedObject(VisibilityKey, (NSNumber)isVisible);
        }

        /// <summary>
        /// Retrieves the visibility for the given <see cref="UIView"/>.
        /// </summary>
        /// <param name="view">
        /// The view to retrieve a visibility from.
        /// </param>
        /// <returns>
        /// Returns true if the view is visible.
        /// </returns>
        public static bool IsVisible(this UIView view)
        {
            NSNumber isVisibleInt = new NSNumber(1);
            object associatedObject = view.GetAssociatedObject(VisibilityKey);
            if (associatedObject != null)
            {
                isVisibleInt = (bool)(NSNumber)associatedObject;
            }

            if (isVisibleInt.Int32Value == 1)
            {
                return true;
            }

            return false;
        }

        private static void ClearMargins(UIView view)
        {
            ClearConstraint(view, NSLayoutAttribute.Left);
            ClearConstraint(view, NSLayoutAttribute.Top);
            DeactivateConstraint(view, NSLayoutAttribute.Right);
            DeactivateConstraint(view, NSLayoutAttribute.Bottom);
        }

        private static void RestoreMargins(UIView view)
        {
            RestoreConstraint(view, NSLayoutAttribute.Left);
            RestoreConstraint(view, NSLayoutAttribute.Top);
            ActivateConstraint(view, NSLayoutAttribute.Right);
            ActivateConstraint(view, NSLayoutAttribute.Bottom);
        }

        private static Tuple<NSLayoutConstraint, bool> FindOrCreateConstraint(
            UIView view,
            NSLayoutAttribute layoutAttribute)
        {
            NSLayoutConstraint constraint = GetConstraint(view, view.Superview, layoutAttribute);

            if (constraint != null)
            {
                return Tuple.Create(constraint, false);
            }

            constraint = NSLayoutConstraint.Create(
                view,
                layoutAttribute,
                NSLayoutRelation.Equal,
                null,
                NSLayoutAttribute.NoAttribute,
                1,
                0);

            return Tuple.Create(constraint, true);
        }

        private static void RestoreConstraint(UIView view, NSLayoutAttribute attribute)
        {
            NSLayoutConstraint constraint = GetConstraint(view, view.Superview, attribute);
            if (constraint == null)
            {
                return;
            }

            bool shouldRemoveConstraint = constraint.Restore();
            if (shouldRemoveConstraint)
            {
                view.Superview.RemoveConstraint(constraint);
            }
        }

        private static void ClearConstraint(UIView view, NSLayoutAttribute attribute)
        {
            NSLayoutConstraint constraint = GetConstraint(view, view.Superview, attribute);
            constraint?.Clear(false);
        }

        private static void ActivateConstraint(UIView view, NSLayoutAttribute attribute)
        {
            NSString key = GetDeactivatedConstraintKey(attribute);
            NSLayoutConstraint constraint = GetDeactivatedConstraint(view, key);
            if (constraint != null)
            {
                constraint.Active = true;
            }
        }

        private static void DeactivateConstraint(UIView view, NSLayoutAttribute attribute)
        {
            NSLayoutConstraint constraint = GetConstraint(view, view.Superview, attribute);
            if (constraint == null)
            {
                return;
            }

            constraint.Active = false;
            NSString key = GetDeactivatedConstraintKey(attribute);
            view.SetAssociatedObject(key, constraint);
        }

        private static NSLayoutConstraint GetConstraint(UIView view, UIView constraintView, NSLayoutAttribute attribute)
        {
            return constraintView.Constraints.FirstOrDefault(constraint => constraint.FirstItem.Equals(view) && constraint.FirstAttribute == attribute);
        }

        private static NSLayoutConstraint GetDeactivatedConstraint(UIView view, NSString key)
        {
            return (NSLayoutConstraint)view.GetAssociatedObject(key);
        }

        private static NSString GetDeactivatedConstraintKey(NSLayoutAttribute attribute)
        {
            return attribute == NSLayoutAttribute.Right ? DeactivatedConstraintRight : DeactivatedConstraintBottom;
        }

        private static NSLayoutConstraint GetZeroConstraint(UIView view, NSLayoutAttribute attribute)
        {
            Tuple<NSLayoutConstraint, bool> result = FindOrCreateConstraint(view, attribute);

            NSLayoutConstraint constraint = result.Item1;
            bool didCreateConstraint = result.Item2;

            constraint.Clear(didCreateConstraint);

            return constraint;
        }
    }
}