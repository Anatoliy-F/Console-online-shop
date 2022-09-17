using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    public class LoginView : BaseView
    {
        private readonly IEnumerable<User> _user;

        /// <summary>
        /// Initialize new instance of LoginView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public LoginView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _user = (IEnumerable<User>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var column1 = this.MakeColumn(_user.Select(c => c.Id.ToString()));
            var column2 = this.MakeColumn(_user.Select(c => c.Name));
            var column3 = this.MakeColumn(_user.Select(c => c.PasswordHash));

            for (int i = 0; i < column1.Count; i++)
            {
                this.PrintColorizedDefinition(column1[i], column2[i], ConsoleColor.Magenta);
                this.PrintColorizedMessage($"\t\t{column3[i]}", ConsoleColor.Yellow);
                if (i < column1.Count - 1) { this.PrintBorderLine('-'); }
            }
        }
    }
}
