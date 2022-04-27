// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;

    /// <summary>
    /// Defines a list of <see cref="FluentValidation.IValidator"/> objects that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of item being validated.</typeparam>
    public class FluentValidatorCollection<T> : List<IValidator<T>>, IValidatorCollection
    {
        private readonly List<string> feedbackMessages = new();

        /// <summary>Initializes a new instance of the <see cref="FluentValidatorCollection{T}"/> class that is empty and has the default initial capacity.</summary>
        public FluentValidatorCollection()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FluentValidatorCollection{T}"/> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection">collection</paramref> is null.</exception>
        public FluentValidatorCollection(IEnumerable<IValidator<T>> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidatorCollection{T}"/> class that is empty and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity">capacity</paramref> is less than 0.</exception>
        public FluentValidatorCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Occurs when the input value is validated against the collection of validators.
        /// </summary>
        public event InputValidatedEventHandler Validated;

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets the validator feedback messages for ones which are invalid.
        /// </summary>
        public IEnumerable<string> FeedbackMessages => this.feedbackMessages;

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/> against the validators provided.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="Exception">Potentially thrown by the <see cref="Validated"/> delegate callback.</exception>
        public void Validate(object value)
        {
            this.feedbackMessages.Clear();

            this.IsDirty = true;

            this.ForEach(validator =>
            {
                var result = validator.Validate((T)value);
                if (!result.IsValid)
                {
                    IsInvalid = true;
                }

                if (result.Errors != null)
                {
                    foreach (var message in result.Errors)
                    {
                        this.feedbackMessages.Add(message.ErrorMessage);
                    }
                }
            });

            this.Validated?.Invoke(this, new InputValidatedEventArgs(this.IsInvalid, this.IsDirty));
        }
    }
}