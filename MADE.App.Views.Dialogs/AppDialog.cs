// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDialog.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a service for handling application system alert dialogs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Dialogs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.App.Views.Dialogs.Buttons;
    using MADE.App.Views.Threading;

    /// <summary>
    /// Defines a service for handling application system alert dialogs.
    /// </summary>
    public class AppDialog : IAppDialog, IDisposable
    {
        private readonly IUIDispatcher dispatcher;

        private SemaphoreSlim dialogSemaphore;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDialog"/> class.
        /// </summary>
        /// <param name="dispatcher">
        /// The dispatcher for launching dialogs on the UI thread.
        /// </param>
        public AppDialog(IUIDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;

            this.dialogSemaphore = new SemaphoreSlim(1, 1);
        }

        /// <summary>
        /// Shows an application system alert dialog with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        public void Show(string message)
        {
            this.ShowAsync(string.Empty, message, default(Action), null).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows an application system alert dialog with the specified message and dialog buttons.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="buttons">
        /// The button definitions for performing actions.
        /// </param>
        public void Show(string message, params DialogButton[] buttons)
        {
            this.ShowAsync(string.Empty, message, default(Action), buttons).ConfigureAwait(false);
        }

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
        public void Show(string message, Action cancelAction, params DialogButton[] buttons)
        {
            this.ShowAsync(string.Empty, message, cancelAction, buttons).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows an application system alert dialog with the specified title and message.
        /// </summary>
        /// <param name="title">
        /// The title to display.
        /// </param>
        /// <param name="message">
        /// The message to display.
        /// </param>
        public void Show(string title, string message)
        {
            this.ShowAsync(title, message, default(Action), null).ConfigureAwait(false);
        }

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
        public void Show(string title, string message, params DialogButton[] buttons)
        {
            this.ShowAsync(title, message, default(Action), buttons).ConfigureAwait(false);
        }

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
        public void Show(string title, string message, Action cancelAction, params DialogButton[] buttons)
        {
            this.ShowAsync(title, message, cancelAction, buttons).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows an application system alert dialog with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        public Task ShowAsync(string message)
        {
            return this.ShowAsync(string.Empty, message, default(Action), null);
        }

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
        public Task ShowAsync(string message, params DialogButton[] buttons)
        {
            return this.ShowAsync(string.Empty, message, default(Action), buttons);
        }

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
        public Task ShowAsync(string message, Action cancelAction, params DialogButton[] buttons)
        {
            return this.ShowAsync(string.Empty, message, cancelAction, buttons);
        }

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
        public Task ShowAsync(string title, string message)
        {
            return this.ShowAsync(title, message, default(Action), null);
        }

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
        public Task ShowAsync(string title, string message, params DialogButton[] buttons)
        {
            return this.ShowAsync(title, message, default(Action), buttons);
        }

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
        public async Task ShowAsync(string title, string message, Action cancelAction, params DialogButton[] buttons)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

#if __ANDROID__ || __IOS__ || WINDOWS_UWP
            if (this.dispatcher != null)
            {
                await this.dispatcher.RunAsync(
                    async () =>
                        {
                            await this.dialogSemaphore.WaitAsync();

                            try
                            {
                                XPlat.UI.Popups.MessageDialog dialog =
                                    new XPlat.UI.Popups.MessageDialog(message)
                                        {
                                            Title = string.IsNullOrWhiteSpace(title)
                                                        ? "System alert!"
                                                        : title
                                        };

                                if (buttons != null)
                                {
                                    foreach (DialogButton button in buttons)
                                    {
                                        dialog.Commands.Add(new XPlat.UI.Popups.UICommand(button.Content, command => button.Invoke()));
                                    }
                                }

                                XPlat.UI.Popups.IUICommand result = await dialog.ShowAsync();

                                if (result == null)
                                {
                                    cancelAction?.Invoke();
                                }

                                tcs.SetResult(true);
                            }
                            catch (Exception ex)
                            {
                                tcs.SetException(ex);
                            }
                            finally
                            {
                                this.dialogSemaphore.Release();
                            }
                        });
            }
            else
            {
                tcs.SetResult(false);
            }
#else
            tcs.SetException(new PlatformNotSupportedException("The target platform being used is not currently supported."));
#endif

            await tcs.Task;
        }

        /// <summary>
        /// Disposes of the disposable components.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the disposable components.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether the object is disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.dialogSemaphore == null)
            {
                return;
            }

            this.dialogSemaphore.Dispose();
            this.dialogSemaphore = null;
        }
    }
}