// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;

    /// <summary>
    /// Defines a data validator for ensuring a value is within a minimum and maximum range.
    /// </summary>
    public class BetweenValidator : IValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidator"/> class.
        /// </summary>
        public BetweenValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidator"/> class with a minimum and maximum range.
        /// </summary>
        /// <param name="min">The minimum value within the range.</param>
        /// <param name="max">The maximum value within the range.</param>
        public BetweenValidator(IComparable min, IComparable max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the minimum value within the range.
        /// </summary>
        public IComparable Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value within the range.
        /// </summary>
        public IComparable Max { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = true;

            if (value is IComparable comparable)
            {
                isInvalid = IsLessThan(comparable, this.Min) || IsGreaterThan(comparable, this.Max);
            }

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }

        private static bool IsGreaterThan<T>(T value, T other)
            where T : IComparable
        {
            return value.CompareTo(other) > 0;
        }

        private static bool IsLessThan<T>(T value, T other)
            where T : IComparable
        {
            return value.CompareTo(other) < 0;
        }
    }
}