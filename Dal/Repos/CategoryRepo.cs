using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Dal.Repos
{
    /// <inheritdoc />
    public class CategoryRepo : ICategoryRepo
    {   
        /// <summary>
        /// Categories list
        /// </summary>
        private readonly List<Category> _categories;

        /// <summary>
        /// Initialize new instance of CategoryRepository to manage saving, updating and retrieving Categories entities 
        /// </summary>
        /// <param name="shopContext">storage</param>
        public CategoryRepo(IShopContext shopContext)
        {
            _categories = shopContext.Categories;
        }

        /// <inheritdoc />
        public int Add(Category entity)
        {   
            if(_categories.Any(c => c.Name == entity.Name))
            {
                throw new CustomRepoException("We already have this category");
            }
            int id = _categories.Max(c => c.Id) + 1;
            entity.Id = id;
            _categories.Add(entity);
            return id;
        }

        /// <inheritdoc />
        public Category Find(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        /// <inheritdoc />
        public IEnumerable<Category> GetAll()
        {
            return _categories;
        }

        /// <inheritdoc />
        public int Update(int id, string name)
        {
            Category category = _categories.FirstOrDefault(c => c.Id == id);

            if(category == null)
            {
                throw new NotFoundException($"No categories with id: {id} in our shop");
            }
            if (name != String.Empty)
            {
                category.Name = name;
            }
            return category.Id;
        }
    }
}
