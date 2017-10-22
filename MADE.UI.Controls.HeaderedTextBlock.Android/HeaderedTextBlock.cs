// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;

    using Android.Content;
    using Android.Content.Res;
    using Android.Runtime;
    using Android.Util;
    using Android.Widget;

    using Orientation = MADE.UI.Layout.Orientation;

    /// <summary>
    /// Defines a UI element representing read-only text with a header component.
    /// </summary>
    public class HeaderedTextBlock : Control, IHeaderedTextBlock
    {
        private bool hideIfNullOrWhiteSpace;

        private LinearLayout container;

        private TextView headerTextView;

        private TextView textTextView;

        private Orientation orientation;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        /// <param name="javaReference">
        /// The Java reference.
        /// </param>
        /// <param name="transfer">
        /// The JNI handle ownership.
        /// </param>
        public HeaderedTextBlock(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        public HeaderedTextBlock(Context context)
            : base(context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        public HeaderedTextBlock(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
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
        public HeaderedTextBlock(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
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
        public HeaderedTextBlock(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can interact with the control.
        /// </summary>
        public override bool IsEnabled { get; set; }

        /// <summary>
        /// Gets the reference identifier for the control's layout.
        /// </summary>
        public override int LayoutReference => Resource.Layout.HeaderedTextBlock;

        /// <summary>
        /// Gets or sets the string associated with the header.
        /// </summary>
        public string Header
        {
            get => this.headerTextView?.Text;
            set
            {
                if (this.headerTextView == null || value == this.headerTextView.Text)
                {
                    return;
                }

                this.headerTextView.Text = value;
                this.RaisePropertyChanged();
                this.UpdateVisibility();
            }
        }

        /// <summary>
        /// Gets or sets the string associated with the text.
        /// </summary>
        public string Text
        {
            get => this.textTextView?.Text;
            set
            {
                if (this.textTextView == null || value == this.headerTextView.Text)
                {
                    return;
                }

                this.textTextView.Text = value;
                this.RaisePropertyChanged();
                this.UpdateVisibility();
            }
        }

        /// <summary>
        /// Gets or sets the orientation the header and text should layout as.
        /// </summary>
        public Orientation Orientation
        {
            get => this.orientation;
            set
            {
                this.Set(() => this.Orientation, ref this.orientation, value);
                this.UpdateOrientation();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the control if the <see cref="IHeaderedTextBlock.Text"/> is null or whitespace.
        /// </summary>
        public bool HideIfNullOrWhiteSpace
        {
            get => this.hideIfNullOrWhiteSpace;
            set
            {
                this.Set(() => this.HideIfNullOrWhiteSpace, ref this.hideIfNullOrWhiteSpace, value);
                this.UpdateVisibility();
            }
        }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        /// <param name="defStyleAttr">
        /// The XML default style attribute.
        /// </param>
        /// <param name="defStyleRes">
        /// The default style resource.
        /// </param>
        public override void OnApplyTemplate(IAttributeSet attrs, int defStyleAttr, int defStyleRes)
        {
            this.container = this.GetTemplateChild<LinearLayout>(Resource.Id.htb_container);
            this.headerTextView = this.GetTemplateChild<TextView>(Resource.Id.htb_header);
            this.textTextView = this.GetTemplateChild<TextView>(Resource.Id.htb_text);

            if (attrs != null)
            {
                TypedArray typedArray = this.Context.ObtainStyledAttributes(
                    attrs,
                    Resource.Styleable.HeaderedTextBlock,
                    defStyleAttr,
                    defStyleRes);

                this.hideIfNullOrWhiteSpace = typedArray.GetBoolean(
                    Resource.Styleable.HeaderedTextBlock_hide_if_empty,
                    false);

                if (this.headerTextView != null)
                {
                    string header = typedArray.GetString(Resource.Styleable.HeaderedTextBlock_header);
                    this.headerTextView.Text = header;
                }

                if (this.textTextView != null)
                {
                    string text = typedArray.GetString(Resource.Styleable.HeaderedTextBlock_text);
                    this.textTextView.Text = text;
                }
            }

            this.UpdateVisibility();
            this.UpdateOrientation();
        }

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="IHeaderedTextBlock.Header"/> and <see cref="IHeaderedTextBlock.Text"/> properties.
        /// </summary>
        public void UpdateVisibility()
        {
            if (!this.HideIfNullOrWhiteSpace || !string.IsNullOrWhiteSpace(this.Text))
            {
                this.IsVisible = true;
                this.headerTextView?.SetVisible(!string.IsNullOrWhiteSpace(this.Header));
                this.textTextView?.SetVisible(!string.IsNullOrWhiteSpace(this.Text));
            }
            else
            {
                this.IsVisible = false;
                this.headerTextView?.SetVisible(false);
                this.textTextView?.SetVisible(false);
            }
        }

        /// <summary>
        /// Updates the layout for the control based on the current <see cref="IHeaderedTextBlock.Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            this.container?.SetOrientation(this.Orientation);
        }
    }
}