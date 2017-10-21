// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an base class for UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Android.Content;
    using Android.Graphics.Drawables;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    using MADE.UI.Design;

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
            this.Initialize();
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
            this.Initialize();
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
            this.Initialize(attrs);
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
            this.Initialize(attrs);
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
            this.Initialize(attrs);
        }

        /// <summary>
        /// The event associated with the control being loaded.
        /// </summary>
        public event ControlLoadedEventHandler ControlLoaded;

        /// <summary>
        /// The event associated with a property value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The event associated with a property value changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Gets or sets a value indicating whether the user can interact with the control.
        /// </summary>
        public abstract bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible to the user.
        /// </summary>
        public bool IsVisible
        {
            get => this.Visibility == ViewStates.Visible;
            set
            {
                this.SetVisible(value);
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the reference identifier for the control's layout.
        /// </summary>
        public abstract int LayoutReference { get; }

        /// <summary>
        /// Gets the view associated with the inflated layout.
        /// </summary>
        public View View { get; private set; }

        /// <summary>
        /// Gets or sets a color that provides the background of the control.
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                ColorDrawable colorDrawable = this.Background as ColorDrawable;
                return colorDrawable?.Color ?? Android.Graphics.Color.Transparent;
            }
            set
            {
                this.SetBackgroundColor(value);
                this.RaisePropertyChanged();
            }
        }

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

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        public abstract void OnApplyTemplate(IAttributeSet attrs);

        /// <summary>
        /// Raises the property changed event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name to raise.
        /// </param>
        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the property changed event for the specified property expression.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <typeparam name="T">
        /// The type of property to raise.
        /// </typeparam>
        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler == null)
            {
                return;
            }

            string propertyName = GetPropertyName(propertyExpression);

            if (!string.IsNullOrEmpty(propertyName))
            {
                this.RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Raises the property changing event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name to raise.
        /// </param>
        public virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangingEventHandler handler = this.PropertyChanging;
            handler?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the property changing event for the specified property expression.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <typeparam name="T">
        /// The type of property to raise.
        /// </typeparam>
        public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChangingEventHandler handler = this.PropertyChanging;
            if (handler == null)
            {
                return;
            }
            string propertyName = GetPropertyName(propertyExpression);
            handler(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Verifies that a property name exists for the given control type.
        /// </summary>
        /// <param name="propertyName">
        /// The property name to verify.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the property cannot be found on the associated type.
        /// </exception>
        public void VerifyPropertyName([CallerMemberName] string propertyName = null)
        {
            Type myType = this.GetType();

            if (!string.IsNullOrEmpty(propertyName) && myType.GetProperty(propertyName) == null)
            {
                throw new ArgumentException("The property could not be found.", propertyName);
            }
        }

        /// <summary>
        /// Gets the property name from the specified property expression.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression to get a property name from.
        /// </param>
        /// <typeparam name="T">
        /// The type of property.
        /// </typeparam>
        /// <returns>
        /// Returns the property name.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the property expression is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the property expression is not for a property.
        /// </exception>
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression body))
            {
                throw new ArgumentException(
                    "The body of the property expression cannot be null.",
                    nameof(propertyExpression));
            }

            PropertyInfo property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException(
                    "The body of the property expression is not a property.",
                    nameof(propertyExpression));
            }

            return property.Name;
        }

        /// <summary>
        /// Sets a property value and updates the property change events.
        /// </summary>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <param name="field">
        /// The reference field.
        /// </param>
        /// <param name="newValue">
        /// The new value to update to.
        /// </param>
        /// <typeparam name="T">
        /// The type of the property.
        /// </typeparam>
        /// <returns>
        /// Returns a value indicating whether the set was successful.
        /// </returns>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            this.RaisePropertyChanging(propertyExpression);
            field = newValue;
            this.RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        /// Sets a property value and updates the property change events.
        /// </summary>
        /// <param name="propertyName">
        /// The property name to update.
        /// </param>
        /// <param name="field">
        /// The reference field.
        /// </param>
        /// <param name="newValue">
        /// The new value to update to.
        /// </param>
        /// <typeparam name="T">
        /// The type of the property.
        /// </typeparam>
        /// <returns>
        /// Returns a value indicating whether the set was successful.
        /// </returns>
        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            this.RaisePropertyChanging(propertyName);
            field = newValue;
            this.RaisePropertyChanged(propertyName);

            return true;
        }

        private void Initialize()
        {
            this.Initialize(null);
        }

        private void Initialize(IAttributeSet attrs)
        {
            this.View = View.Inflate(this.Context, this.LayoutReference, this);
            this.OnApplyTemplate(attrs);

            ControlLoadedEventHandler handler = this.ControlLoaded;
            handler?.Invoke(this, new ControlLoadedEventArgs());
        }
    }
}