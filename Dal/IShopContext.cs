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
        /// Represent list of Products <see cref="ConsoleShop.Model.Product"/>
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Represent list of Categories <see cref="ConsoleShop.Model.Category"/>
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Represent list of Users <see cref="ConsoleShop.Model.User"/>
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Represent list of Orders <see cref="ConsoleShop.Model.Order"/>
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
