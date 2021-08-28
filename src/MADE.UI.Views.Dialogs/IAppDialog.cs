// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using MADE.UI.Views.Dialogs.Buttons;

    /// <summary>
    /// Defines an interface for handling application system alert dialogs.
    /// </summary>
    public interface IAppDialog
    {
        /// <summary>
        /// Shows an application system alert dialog with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        void Show(string message);

        /// <summary>
        /// Shows an application system alert dialog with the specified message and dialog buttons.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        void Show(string message, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified message, cancellation action and dialog buttons.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="cancelAction">
        /// The action to perform when the dialog has been cancelled/dismissed.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        void Show(string message, Action cancelAction, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified title and message.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        void Show(string title, string message);

        /// <summary>
        /// Shows an application system alert dialog with the specified title, message and dialog buttons.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        void Show(string title, string message, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified title, message, cancellation action and dialog buttons.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="cancelAction">
        /// The action to perform when the dialog has been cancelled/dismissed.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        void Show(string title, string message, Action cancelAction, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string message);

        /// <summary>
        /// Shows an application system alert dialog with the specified message and dialog buttons.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string message, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified message, cancellation action and dialog buttons.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="cancelAction">
        /// The action to perform when the dialog has been cancelled/dismissed.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string message, Action cancelAction, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified title and message.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string title, string message);

        /// <summary>
        /// Shows an application system alert dialog with the specified title, message and dialog buttons.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string title, string message, params DialogButton[] buttons);

        /// <summary>
        /// Shows an application system alert dialog with the specified title, message, cancellation action and dialog buttons.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="cancelAction">
        /// The action to perform when the dialog has been cancelled/dismissed.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task ShowAsync(string title, string message, Action cancelAction, params DialogButton[] buttons);
    }
}