// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element for creating custom controls in Android applications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Views.Controls
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

    using MADE.App.Design.Color;
    using MADE.App.Views.Extensions;

    /// <summary>
    /// Defines a UI element for creating custom controls in Android applications.
    /// </summary>
    public class Control : FrameLayout, IControl
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
        public Control(Context context)
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
        public Control(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Initialize(attrs, 0, 0);
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
        public Control(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            this.Initialize(attrs, defStyleAttr, 0);
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
        public Control(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
            this.Initialize(attrs, defStyleAttr, defStyleRes);
        }

        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        public event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Gets the view associated with the inflated layout.
        /// </summary>
        public View View { get; private set; }

        /// <summary>
        /// Gets the ID for the Android layout associated with the view.
        /// </summary>
        public virtual int LayoutId { get; } = 0;

        /// <summary>
        /// Gets or sets a color that provides the background of the view.
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
        /// Gets or sets a value indicating whether the view is enabled and can be interacted with.
        /// </summary>
        public virtual bool IsEnabled
        {
            get => this.Enabled;
            set
            {
                if (value != this.Enabled)
                {
                    this.Enabled = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
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
        public virtual void OnApplyTemplate(IAttributeSet attrs, int defStyleAttr, int defStyleRes)
        {
        }

        /// <summary>
        /// Retrieves the element from the instantiated view by the given resource identifier.
        /// </summary>
        /// <param name="resourceId">
        /// The view's resource identifier.
        /// </param>
        /// <typeparam name="TView">
        /// The type of view to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the view from the layout, if the view is found.
        /// </returns>
        public TView GetChildView<TView>(int resourceId)
            where TView : View
        {
            return this.View?.FindViewById<TView>(resourceId);
        }

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
            this.Initialize(null, -1, -1);
        }

        private void Initialize(IAttributeSet attrs, int defStyleAttr, int defStyleRes)
        {
            this.View = View.Inflate(this.Context, this.LayoutId, this);
            this.OnApplyTemplate(attrs, defStyleAttr, defStyleRes);

            ViewLoadedEventHandler handler = this.ViewLoaded;
            handler?.Invoke(this, new ViewLoadedEventArgs());
        }
    }
}
#endif