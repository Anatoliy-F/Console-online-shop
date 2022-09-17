using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Commands.ViewModels
{
    /// <summary>
    /// Implements the products form's
    /// </summary>
    public static class ProductViewModel
    {
        /// <summary>
        /// Implements the product creation form
        /// </summary>
        /// <param name="categories">List of available categories</param>
        /// <returns>New Product instance</returns>
        public static Product CreateProduct(IEnumerable<Category> categories)
        {
            Product product = new Product();
            Console.WriteLine("Enter product name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter product description");
            product.Description = Console.ReadLine();
            Console.WriteLine("Enter category id");
            foreach(var category in categories)
            {
                Console.WriteLine($"CategoryName: {category.Name} | Id: [{category.Id}]");
            }
            string categoryId = Console.ReadLine();

            if(Int32.TryParse(categoryId, out int catId) && categories.Any(c => c.Id == catId))
            {
                product.CategoryId = catId;
            }
            Console.WriteLine("Enter price");

            string priceString = Console.ReadLine();
            if(Decimal.TryParse(priceString, out decimal price))
            {
                product.Price = price;
            }
            return product;
        }

        /// <summary>
        /// Implements the product update form
        /// </summary>
        /// <param name="product">Available product</param>
        /// <param name="categories">List of available categories</param>
        /// <returns></returns>
        public static Product ChangeProduct(Product product, IEnumerable<Category> categories)
        {
            Product updateProduct = new Product();
            Console.WriteLine($"Current name: {product.Name}, enter new name, press <Enter> to skip");
            updateProduct.Name = Console.ReadLine();
            Console.WriteLine($"Current description: {product.Description} \nenter new description, or press <Enter> to skip");
            updateProduct.Description = Console.ReadLine();
            Console.WriteLine($"Current category: {product.CategoryNav.Name}\nEnter new category id, or press <Enter> to skip");
            foreach (var category in categories)
            {
                Console.WriteLine($"CategoryName: {category.Name} | Id: [{category.Id}]");
            }
            string categoryId = Console.ReadLine();
            if(categoryId != String.Empty && Int32.TryParse(categoryId, out int catId) && categories.Any(c => c.Id == catId))
            {
                updateProduct.CategoryId = catId;
            }
            Console.WriteLine($"Current product price: {product.Price}\nEnter new price, or press <Enter> to skip");
           
            string priceString = Console.ReadLine();
            
            if(priceString != String.Empty && Decimal.TryParse(priceString, out decimal price))
            {
                updateProduct.Price = price;
            }
            return updateProduct;
        }
    }
}
