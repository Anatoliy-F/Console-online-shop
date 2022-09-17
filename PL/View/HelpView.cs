using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    public class HelpView : BaseView
    {
        private readonly IEnumerable<ICommand> _commands;

        /// <summary>
        /// Initialize new instance of HelpView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public HelpView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _commands = (IEnumerable<ICommand>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var column1 = this.MakeColumn(_commands.Select(c => c.Name));

            int padding = column1.First().Length;
            List<string> desc = new List<string>();
            List<string> pattern = new List<string>();
            foreach(var command in _commands)
            {
                var args = command.Description?.Split("Pattern:");
                desc.Add(args?[0] ?? "");
                string pad = new string(' ', padding);
                pattern.Add(args?.Length > 1 ? "\t" + pad + args[1] : "\t" + pad);
            }
            var column2 = this.MakeColumn(desc);
            for (int i = 0; i < column1.Count; i++)
            {
                this.PrintColorizedDefinition(column1[i], column2[i], ConsoleColor.Magenta);
                this.PrintColorizedMessage(pattern[i], ConsoleColor.Yellow);
            }
        }
    }
}
