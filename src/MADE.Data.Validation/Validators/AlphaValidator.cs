// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    /// <summary>
    /// Defines a data validator for ensuring a value contains alpha characters.
    /// </summary>
    public class AlphaValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaValidator"/> class with the expected RegEx pattern.
        /// </summary>
        public AlphaValidator()
        {
            this.Key = nameof(AlphaValidator);
            this.Pattern = "^[a-zA-Z]*$";
        }
    }
}