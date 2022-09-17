using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Commands.Base
{
    /// <summary>
    /// Base Command class, defines method for checking permissions
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string Description { get; private set; }

        /// <summary>
        /// Factory that instantiate Error response, set once for all Command instances
        /// </summary>
        public static IActionResultFactory ErrorViewFactory { get; set; }

        /// <inheritdoc/>
        public IEnumerable<UserRole> Roles { get; private set; }

        /// <summary>
        /// Base access error message
        /// </summary>
        private string _permissionErrorMsg = "Access dinied, because you don't expected permission level";

        /// <summary>
        /// Base arguments validation error message
        /// </summary>
        protected string _argsErrorMessage  = "You specified invalid args, please check help command";

        /// <summary>
        /// Base class constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description of the result of the command execution</param>
        /// <param name="roles">List of user roles for which the command is available for execution</param>
        protected BaseCommand(string name, string description, IEnumerable<UserRole> roles)
        {
            Name = name;
            Description = description;
            Roles = roles;
        }

        /// <inheritdoc/>
        public IActionResult Execute(User user, string args)
        {
            if (HasPermission(user))
            {
                if(ReadArgs(args))
                {
                    return ExecuteDelegate();
                }
                else
                {
                    return ErrorViewFactory.GetResultRender(ActionResult.Error, _argsErrorMessage);
                }
            }
            else
            {
                return ErrorViewFactory.GetResultRender(ActionResult.Error, _permissionErrorMsg);
            }
        }

        /// <summary>
        /// Method executed by the command after checking the access rights and command arguments
        /// </summary>
        /// <returns>Response object</returns>
        protected abstract IActionResult ExecuteDelegate();

        /// <summary>
        /// Reads and validates request arguments
        /// </summary>
        /// <param name="args">Request arguments</param>
        /// <returns>True, if arguments correct</returns>
        protected abstract bool ReadArgs(string args);

        /// <summary>
        /// Check user permissions
        /// </summary>
        /// <param name="user">User object for check execute permissions</param>
        /// <returns>True, if user has permissions</returns>
        private bool HasPermission(User user)
        {
            if (Roles.Contains(user.Role))
            {
                return true;
            }
            else
            {
                string roles = "[ " + String.Join(" | ", this.Roles) + " ]";
                string message = $"Access Denied! You should have one of this {roles} permissions";
                if (Roles.Contains(UserRole.RegisteredUser))
                {
                    message += "\n\tLogin or Register to provide access for this functionality";
                }
                _permissionErrorMsg = message;
                return false;
            }
        }

    }
}
