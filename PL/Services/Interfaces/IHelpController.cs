using ConsoleShop.Controller.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with Commands
    /// </summary>
    public interface IHelpController : IController
    {
        /// <summary>
        /// Returns all available commands
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult ViewCommands();

        /// <summary>
        /// Return requested command
        /// </summary>
        /// <param name="cmd">The name of the requested command</param>
        /// <returns>Response object</returns>
        public IActionResult ShowCommand(string cmd);
    }
}
