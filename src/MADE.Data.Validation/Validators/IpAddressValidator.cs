// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a data validator for ensuring a value is a valid IP address.
    /// </summary>
    public class IpAddressValidator : IValidator
    {
        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(IpAddressValidator);

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            string str = value?.ToString() ?? string.Empty;

            string[] nibbles = str.Split('.');
            this.IsInvalid = nibbles.Length != 4 || !nibbles.All(IsNibbleValid);
            this.IsDirty = true;
        }

        private static bool IsNibbleValid(string nibble)
        {
            if (nibble.Length > 3 || nibble.Length == 0)
            {
                return false;
            }

            if (nibble[0] == '0' && nibble != "0")
            {
                return false;
            }

            if (!Regex.IsMatch(nibble, @"^\d+$"))
            {
                return false;
            }

            int.TryParse(nibble, out int numeric);
            return numeric >= 0 && numeric <= 255;
        }
    }
}