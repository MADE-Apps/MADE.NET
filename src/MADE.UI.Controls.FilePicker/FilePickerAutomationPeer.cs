// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using Windows.UI.Xaml.Automation.Peers;

    /// <summary>
    /// Defines a framework element automation peer for the <see cref="FilePicker"/> control.
    /// </summary>
    public class FilePickerAutomationPeer : FrameworkElementAutomationPeer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePickerAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="FilePicker" /> that is associated with this <see cref="AutomationPeer"/>.
        /// </param>
        public FilePickerAutomationPeer(FilePicker owner)
            : base(owner)
        {
        }

        private FilePicker OwningFilePicker => this.Owner as FilePicker;

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
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

            if (this.OwningFilePicker != null)
            {
                name = this.OwningFilePicker.Name;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = this.GetClassName();
            }

            return name;
        }

        protected override object GetPatternCore(PatternInterface patternInterface)
        {
            return base.GetPatternCore(patternInterface);
        }
    }
}