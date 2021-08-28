// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Dialogs.Buttons
{
    /// <summary>
    /// Defines a button to be used within a application system alert dialog.
    /// </summary>
    public class DialogButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogButton"/> class.
        /// </summary>
        /// <param name="type">
        /// The type of button.
        /// </param>
        public DialogButton(DialogButtonType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogButton"/> class with content text.
        /// </summary>
        /// <param name="type">
        /// The type of button.
        /// </param>
        /// <param name="content">
        /// The content text to display on the button.
        /// </param>
        public DialogButton(DialogButtonType type, string content)
        {
            this.Type = type;
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogButton"/> class with content text and an invoke action.
        /// </summary>
        /// <param name="type">
        /// The type of button.
        /// </param>
        /// <param name="content">
        /// The content text to display on the button.
        /// </param>
        /// <param name="invokeAction">
        /// The action to perform when the button is invoked.
        /// </param>
        public DialogButton(DialogButtonType type, string content, DialogButtonInvokedHandler invokeAction)
        {
            this.Type = type;
            this.Content = content;
            this.InvokeAction = invokeAction;
        }

        /// <summary>
        /// Gets the type of button.
        /// </summary>
        public DialogButtonType Type { get; }

        /// <summary>
        /// Gets or sets the content text to display on the button.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the action to perform when the button is invoked.
        /// </summary>
        public DialogButtonInvokedHandler InvokeAction { get; set; }

        /// <summary>
        /// Invokes the specified <see cref="InvokeAction"/>.
        /// </summary>
        public void Invoke()
        {
            this.InvokeAction?.Invoke(this);
        }
    }
}