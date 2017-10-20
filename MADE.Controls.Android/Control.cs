// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an base class for UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Controls
{
    using System;

    using Android.Content;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    /// <summary>
    /// Defines an base class for UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public abstract class Control : FrameLayout, IAndroidControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="javaReference">
        /// The Java reference.
        /// </param>
        /// <param name="transfer">
        /// The JNI handle ownership.
        /// </param>
        protected Control(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        protected Control(Context context)
            : base(context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        protected Control(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        /// <param name="defStyleAttr">
        /// The XML default style attribute.
        /// </param>
        protected Control(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        /// <param name="defStyleAttr">
        /// The XML default style attribute.
        /// </param>
        /// <param name="defStyleRes">
        /// The default style resource.
        /// </param>
        protected Control(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        /// <summary>
        /// The event associated with the control being loaded.
        /// </summary>
        public event ControlLoadedEventHandler ControlLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether the user can interact with the control.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible to the user.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets the reference identifier for the control's layout.
        /// </summary>
        public int LayoutReference { get; }

        /// <summary>
        /// Gets the view associated with the inflated layout.
        /// </summary>
        public View View { get; }

        /// <summary>
        /// Retrieves the element from the instantiated control template by the given resource identifier.
        /// </summary>
        /// <param name="resourceId">
        /// The element resource identifier.
        /// </param>
        /// <typeparam name="TElement">
        /// The type of element to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the element from the template, if the element is found.
        /// </returns>
        public TElement GetTemplateChild<TElement>(int resourceId)
            where TElement : View
        {
            return this.View?.FindViewById<TElement>(resourceId);
        }
    }
}