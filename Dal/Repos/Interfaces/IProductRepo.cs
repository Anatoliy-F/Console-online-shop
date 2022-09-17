using ConsoleShop.Model;
using System.Collections.Generic;

namespace ConsoleShop.Dal.Repos.Interfaces
{
    /// <summary>
    /// Interface defines the operations applicable to the ConsoleShop.Model.Product entities
    /// </summary>
    public interface IProductRepo : IRepo<Product>
    {
        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">int id - id of existing Product</param>
        /// <param name="updatedProduct">Product instance, all non-empty properties 
        /// replaces properties of existing product</param>
        /// <returns>int id - id of updated Product</returns>
        /// <exception cref="ConsoleShop.Dal.Exception.NotFoundException">Thrown when 
        /// collection didn't containt object with <paramref name="id"/></exception>
        /// <exception cref="ConsoleShop.Dal.Exception.CustomRepoException">Thrown when
        /// <paramref name="updatedProduct"/> specified Category that
        /// application context didn't containt </exception>
        public int Update(int id, Product updatedProduct);

        /// <summary>
        /// Return Products from collection searching them by name
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>IEnumerable&lt;Product&gt; <see cref="ConsoleShop.Model.Product"/></returns>
        public IEnumerable<Product> FindByName(string name);
    }
}
