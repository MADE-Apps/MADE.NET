// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogButtonType.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines values associated with the type of a dialog button.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Dialogs.Buttons
{
    /// <summary>
    /// Defines values associated with the type of a dialog button.
    /// </summary>
    public enum DialogButtonType
    {
        /// <summary>
        /// The button is neutral and can be used to perform any action.
        /// </summary>
        Neutral,

        /// <summary>
        /// The button is positive and can be used to perform positive actions, such as saving.
        /// </summary>
        Positive,

        /// <summary>
        /// The button is negative and can be used to perform negative actions, such as cancelling.
        /// </summary>
        Negative
    }
}