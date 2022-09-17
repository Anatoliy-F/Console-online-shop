using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    /// <summary>
    /// Implement IActionResultFactory. Instantiate ProductView object.
    /// </summary>
    public class ProductViewFactory : IActionResultFactory
    {
        /// <summary>
        /// Return instance of ProductView object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="responseBody">Collection of requested entitie</param>
        /// <returns>ProductView object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new ProductView(result, message, responseBody);
        }
    }
}
