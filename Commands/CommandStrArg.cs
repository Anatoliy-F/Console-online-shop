using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;


namespace ConsoleShop.Commands
{
    /// <summary>
    /// Command that takes one string argument to execute
    /// </summary>
    public class CommandStrArg : BaseCommand
    {
        /// <summary>
        /// Delegate for execution
        /// </summary>
        private readonly Func<string, IActionResult> _executor;

        /// <summary>
        /// String argument
        /// </summary>
        private string _arg;

        /// <summary>
        /// Command class constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description of the result of the command execution</param>
        /// <param name="roles">List of user roles for which the command is available for execution</param>
        /// <param name="executor">Delegate to be executed when the command is invoked</param>
        public CommandStrArg(string name,
            string description, 
            IEnumerable<UserRole> roles, 
            Func<string, IActionResult> executor) : base(name, description, roles)
        {
            _executor = executor;
        }

        /// <summary>
        /// Delegate execution
        /// </summary>
        /// <returns>Response object <see cref="ConsoleShop.Controller.Base.IActionResult"/></returns>
        protected override IActionResult ExecuteDelegate()
        {
           return _executor(_arg);
        }

        /// <summary>
        /// Read and validate that request arguments is one string
        /// </summary>
        /// <param name="args">Request arguments</param>
        /// <returns>True, if arguments correct</returns>
        protected override bool ReadArgs(string args)
        {
            _arg = String.Empty;
            args = args.Trim();
            if(args.Length == 0)
            {
                _argsErrorMessage = "Argument should be non-empty string";
                return false;
            }

            _arg = args;
            return true;
        }
    }
}
