using ConsoleShop.Model.BaseEntity;
using System;

namespace ConsoleShop.Model
{

    /// <summary>
    /// Category - entity that separate product in store in subsets
    /// </summary>
    public class Category : IEntity
    {
        /// <summary>
        /// represent unique identificator for entity 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// represent title of the Category
        /// </summary>
        public string Name { get; set; } = String.Empty;
    }
}
