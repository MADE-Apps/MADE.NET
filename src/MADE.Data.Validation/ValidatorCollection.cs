// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MADE.Data.Validation.Extensions;

    /// <summary>
    /// Defines a list of <see cref="IValidator"/> objects that can be accessed by index.
    /// </summary>
    public class ValidatorCollection : List<IValidator>, IValidatorCollection
    {
        /// <summary>Initializes a new instance of the <see cref="ValidatorCollection"/> class that is empty and has the default initial capacity.</summary>
        public ValidatorCollection()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ValidatorCollection"/> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection">collection</paramref> is null.</exception>
        public ValidatorCollection(IEnumerable<IValidator> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorCollection"/> class that is empty and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity">capacity</paramref> is less than 0.</exception>
        public ValidatorCollection(int capacity)
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
        public bool IsInvalid
        {
            get => this.Any(validator => validator.IsInvalid);
            set => this.ForEach(validator => validator.IsInvalid = value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty
        {
            get => this.Any(validator => validator.IsDirty);
            set => this.ForEach(validator => validator.IsDirty = value);
        }

        /// <summary>
        /// Gets the validator feedback messages for ones which are invalid.
        /// </summary>
        public IEnumerable<string> FeedbackMessages => this.Where(x => x.IsInvalid).Select(x => x.FeedbackMessage).Where(x => !x.IsNullOrWhiteSpace());

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/> against the validators provided.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="Exception">Potentially thrown by the <see cref="Validated"/> delegate callback.</exception>
        public void Validate(object value)
        {
            this.ForEach(validator => validator.Validate(value));
            this.Validated?.Invoke(this, new InputValidatedEventArgs(this.IsInvalid, this.IsDirty));
        }
    }
}