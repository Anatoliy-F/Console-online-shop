using ConsoleShop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Services
{
    /// <summary>
    /// Сlass that reads the request and selects the command to execute
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// List of available commands
        /// </summary>
        private readonly IEnumerable<ICommand> commands;

        /// <summary>
        /// Initialize new instance of Requesthelper
        /// </summary>
        /// <param name="commands">List of available commands</param>
        public RequestHelper(IEnumerable<ICommand> commands)
        {
            this.commands = commands;
        }

        /// <summary>
        /// Read request and selects the command to execute
        /// </summary>
        /// <param name="line">String entered by the user in the console</param>
        /// <returns>ICommand object, string arguments</returns>
        public (ICommand, string) ReadRequest(string line)
        {
            ICommand command = null;
            string commandArgs = String.Empty;
            if (line != String.Empty)
            {
                string[] args = line.Split(' ');
                string commandString = args[0].ToLower();
                command = commands.FirstOrDefault(c => c.Name.ToLower() == commandString);

                if (args.Length > 1)
                {
                    commandArgs = line.Remove(0, args[0].Length + 1);
                }
            }

            if (command == null || command.Name == String.Empty)
            {
                command = commands.First(c => c.Name == String.Empty);
                commandArgs = "Unknow command, please use help";
            }
            
            return (command, commandArgs);
        }

    }
}
