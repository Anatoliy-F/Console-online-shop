using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    /// <summary>
    /// Implement IActionResultFactory. Instantiate ErrorView object.
    /// </summary>
    public class ErrorViewFactory : IActionResultFactory
    {
        /// <summary>
        /// Return instance of ErrorView object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="responseBody">Collection of requested entitie</param>
        /// <returns>ErrorView object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new ErrorView(result, message, responseBody);
        }
    }
}
