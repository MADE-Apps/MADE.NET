// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using MADE.Data.Validation.Extensions;

    public class MinValueValidator : IValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinValueValidator"/> class.
        /// </summary>
        public MinValueValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinValueValidator"/> class with a minimum value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        public MinValueValidator(IComparable min)
        {
            this.Min = min;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(MinValueValidator);

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public IComparable Min { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = true;

            if (value is IComparable comparable)
            {
                isInvalid = comparable.IsLessThan(this.Min);
            }

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}
