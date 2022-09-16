using ConsoleShop.Model.BaseEntity;
using System;

namespace ConsoleShop.Model
{
    /// <summary>
    /// Product - entity that represent product in our store
    /// </summary>
    public class Product : IEntity
    {
        /// <summary>
        /// represent unique identificator for entity 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// represent Id of category that corresponds for product
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// represent Product title for searching and ordering purposes
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// represent Product description. Appointment, manufacturer, etc.
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// represent Product cost
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// navigation property, that represent Product's Category
        /// </summary>
        public Category CategoryNav { get; set; } = null;
    }
}
