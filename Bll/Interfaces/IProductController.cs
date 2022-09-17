using ConsoleShop.Controller.Base;
using ConsoleShop.Model;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with Product
    /// </summary>
    public interface IProductController : IController
    {
        /// <summary>
        /// Return all products
        /// </summary>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult ShowAll();

        /// <summary>
        /// Return product by id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult ShowById(int id);

        /// <summary>
        /// Returning a collection of products whose names match the description of the query
        /// </summary>
        /// <param name="name">Search query</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult ShowByName(string name);

        /// <summary>
        /// Add new Product in store
        /// </summary>
        /// <param name="product">New Product object</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult AddNew(Product product);

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">Id of updated product</param>
        /// <param name="product">Product object, all non-empty properties 
        /// replaces properties of existing product</param>
        /// <returns>Response object <see cref="Base.IActionResult"/></returns>
        public IActionResult UpdateProduct(int id, Product product);
    }
}
