using ConsoleShop.Controller.Base;

namespace ConsoleShop.Controller.Interfaces
{   
    /// <summary>
    /// Defines operations applicable with error
    /// </summary>
    public interface IErrorController : IController
    {
        /// <summary>
        /// Show error object
        /// </summary>
        /// <param name="msg">Error message</param>
        /// <returns>Response object</returns>
        public IActionResult ShowError(string msg);
    }
}
