using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    /// <summary>
    /// Implement IActionResultFactory. Instantiate LoginView object.
    /// </summary>
    public class LoginViewFactory : IActionResultFactory
    {
        /// <summary>
        /// Return instance of LoginView object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="responseBody">Collection of requested entitie</param>
        /// <returns>LoginView object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new LoginView(result, message, responseBody);
        }
    }
}
