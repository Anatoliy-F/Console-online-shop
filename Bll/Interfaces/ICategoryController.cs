using ConsoleShop.Controller.Base;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with Category entities
    /// </summary>
    public interface ICategoryController
    {
        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult ShowAll();

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Response object</returns>
        public IActionResult ShowById(int id);

        /// <summary>
        /// Add new Category
        /// </summary>
        /// <param name="name">Name of new Category</param>
        /// <returns>Response object</returns>
        public IActionResult Add(string name);

        /// <summary>
        /// Update existing category object
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="name">New category name</param>
        /// <returns>Response object</returns>
        public IActionResult Update(int id, string name);
    }
}
