using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Base View Interface for all View Interfaces
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Display a Message to the user
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="closeWindow">Whether to close or not the window after the message</param>
        void DisplayMessage(string message, bool closeWindow);
    }
}
