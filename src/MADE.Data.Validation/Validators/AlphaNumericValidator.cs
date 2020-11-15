// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    /// <summary>
    /// Defines a data validator for ensuring a value contains alphanumeric characters.
    /// </summary>
    public class AlphaNumericValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaNumericValidator"/> class with the expected RegEx pattern.
        /// </summary>
        public AlphaNumericValidator()
        {
            this.Key = nameof(AlphaNumericValidator);
            this.Pattern = "^[a-zA-Z0-9]*$";
        }
    }
}
