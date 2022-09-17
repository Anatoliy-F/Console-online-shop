using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Commands.Base
{
    /// <summary>
    /// Defines properties and methods of Command objects
    /// </summary>
    public interface ICommand : IEntity
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Command description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// List of user roles for which the command is available for execution
        /// </summary>
        IEnumerable<UserRole> Roles { get; }

        /// <summary>
        /// User request execution
        /// </summary>
        /// <param name="user">User object for check execute permissions</param>
        /// <param name="args">Request arguments</param>
        /// <returns>Response object</returns>
        public IActionResult Execute(User user, string args);
    }
}
