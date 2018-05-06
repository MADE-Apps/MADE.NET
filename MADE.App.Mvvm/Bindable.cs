// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bindable.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an abstract class for creating bindable objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines an abstract class for creating bindable objects.
    /// </summary>
    public abstract class Bindable : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event for the given property value.
        /// </summary>
        /// <param name="propertyExpression">
        /// The expression containing the property to raise as changed.
        /// </param>
        /// <typeparam name="T">
        /// The type of property.
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
        /// Sets the value of the given property value and raises the property changed event if the value is different to the previously set value.
        /// </summary>
        /// <param name="propertyExpression">
        /// The expression containing the property to raise as changed.
        /// </param>
        /// <param name="field">
        /// The reference field for the property.
        /// </param>
        /// <param name="value">
        /// The value to update the property with.
        /// </param>
        /// <typeparam name="T">
        /// The type of property.
        /// </typeparam>
        /// <returns>
        /// Returns true if the property was updated; else returns false.
        /// </returns>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            this.RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        /// Raises the property changed event for the given property name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to raise as changed.
        /// </param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (object.Equals(propertyExpression, null))
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression body))
            {
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));
            }

            if (!(body.Member is PropertyInfo property))
            {
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
            }

            return property.Name;
        }
    }
}