using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    public class LoginViewFactory : IActionResultFactory
    {
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new LoginView(result, message, responseBody);
        }
    }
}
