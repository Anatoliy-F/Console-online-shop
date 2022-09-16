using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Controller.Base
{
    /// <summary>
    /// Define factory that instantiate IActionResult object
    /// </summary>
    public interface IActionResultFactory
    {
        /// <summary>
        /// The method accepts response parameters and returns a response object
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response message</param>
        /// <param name="responseBody">Collection of requested entities, may be null, if
        /// response didn't containt body</param>
        /// <returns>IActionResult object</returns>
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null);
    }
}
