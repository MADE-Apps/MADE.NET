// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a base class for UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Foundation;

    using UIKit;

    /// <summary>
    /// Defines a base class for UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public abstract class Control : UIView, IIOSControl, IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        protected Control(IntPtr handle)
            : base(handle)
        {
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
        /// Represents the method that handles the <see cref="Disposed" /> event of a component.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets or sets the <see cref="ISite"/> associated with the <see cref="IComponent" />.
        /// </summary>
        public ISite Site { get; set; }

        /// <summary>
        /// Gets the name of the nib to load.
        /// </summary>
        public abstract string NibName { get; }

        /// <summary>
        /// Gets or sets the root view.
        /// </summary>
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        public UIView RootView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can interact with the control.
        /// </summary>
        [Export("IsEnabled"), Browsable(true)]
        public bool IsEnabled
        {
            get => this.UserInteractionEnabled;
            set
            {
                this.UserInteractionEnabled = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible to the user.
        /// </summary>
        [Export("IsVisible"), Browsable(true)]
        public bool IsVisible
        {
            get => this.IsVisible();
            set
            {
                this.SetVisible(value);
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Called after the object has been loaded from the nib file. Overriders must call base.AwakeFromNib().
        /// </summary>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            if (this.Site != null && this.Site.DesignMode)
            {
                return;
            }

            this.Initialize();
        }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        public abstract void OnApplyTemplate();

        /// <summary>
        /// Cleans up the designer outlets.
        /// </summary>
        public virtual void ReleaseDesignerOutlets()
        {
            if (this.RootView != null)
            {
                this.RootView.Dispose();
                this.RootView = null;
            }
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
            NSBundle.MainBundle.LoadNib(this.NibName, this, null);

            if (this.RootView == null)
            {
                return;
            }

            this.AddSubview(this.RootView);

            this.OnApplyTemplate();

            ControlLoadedEventHandler handler = this.ControlLoaded;
            handler?.Invoke(this, new ControlLoadedEventArgs());
        }
    }
}