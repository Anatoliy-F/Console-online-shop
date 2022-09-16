using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Controller.Base
{
    /// <inheritdoc />
    public abstract class BaseActionResult : IActionResult
    {
        /// <inheritdoc />
        public ActionResult Result { get; protected set; }

        /// <inheritdoc />
        public string Message { get; protected set; }

        /// <inheritdoc />
        public IEnumerable<IEntity> ResultBody { get; protected set; }

        /// <summary>
        /// Initialize new instance of response object. 
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response message</param>
        /// <param name="resultBody">Collection of requested entities</param>
        protected BaseActionResult(ActionResult result, string message, IEnumerable<IEntity> resultBody = null)
        {
            Result = result;
            Message = message;
            ResultBody = resultBody;
        }

        /// <inheritdoc />
        public abstract void RenderResult();
    }
}
