using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class HelpController : IHelpController
    {
        /// <summary>
        /// List of available commands
        /// </summary>
        private readonly IEnumerable<ICommand> _commands;
        /// <summary>
        /// An object that provide session functions
        /// </summary>
        private readonly ISession _session;
        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of HelpController
        /// </summary>
        /// <param name="commands">List of available commands</param>
        /// <param name="session">An object that provide session functions</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public HelpController(IEnumerable<ICommand> commands, ISession session, IActionResultFactory actionResultFactory)
        {
            _commands = commands.Where(c => c.Name != String.Empty);
            _session = session;
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult ViewCommands()
        {
            var commands = _commands.Where(c => c.Roles.Contains(_session.User.Role));
            return _actionResultFactory.GetResultRender(
                ActionResult.Succes,
                "List Of Accessible Commands",
                commands
                );
        }

        /// <inheritdoc/>
        public IActionResult ShowCommand(string cmd)
        {
            var commands = _commands.Where(c => c.Roles.Contains(_session.User.Role));
            if (commands.Any(c => c.Name.ToLower() == cmd.ToLower()))
            {
                return _actionResultFactory.GetResultRender(
                    ActionResult.Succes,
                    $"{cmd} description: ",
                    commands.Where(c => c.Name.ToLower() == cmd.ToLower())
                    );
            }
            else
            {
                return _actionResultFactory.GetResultRender(
                    ActionResult.Warning,
                    $"No such command {cmd}, use [Help]"
                    );
            }
        }
    }
}
