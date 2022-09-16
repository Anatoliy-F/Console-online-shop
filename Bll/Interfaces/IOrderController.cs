using ConsoleShop.Controller.Base;
using ConsoleShop.Model;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with Order entities
    /// </summary>
    public interface IOrderController : IController
    {
        /// <summary>
        /// Returns orders made by an authorized user
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult ShowOrders();

        /// <summary>
        /// Returns all orders
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult ShowAllOrders();

        /// <summary>
        /// The method confirms the receipt of the order by the user
        /// </summary>
        /// <param name="orderId">Id of order</param>
        /// <returns>Response object</returns>
        public IActionResult ConfirmReceipt(int orderId);

        /// <summary>
        /// Cancellation of the order by the user
        /// </summary>
        /// <param name="orderId">Id of order</param>
        /// <returns>Response object</returns>
        public IActionResult CancellOrder(int orderId);

        /// <summary>
        /// Change order status
        /// </summary>
        /// <param name="orderId">Id of order</param>
        /// <param name="status">Status</param>
        /// <returns>Response object</returns>
        public IActionResult ChangeOrderStatus(int orderId, OrderStatus status);
    }
}
