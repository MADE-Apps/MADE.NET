// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Views
{
    using System;

    using Android.Content;
    using Android.Content.Res;
    using Android.Runtime;
    using Android.Util;
    using Android.Widget;

    using MADE.App.Views.Controls;
    using MADE.App.Views.Extensions;

    using Orientation = XPlat.UI.Controls.Orientation;

    /// <summary>
    /// Defines a UI element representing read-only text with a header component.
    /// </summary>
    public class HeaderedTextBlock : Control, IHeaderedTextBlock
    {
        private Orientation orientation;

        private bool hideIfNullOrWhiteSpace;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        /// <param name="javaReference">
        /// The Java reference.
        /// </param>
        /// <param name="transfer">
        /// The JNI handle ownership.
        /// </param>
        protected HeaderedTextBlock(IntPtr javaReference, JniHandleOwnership transfer)
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
        /// Gets the ID for the Android layout associated with the view.
        /// </summary>
        public override int LayoutId => Controls.HeaderedTextBlock.Resource.Layout.HeaderedTextBlockLayout;

        /// <summary>
        /// Gets the UI element associated with the container for the header and text content.
        /// </summary>
        public LinearLayout HeaderTextBlockContainer { get; private set; }

        /// <summary>
        /// Gets the UI element associated with the header text.
        /// </summary>
        public TextView HeaderContent { get; private set; }

        /// <summary>
        /// Gets the UI element associated with the content text.
        /// </summary>
        public TextView TextContent { get; private set; }

        /// <summary>
        /// Gets or sets the string associated with the header.
        /// </summary>
        public string Header
        {
            get => this.HeaderContent?.Text;
            set
            {
                if (this.HeaderContent == null || value == this.HeaderContent.Text)
                {
                    return;
                }

                this.HeaderContent.Text = value;
                this.RaisePropertyChanged();
                this.UpdateVisibility();
            }
        }

        /// <summary>
        /// Gets or sets the string associated with the text.
        /// </summary>
        public string Text
        {
            get => this.TextContent?.Text;
            set
            {
                if (this.TextContent == null || value == this.TextContent.Text)
                {
                    return;
                }

                this.TextContent.Text = value;
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
        /// Gets or sets a value indicating whether to hide the control if the <see cref="Text"/> is null or whitespace.
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
            base.OnApplyTemplate(attrs, defStyleAttr, defStyleRes);

            this.HeaderTextBlockContainer = this.View?.FindViewById<LinearLayout>(
                Controls.HeaderedTextBlock.Resource.Id.HeaderedTextBlock_LayoutContainer);

            this.HeaderContent = this.View?.FindViewById<TextView>(
                Controls.HeaderedTextBlock.Resource.Id.HeaderedTextBlock_HeaderTextView);

            this.TextContent = this.View?.FindViewById<TextView>(
                Controls.HeaderedTextBlock.Resource.Id.HeaderedTextBlock_ContentTextView);

            if (attrs != null)
            {
                TypedArray typedArray = this.Context.ObtainStyledAttributes(
                    attrs,
                    Controls.HeaderedTextBlock.Resource.Styleable.HeaderedTextBlock,
                    defStyleAttr,
                    defStyleRes);

                this.hideIfNullOrWhiteSpace = typedArray.GetBoolean(
                    Controls.HeaderedTextBlock.Resource.Styleable.HeaderedTextBlock_hide_if_empty,
                    false);

                if (this.HeaderContent != null)
                {
                    string header = typedArray.GetString(
                        Controls.HeaderedTextBlock.Resource.Styleable.HeaderedTextBlock_header);
                    this.HeaderContent.Text = header;
                }

                if (this.TextContent != null)
                {
                    string text = typedArray.GetString(
                        Controls.HeaderedTextBlock.Resource.Styleable.HeaderedTextBlock_text);
                    this.TextContent.Text = text;
                }
            }

            this.UpdateVisibility();
            this.UpdateOrientation();
        }

        /// <summary>
        /// Updates the layout for the control based on the current <see cref="Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            this.HeaderTextBlockContainer?.SetOrientation(this.Orientation);
        }

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="Header"/> and <see cref="Text"/> properties.
        /// </summary>
        public void UpdateVisibility()
        {
            if (!this.HideIfNullOrWhiteSpace || !string.IsNullOrWhiteSpace(this.Text))
            {
                this.IsVisible = true;
                this.HeaderContent?.SetVisible(!string.IsNullOrWhiteSpace(this.Header));
                this.TextContent?.SetVisible(!string.IsNullOrWhiteSpace(this.Text));
            }
            else
            {
                this.IsVisible = false;
                this.HeaderContent?.SetVisible(false);
                this.TextContent?.SetVisible(false);
            }
        }
    }
}
#endif