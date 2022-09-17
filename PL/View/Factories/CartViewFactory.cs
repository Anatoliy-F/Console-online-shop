using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    /// <summary>
    /// Implement IActionResultFactory. Instantiate CartView object.
    /// </summary>
    public class CartViewFactory : IActionResultFactory
    {
        /// <summary>
        /// Return instance of CartView object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="responseBody">Collection of requested entitie</param>
        /// <returns>CartView object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new CartView(result, message, responseBody);
        }
    }
}
