// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand{T}.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an implementation of the ICommand for running an action on execution.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Mvvm
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Defines an implementation of the <see cref="ICommand"/> for running an action on execution.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item passed to the command.
    /// </typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> executeAction;

        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with an action.
        /// </summary>
        /// <param name="executeAction">
        /// The action to execute when the command is fired.
        /// </param>
        public RelayCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with an action and a function to check whether the action can execute.
        /// </summary>
        /// <param name="executeAction">
        /// The action to execute when the command is fired.
        /// </param>
        /// <param name="canExecute">
        /// The function to check whether the action can be executed.
        /// </param>
        public RelayCommand(Action<T> executeAction, Func<T, bool> canExecute)
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecute = canExecute ?? (e => true);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>
        /// Returns true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            try
            {
                return this.canExecute(ConvertParameterValue(parameter));
            }
            catch (FormatException)
            {
                // Thrown if the parameter cannot be converted correctly.
            }

            return false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                return;
            }

            this.executeAction(ConvertParameterValue(parameter));
        }

        private static T ConvertParameterValue(object parameter)
        {
            parameter = parameter is T ? parameter : Convert.ChangeType(parameter, typeof(T));
            return (T)parameter;
        }
    }
}