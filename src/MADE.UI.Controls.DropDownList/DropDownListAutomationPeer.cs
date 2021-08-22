// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System.Linq;
    using Windows.UI.Xaml.Automation.Peers;
    using Windows.UI.Xaml.Automation.Provider;
    using Windows.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// Defines a framework element automation peer for the <see cref="DropDownList"/> control.
    /// </summary>
    public class DropDownListAutomationPeer : FrameworkElementAutomationPeer, ISelectionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownListAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="DropDownList" /> that is associated with this <see cref="AutomationPeer"/>.
        /// </param>
        public DropDownListAutomationPeer(DropDownList owner)
            : base(owner)
        {
        }

        /// <summary>Gets a value indicating whether the Microsoft UI Automation provider allows more than one child element to be selected concurrently.</summary>
        /// <returns>True if multiple selection is allowed; otherwise, false.</returns>
        public bool CanSelectMultiple => this.OwningDropDownList != null &&
                                         this.OwningDropDownList.SelectionMode == DropDownListSelectionMode.Multiple;

        /// <summary>Gets a value indicating whether the UI Automation provider requires at least one child element to be selected.</summary>
        /// <returns>False.</returns>
        public bool IsSelectionRequired => false;

        private DropDownList OwningDropDownList => this.Owner as DropDownList;

        /// <summary>Retrieves a UI Automation provider for each child element that is selected.</summary>
        /// <returns>An array of UI Automation providers.</returns>
        public IRawElementProviderSimple[] GetSelection()
        {
            if (this.OwningDropDownList.SelectionMode == DropDownListSelectionMode.Single)
            {
                return this.OwningDropDownList.DropDownContent.ContainerFromItem(this.OwningDropDownList.SelectedItem)
                    is SelectorItem selectorItem
                    ? new[] { this.ProviderFromPeer(FromElement(selectorItem)) }
                    : new IRawElementProviderSimple[] { };
            }

            return this.OwningDropDownList.SelectedItems
                .Select(item => this.OwningDropDownList.DropDownContent.ContainerFromItem(item) as SelectorItem)
                .Select(selectorItem => this.ProviderFromPeer(FromElement(selectorItem)))
                .ToArray();
        }

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ComboBox;
        }

        /// <summary>
        /// Provides the type name of the <see cref="DropDownList"/> when a Microsoft UI Automation client calls GetClassName or an equivalent Microsoft UI Automation client API.
        /// </summary>
        /// <returns>The type name of the <see cref="DropDownList"/>.</returns>
        protected override string GetClassNameCore()
        {
            return this.Owner.GetType().Name;
        }

        /// <summary>
        /// Provides the name given to the <see cref="DropDownList"/> when a Microsoft UI Automation client calls GetName or an equivalent Microsoft UI Automation client API.
        /// </summary>
        /// <returns>The name of the <see cref="DropDownList"/>.</returns>
        protected override string GetNameCore()
        {
            string name = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                name = base.GetNameCore();
            }

            if (this.OwningDropDownList != null)
            {
                name = this.OwningDropDownList.Name;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = this.GetClassName();
            }

            return name;
        }

        /// <summary>
        /// Provides the interaction patterns associated with the <see cref="DropDownList"/> when a Microsoft UI Automation client calls GetPattern or an equivalent Microsoft UI Automation client API.
        /// </summary>
        /// <param name="patternInterface">A value from the PatternInterface enumeration.</param>
        /// <returns>This if it supports the pattern interface; otherwise, null.</returns>
        protected override object GetPatternCore(PatternInterface patternInterface)
        {
            switch (patternInterface)
            {
                case PatternInterface.Selection:
                    return this;
            }

            return base.GetPatternCore(patternInterface);
        }
    }
}