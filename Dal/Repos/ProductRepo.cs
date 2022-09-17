using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Dal.Repos
{
    /// <inheritdoc />
    public class ProductRepo : IProductRepo
    {   
        /// <summary>
        /// Products list
        /// </summary>
        private readonly List<Product> _products;

        /// <summary>
        /// Categories list
        /// </summary>
        private readonly List<Category> _categories;

        /// <summary>
        /// Initialize new instance of ProductRepository to manage saving, updating and retrieving Product entities
        /// </summary>
        /// <param name="shopContext">Storage for all entities</param>
        public ProductRepo(IShopContext shopContext)
        {
            _products = shopContext.Products;
            _categories = shopContext.Categories;
        }

        /// <inheritdoc />
        public int Add(Product entity)
        {
            if(entity.Name != String.Empty && entity.Description != String.Empty && entity.Price > 0m && entity.CategoryId != 0)
            {
                var category = _categories.FirstOrDefault(c => c.Id == entity.CategoryId);

                if (category != null){
                    entity.CategoryNav = category;

                    int id = _products.Max(p => p.Id) + 1;
                    entity.Id = id;
                    _products.Add(entity);
                    return id;
                }
                else
                {
                    throw new NotFoundException($"No category with id: {entity.CategoryId} in shop");
                }
            }
            else
            {
                throw new CustomRepoException("Product shouldn't contains empty properties or price less than 0");
            }
        }

        /// <inheritdoc />
        public int Update(int id, Product updatedProduct)
        {
            Product product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"No product with id: {id} in our shop");
            }

            if (updatedProduct.CategoryId != 0)
            {
                var category = _categories.FirstOrDefault(c => c.Id == updatedProduct.CategoryId);
                if (category != null) {
                    product.CategoryId = updatedProduct.CategoryId;
                    product.CategoryNav = _categories.FirstOrDefault(c => c.Id == updatedProduct.CategoryId);
                }
                else
                {
                    throw new CustomRepoException($"No category with id: {updatedProduct.CategoryId} in our shop");
                }
            }

            if (updatedProduct.Price > 0)
            {
                product.Price = updatedProduct.Price;
            }

            if (updatedProduct.Name != String.Empty)
            {
                product.Name = updatedProduct.Name;
            }

            if (updatedProduct.Description != String.Empty)
            {
                product.Description = updatedProduct.Description;
            }
            return product.Id;
        }

        /// <inheritdoc />
        public Product Find(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        /// <inheritdoc />
        public IEnumerable<Product> FindByName(string name)
        {
            return _products.Where(p => p.Name.ToLower().Contains(name.ToLower()));
        }

        /// <inheritdoc />
        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
    }
}
