using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    /// <summary>
    /// Implement IActionResultFactory. Instantiate OrderView object.
    /// </summary>
    public class OrderViewFactory : IActionResultFactory
    {
        /// <summary>
        /// Return instance of OrderView object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="responseBody">Collection of requested entitie</param>
        /// <returns>OrderView object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new OrderView(result, message, responseBody);
        }
    }
}
