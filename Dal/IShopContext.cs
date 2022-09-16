using ConsoleShop.Model;
using System.Collections.Generic;

namespace ConsoleShop.Dal
{
    /// <summary>
    /// Represent storage for all entities
    /// </summary>
    public interface IShopContext
    {
        /// <summary>
        /// Represent list of Products
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Represent list of Categories
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Represent list of Users
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Represent list of Orders
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
