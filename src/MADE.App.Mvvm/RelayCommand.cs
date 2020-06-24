// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="MADE Apps">
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
    public class RelayCommand : ICommand
    {
        private readonly Action executeAction;

        private readonly Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with an action.
        /// </summary>
        /// <param name="executeAction">
        /// The action to execute when the command is fired.
        /// </param>
        public RelayCommand(Action executeAction)
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
        public RelayCommand(Action executeAction, Func<bool> canExecute)
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecute = canExecute ?? (() => true);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// Returns true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute()
        {
            return this.CanExecute(null);
        }

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
            return this.canExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute()
        {
            this.Execute(null);
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

            this.executeAction();
        }
    }
}