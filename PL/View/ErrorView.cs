using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;

namespace ConsoleShop.View
{
    /// <summary>
    /// Implement IActionResult for rendering errors
    /// </summary>
    public class ErrorView : BaseView
    {
        /// <summary>
        /// Initialize new instance of ErrorView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public ErrorView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody){}

        /// <summary>
        /// Displays default message
        /// </summary>
        protected override void RenderBody()
        {
            Console.WriteLine("Use Help");
        }
    }
}
