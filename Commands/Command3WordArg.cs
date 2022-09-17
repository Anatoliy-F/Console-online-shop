using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;


namespace ConsoleShop.Commands
{
    /// <summary>
    /// Command that takes three words (string without whitespaces) arguments to execute
    /// </summary>
    public class Command3WordArg : BaseCommand
    {
        /// <summary>
        /// Delegate for execution
        /// </summary>
        private readonly Func<string, string, string, IActionResult> _executor;

        /// <summary>
        /// String without whitespaces
        /// </summary>
        private string _arg1, _arg2, _arg3;

        /// <summary>
        /// Command class constructor
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Description of the result of the command execution</param>
        /// <param name="roles">List of user roles for which the command is available for execution</param>
        /// <param name="executor">Delegate to be executed when the command is invoked</param>
        public Command3WordArg(string name,
           string description,
           IEnumerable<UserRole> roles,
           Func<string, string, string, IActionResult> executor) : base(name, description, roles)
        {
            _executor = executor;
        }

        /// <summary>
        /// Delegate execution
        /// </summary>
        /// <returns>Response object <see cref="ConsoleShop.Controller.Base.IActionResult"/></returns>
        protected override IActionResult ExecuteDelegate()
        {
            return _executor(_arg1, _arg2, _arg3);
        }

        /// <summary>
        /// Read and validate that request arguments is three words (string without whitespaces)
        /// </summary>
        /// <param name="args">Request arguments</param>
        /// <returns>True, if arguments correct</returns>
        protected override bool ReadArgs(string args)
        {
            _arg3 = _arg2 = _arg1 = String.Empty;

            string[] argsArr = args.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (argsArr.Length != 3 || argsArr[0] == String.Empty || argsArr[1] == String.Empty || argsArr[2] == String.Empty)
            {
                _argsErrorMessage = "You should transmit 3 non-empty word!";
                return false;
            }
            _arg1 = argsArr[0];
            _arg2 = argsArr[1];
            _arg3 = argsArr[2];
            return true;
        }
    }
}
