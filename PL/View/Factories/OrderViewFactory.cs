using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    public class OrderViewFactory : IActionResultFactory
    {
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new OrderView(result, message, responseBody);
        }
    }
}
