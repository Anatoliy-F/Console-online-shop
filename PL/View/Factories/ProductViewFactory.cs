using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.View.Factories
{
    public class ProductViewFactory : IActionResultFactory
    {
        public IActionResult GetResultRender(ActionResult result, string message, IEnumerable<IEntity> responseBody = null)
        {
            return new ProductView(result, message, responseBody);
        }
    }
}
