using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Dal.Repos
{
    /// <summary>
    /// Implement IRepo. Provide base repositories operations
    /// </summary>
    /// <typeparam name="T">IEntity type</typeparam>
    public abstract class BaseRepo<T> where T : IEntity, new()
    {
        /// <summary>
        /// Storage for all entities
        /// </summary>
        public IShopContext ShopContext { get; }

        /// <summary>
        /// Collection of entities
        /// </summary>
         public List<T> List { get; }

        /// <summary>
        /// Initialize new instance of repository
        /// </summary>
        /// <param name="shopContext">Storage for all entities</param>
        protected BaseRepo(IShopContext shopContext)
        {
            ShopContext = shopContext;
        }

    }
}
