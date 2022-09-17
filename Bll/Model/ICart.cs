using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Bll.Model
{
    /// <summary>
    /// Define properties and methods of shopping cart
    /// </summary>
    public interface ICart : IEntity
    {
        /// <summary>
        /// Position in the user's cart
        /// </summary>
        public List<CartLine> Lines { get; set; }

        /// <summary>
        /// Add item to cart
        /// </summary>
        /// <param name="product">Selected product</param>
        /// <param name="quantity">Quantity of goods</param>
        public void AddItem(Product product, int quantity = 1);

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="product">Selected product</param>
        public void RemoveLine(Product product);

        /// <summary>
        /// Returns the total cost of goods
        /// </summary>
        /// <returns>Total cost of goods</returns>
        public decimal ComputeTotalValue();

        /// <summary>
        /// Removes all items
        /// </summary>
        public void Clear();
    }
}
