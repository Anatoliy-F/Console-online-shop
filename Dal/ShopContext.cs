using ConsoleShop.Model;
using System.Collections.Generic;

namespace ConsoleShop.Dal
{
    /// <inheritdoc />
    public class ShopContext : IShopContext
    {
        /// <inheritdoc />
        public List<Product> Products { get; set; } = new List<Product>();

        /// <inheritdoc />
        public List<Category> Categories { get; set; } = new List<Category>();

        /// <inheritdoc />
        public List<User> Users { get; set; } = new List<User>();

        /// <inheritdoc />
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
