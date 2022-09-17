using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Dal.Repos
{
    /// <inheritdoc />
    public class OrderRepo : IOrderRepo
    {
        /// <summary>
        /// Orders list
        /// </summary>
        private readonly List<Order> _orders;

        /// <summary>
        /// Users list
        /// </summary>
        private readonly List<User> _users;

        /// <summary>
        /// Initialize new instance of OrderRepository to manage saving, updating and retrieving Order's entities 
        /// </summary>
        /// <param name="shopContext">Storage for all entities</param>
        public OrderRepo(IShopContext shopContext)
        {
            _orders = shopContext.Orders;
            _users = shopContext.Users;
        }

        /// <inheritdoc />
        public int Add(Order entity)
        {
            int id = _orders.Max(o => o.Id) + 1;
            entity.Id = id;
            entity.Status = OrderStatus.New;
            _orders.Add(entity);
            var user = _users.Find(u => u.Id == entity.UserId);
            user.Orders.Add(entity);
            
            return id;
        }

        /// <inheritdoc />
        public Order Find(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        /// <inheritdoc />
        public IEnumerable<Order> FindByUser(int userId)
        {
            return _orders.Where(o => o.UserId == userId);
        }

        /// <inheritdoc />
        public IEnumerable<Order> GetAll()
        {
            return _orders;
        }

        /// <inheritdoc />
        public void SetStatus(int orderId, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if(order == null)
            {
                throw new NotFoundException($"No order with id: #{orderId} in our shop");
            }
            order.Status = status;
        }
    }
}
