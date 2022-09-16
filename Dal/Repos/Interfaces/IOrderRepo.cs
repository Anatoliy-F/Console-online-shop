using ConsoleShop.Model;
using System.Collections.Generic;

namespace ConsoleShop.Dal.Repos.Interfaces
{
    /// <summary>
    /// Interface defines the operations applicable to the ConsoleShop.Model.Order entities
    /// </summary>
    public interface IOrderRepo : IRepo<Order>
    {
        /// <summary>
        /// Return Orders made by a specific user
        /// </summary>
        /// <param name="userId">int id - id of existing User</param>
        /// <returns>IEnumerable&lt;Order&gt;</returns>
        public IEnumerable<Order> FindByUser(int userId);

        /// <summary>
        /// Setting current status of Order
        /// </summary>
        /// <param name="orderId">int id - id of existing Order</param>
        /// <param name="status">OrderStatus status - available status</param>
        public void SetStatus(int orderId, OrderStatus status);
    }
}
