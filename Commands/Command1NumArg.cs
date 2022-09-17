using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;

namespace ConsoleShop.Commands
{
    /// <summary>
    /// Command that takes one integer argument to execute
    /// </summary>
    public class Command1NumArg : BaseCommand
    {
        /// <summary>
        /// Delegate for execution
        /// </summary>
        private readonly Func<int, IActionResult> _executor;
        /// <summary>
        /// Number argument
        /// </summary>
        private int _arg;

        /// <summary>
        /// Command class constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description of the result of the command execution</param>
        /// <param name="roles">List of user roles for which the command is available for execution</param>
        /// <param name="executor">Delegate to be executed when the command is invoked</param>
        public Command1NumArg(string name,
            string description,
            IEnumerable<UserRole> roles,
            Func<int, IActionResult> executor) : base(name, description, roles)
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
        /// Read and validate that request arguments is one integer number
        /// </summary>
        /// <param name="args">Request arguments</param>
        /// <returns>True, if arguments correct</returns>
        protected override bool ReadArgs(string args)
        {
            _arg = 0;
            args = args.Trim();
            string[] argsArr = args.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (argsArr.Length != 1)
            {
                _argsErrorMessage = "You should specify 1 int arguments";
                return false;
            }
            if (!Int32.TryParse(argsArr[0], out _arg))
            {
                _argsErrorMessage = "You argument should be integer";
                return false;
            }

            return true;
        }
    }
}
