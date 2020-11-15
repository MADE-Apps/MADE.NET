// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a generic regular expression data validator.
    /// </summary>
    public class RegexValidator : IValidator
    {
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
        /// Gets or sets the RegEx pattern to match on.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            string str = value?.ToString() ?? string.Empty;
            this.IsInvalid = !Regex.IsMatch(str, this.Pattern);
            this.IsDirty = true;
        }
    }
}
