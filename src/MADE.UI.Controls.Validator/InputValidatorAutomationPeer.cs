// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using Windows.UI.Xaml.Automation.Peers;

    /// <summary>
    /// Defines a framework element automation peer for the <see cref="InputValidator"/> control.
    /// </summary>
    public class InputValidatorAutomationPeer : FrameworkElementAutomationPeer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatorAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="InputValidator" /> that is associated with this <see cref="AutomationPeer"/>.
        /// </param>
        public InputValidatorAutomationPeer(InputValidator owner)
            : base(owner)
        {
        }

        private InputValidator OwningInputValidator => this.Owner as InputValidator;

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Group;
        }

        protected override string GetClassNameCore()
        {
            return this.Owner.GetType().Name;
        }

        protected override string GetNameCore()
        {
            string name = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                name = base.GetNameCore();
            }

            if (this.OwningInputValidator != null)
            {
                name = this.OwningInputValidator.Name;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = this.GetClassName();
            }

            return name;
        }
    }
}