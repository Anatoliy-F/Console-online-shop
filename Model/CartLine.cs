using ConsoleShop.Model.BaseEntity;

namespace ConsoleShop.Model
{
    /// <summary>
    /// CartLine - represent item in order. Contain Product and its Quantity
    /// </summary>
    public class CartLine : IEntity
    {
        /// <summary>
        /// represent unique identificator for entity 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// navigation link to Product item
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// represent quantity of ordered goods
        /// </summary>
        public int Quantity { get; set; }
    }
}
