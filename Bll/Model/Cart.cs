using ConsoleShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Bll.Model
{
    /// <inheritdoc/>
    public class Cart : ICart
    {
        /// <inheritdoc/>
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        
        /// <inheritdoc/>
        public virtual void AddItem(Product product, int quantity = 1)
        {
            CartLine line = Lines.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                Lines.Add(new CartLine {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <inheritdoc/>
        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.Id == product.Id);

        /// <inheritdoc/>
        public decimal ComputeTotalValue() =>
            Lines.Sum(e => e.Product.Price * e.Quantity);

        /// <inheritdoc/>
        public virtual void Clear() => Lines.Clear();
    }

   


}
