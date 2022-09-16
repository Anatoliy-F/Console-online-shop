using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Model;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class ErrorController : IErrorController
    {
        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of ErrorController
        /// </summary>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public ErrorController(IActionResultFactory actionResultFactory)
        {
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult ShowError(string msg)
        {
            return _actionResultFactory.GetResultRender(ActionResult.Error, msg);
        }
    }
}
