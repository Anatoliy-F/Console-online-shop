using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;

namespace ConsoleShop.Commands
{
    /// <summary>
    /// Command that takes two integer arguments to execute
    /// </summary>
    public class Command2NumArg : BaseCommand
    {
        /// <summary>
        /// Delegate for execution
        /// </summary>
        private readonly Func<int, int, IActionResult> _executor;

        /// <summary>
        /// Number argument
        /// </summary>
        private int _arg1  = 0;

        /// <summary>
        /// Number argument
        /// </summary>
        private int _arg2  = 0;

        /// <summary>
        /// Command class constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description of the result of the command execution</param>
        /// <param name="roles">List of user roles for which the command is available for execution</param>
        /// <param name="executor">Delegate to be executed when the command is invoked</param>
        public Command2NumArg(string name,
           string description,
           IEnumerable<UserRole> roles,
           Func<int, int, IActionResult> executor) : base(name, description, roles)
        {
            _executor = executor;
        }

        /// <summary>
        /// Delegate execution
        /// </summary>
        /// <returns>Response object</returns>
        protected override IActionResult ExecuteDelegate()
        {
            return _executor(_arg1, _arg2);
        }

        /// <summary>
        /// Read and validate that request arguments is two integer number
        /// </summary>
        /// <param name="args">Request arguments</param>
        /// <returns>True, if arguments correct</returns>
        protected override bool ReadArgs(string args)
        {
            _arg2 = _arg1 = 0;

            string[] argsArr = args.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if(argsArr.Length != 2)
            {
                _argsErrorMessage = "You should specify 2 int arguments";
                return false;
            }
            if (!Int32.TryParse(argsArr[0], out _arg1))
            {
                _argsErrorMessage = "You first argument should be integer";
                return false;
            }
            if(!Int32.TryParse(argsArr[1], out _arg2))
            {
                _argsErrorMessage = "You second argument should be integer";
                return false;
            }
            return true;
        }
    }
}
