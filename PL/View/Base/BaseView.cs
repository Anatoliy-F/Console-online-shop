using ConsoleShop.View.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View.Base
{
    /// <summary>
    /// Implement IActionResult, inherits from ConsoleShop.Controller.Base BaseActionResult
    /// for the console user interface
    /// </summary>
    public abstract class BaseView : BaseActionResult
    {
        /// <summary>
        /// Initialize new instance of response object. 
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        protected BaseView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody) { }

        /// <summary>
        /// Render response body in console
        /// </summary>
        protected abstract void RenderBody();

        /// <summary>
        /// Get console window width to prettier output purposes
        /// </summary>
        protected int Width { get 
            {
                try
                {
                    return Console.WindowWidth;
                }
                catch (Exception)
                {
                    return 100;
                }
            } 
        }

        /// <summary>
        /// Render shop header, displays information about the current user
        /// </summary>
        /// <param name="session">Object that persists the state of a user's session between requests</param>
        protected virtual void RenderHeader(ISession session)
        {
            string msg = $"{session.GetUserName()} | Orders: {session.GetOrdersCount()} | Positions in Cart: {session.GetCartLinesCount()} | Total spent: ${session.GetUserTotal()}";

            int padding = (Width - msg.Length - 2) / 2;
            string padd = new string(' ', padding);
            string line = "|" + padd + msg + padd + "|";

            PrintBorderLine('=', false);
            Console.WriteLine(line);
            PrintBorderLine('=', false);
            Console.WriteLine();
        }

        /// <summary>
        /// Render default shop header, before user's login
        /// </summary>
        protected virtual void RenderHeader()
        {
            string msg = "Welcome in Console_E_shop. Login for have access to all functionality";
            int padding = (Width - msg.Length - 2) / 2;
            string padd = new string(' ', padding);
            string line = "|" + padd + msg + padd + "|";

            PrintBorderLine('=', false);
            Console.WriteLine(line);
            PrintBorderLine('=', false);
            Console.WriteLine();
        }

        /// <summary>
        /// Render shop footer
        /// </summary>
        protected virtual void RenderFooter()
        {
            string msg = "Print \"help\" and see available commands. All commands are case-insensitive";

            int padding = (Width - msg.Length - 2) / 2;
            string padd = new string(' ', padding);
            string line = "|" + padd + msg + padd + "|";

            Console.WriteLine();
            PrintBorderLine('=', false);
            Console.WriteLine(line);
            PrintBorderLine('=', false);
        }

        /// <summary>
        /// Render response with header and footer in console
        /// </summary>
        /// <param name="session">Current session object</param>
        public virtual void Render(ISession session)
        {
            Console.Clear();

            if(session != null)
            {
                RenderHeader(session);
            }
            else
            {
                RenderHeader();
            }

            RenderResult();

            RenderFooter();
        }

        /// <summary>
        /// Render response result
        /// </summary>
        public override void RenderResult()
        {
            RenderMessage();

            if(ResultBody != null)
            {
                this.PrintBorderLine('_');
                RenderBody();
            }
        }

        /// <summary>
        /// Render response status message
        /// </summary>
        protected virtual void RenderMessage()
        {
            switch (this.Result)
            {
                case ActionResult.Error:
                    this.PrintColorizedWordInBracketsNoBreak(Result.ToString(), ConsoleColor.Red);
                    this.PrintColorizedMessage(Message, ConsoleColor.Yellow);
                    break;
                case ActionResult.Warning:
                    this.PrintColorizedWordInBracketsNoBreak(Result.ToString(), ConsoleColor.Yellow);
                    this.PrintColorizedMessage(Message, ConsoleColor.Yellow);
                    break;
                case ActionResult.Succes:
                    this.PrintColorizedWordInBracketsNoBreak(Result.ToString(), ConsoleColor.Green);
                    this.PrintColorizedMessage(Message, ConsoleColor.Yellow);
                    break;
                case ActionResult.NotFound:
                    this.PrintColorizedWordInBracketsNoBreak(Result.ToString(), ConsoleColor.DarkYellow);
                    this.PrintColorizedMessage(Message, ConsoleColor.Yellow);
                    break;
                default:
                    this.PrintColorizedWordInBracketsNoBreak(Result.ToString(), ConsoleColor.DarkYellow);
                    this.PrintColorizedMessage(Message, ConsoleColor.DarkBlue);
                    break;
            }
        }

        /// <summary>
        /// Displays the word in the given color without newline
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="color">Color</param>
        protected void PrintColorizedWordInBracketsNoBreak(string msg, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write($"\t[{msg}]\t");
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Displays horizontal line from given chars with given color
        /// </summary>
        /// <param name="ch">Char</param>
        /// <param name="color">Color</param>
        protected void PrintBorderLineColorized(char ch, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(new String(ch, Width));
            Console.ForegroundColor = prev;
            Console.WriteLine();
        }

        /// <summary>
        /// Displays horizontal line from given chars with default color
        /// </summary>
        /// <param name="ch">Char</param>
        /// <param name="newLine">If true print empty line after border</param>
        protected void PrintBorderLine(char ch, bool newLine = true)
        {
            Console.WriteLine(new String(ch, Width));
            if (newLine)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the message in the given color with a new line
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
        protected void PrintColorizedMessage(string msg, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Displayes the string with given color without new line
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
        protected void PrintColorizedMessageNoBreak(string msg, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Displayes the string with colorized first word
        /// </summary>
        /// <param name="word">Colorized part</param>
        /// <param name="definition">Default color part</param>
        /// <param name="color">Color</param>
        protected void PrintColorizedDefinition(string word, string definition, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write($"\t{word} ");
            Console.ForegroundColor = prev;
            Console.WriteLine(definition);
        }

        /// <summary>
        /// Justify a list of strings
        /// </summary>
        /// <param name="strings">List of string</param>
        /// <returns>list of strings with added whitespaces</returns>
        protected List<string> MakeColumn(IEnumerable<string> strings)
        {
            int columnWidth = strings.Max(s => s.Length);
            List<string> rows = new List<string>();
            foreach (var item in strings)
            {
                rows.Add(item.Length < columnWidth ? item + new string(' ', columnWidth - item.Length) : item);
            }
            return rows;
        }
    }
}
