using ConsoleShop.Model.BaseEntity;
using System;
using System.Collections.Generic;

namespace ConsoleShop.Model
{
    /// <summary>
    /// Represent available order statuses. 
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New order
        /// </summary>
        New = 0,
        /// <summary>
        /// Order cancelled by user
        /// </summary>
        CancelledByUser = 1,
        /// <summary>
        /// Order cancelled by administrator
        /// </summary>
        CancelledByAdministrator = 2,
        /// <summary>
        /// Payment received
        /// </summary>
        PaymentRecieved = 3,
        /// <summary>
        /// Package sent
        /// </summary>
        Sent = 4,
        /// <summary>
        /// Order received
        /// </summary>
        Received = 5,
        /// <summary>
        /// Order completed
        /// </summary>
        Completed = 6,
    }

    /// <summary>
    /// Order - entity that represent purchase in our store
    /// </summary>
    public class Order : IEntity
    {
        /// <summary>
        /// represent unique identificator for entity 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// represent name of order assigmented by user
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// represent identificator of user, who make this order
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// represent delivering address
        /// </summary>
        public string Address { get; set; } = String.Empty;

        /// <summary>
        /// represent currnet order status
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.New;

        /// <summary>
        /// represent type and quantity of buying items
        /// </summary>
        public ICollection<CartLine> Lines { get; set; } = null;
    }
}
