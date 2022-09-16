using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Dal.Repos
{
    /// <inheritdoc />
    public abstract class BaseRepo<T> where T : IEntity, new()
    {
        /// <inheritdoc />
        public IShopContext ShopContext { get; }

        /// <inheritdoc />
        public List<T> List { get; }

        /// <summary>
        /// Initialize new instance of repository
        /// </summary>
        /// <param name="shopContext"></param>
        protected BaseRepo(IShopContext shopContext)
        {
            ShopContext = shopContext;
        }

    }
}
