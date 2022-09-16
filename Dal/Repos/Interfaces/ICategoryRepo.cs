using ConsoleShop.Model;

namespace ConsoleShop.Dal.Repos.Interfaces
{
    /// <summary>
    /// Interface defines the operations applicable to the ConsoleShop.Model.Category entities
    /// </summary>
    public interface ICategoryRepo : IRepo<Category>
    {
        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="id">int id - id of existing Category</param>
        /// <param name="name">string name - new Category title</param>
        /// <returns>int id - id of updated Category</returns>
        /// <exception cref="Dal.Exception.NotFoundException">Trown when category with <paramref name="id"/>
        /// didn;t exist in context</exception>
        int Update(int id, string name);
    }
}
