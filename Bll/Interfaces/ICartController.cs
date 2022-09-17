using ConsoleShop.Controller.Base;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with shopping cart
    /// </summary>
    public interface ICartController : IController
    {
        /// <summary>
        /// Add position in cart
        /// </summary>
        /// <param name="productId">Id of added product</param>
        /// <param name="quantity">Quantity of added product, be default 1</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult AddToCart(int productId, int quantity = 1);

        /// <summary>
        /// Show cart contents
        /// </summary>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult ShowCart();

        /// <summary>
        /// Checkout
        /// </summary>
        /// <param name="name">Order name</param>
        /// <param name="address">Delivery address</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult MakeOrder(string name, string address);

        /// <summary>
        /// Clear the contents of the cart
        /// </summary>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult ClearCart();
    }
}
