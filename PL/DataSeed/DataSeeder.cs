using ConsoleShop.Dal;
using ConsoleShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.DataSeed
{
    /// <summary>
    /// Populates the store with initial data
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Categories collection
        /// </summary>
        private readonly static List<Category> _categories = GetCategories().ToList();
        /// <summary>
        /// Products collection
        /// </summary>
        private readonly static List<Product> _products = GetProducts().ToList();
        /// <summary>
        /// CartLine collection
        /// </summary>
        private readonly static List<CartLine> _cartLines = GetCartLines().ToList();
        /// <summary>
        /// Order collection
        /// </summary>
        private readonly static List<Order> _orders = GetOrders().ToList();
        
        /// <summary>
        /// Return initial data
        /// </summary>
        /// <returns>IShopContext instance</returns>
        public static IShopContext InitContext()
        {
            IShopContext context = new ShopContext();

            context.Categories = GetCategories();
            context.Products = GetProducts();
            context.Users = GetUsers();
            context.Orders = GetOrders();

            return context;
        }

        /// <summary>
        /// Return initial orders collection
        /// </summary>
        /// <returns>List&lt;Order&gt;</returns>
        private static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Name = "Some name",
                    UserId = 1,
                    Address = "Some address",
                    Status = OrderStatus.Sent,
                    Lines = new List<CartLine>
                    {
                        _cartLines[0], _cartLines[1]
                    }
                },
                new Order
                {
                    Id = 2,
                    Name = "New Name",
                    UserId = 2,
                    Address = "Admin address",
                    Status = OrderStatus.PaymentRecieved,
                    Lines = new List<CartLine>
                    {
                        _cartLines[2], _cartLines[3]
                    }
                },
                new Order
                {
                    Id = 3,
                    Name = "Some name",
                    UserId = 1,
                    Address = "Some address",
                    Status = OrderStatus.Completed,
                    Lines = new List<CartLine>
                    {
                        _cartLines[4]
                    }
                },

            };
        }

        /// <summary>
        /// Return initial CartLine collection
        /// </summary>
        /// <returns>List&lt;CartLine&gt;</returns>
        private static List<CartLine> GetCartLines()
        {
            return new List<CartLine> {
                new CartLine
                {
                    Id = 1,
                    Product = _products[0],
                    Quantity = 2
                },
                new CartLine
                {
                    Id = 2,
                    Product = _products[3],
                    Quantity = 4
                },
                new CartLine
                {
                    Id = 3,
                    Product = _products[2],
                    Quantity = 3
                },
                new CartLine
                {
                    Id = 4,
                    Product = _products[6],
                    Quantity = 1
                },
                new CartLine
                {
                    Id = 5,
                    Product = _products[4],
                    Quantity = 2
                },
            };
        }

        /// <summary>
        /// Return initial Users collection
        /// </summary>
        /// <returns>List&lt;User&gt;</returns>
        private static List<User> GetUsers()
        {
            return new List<User>
            {
                new User()
                {
                    Id = 1,
                    Role = UserRole.RegisteredUser,
                    Name = "JohnDoe",
                    PasswordHash = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", //qwerty
                    Orders = new List<Order>
                    {
                        _orders[0], _orders[2]
                    }
                },
                new User()
                {
                    Id = 2,
                    Role = UserRole.Administrator,
                    Name = "Admin",
                    PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", //admin
                    Orders = new List<Order>
                    {
                        _orders[1]
                    }
                }
            };
        }

        /// <summary>
        /// Return initial categories collection
        /// </summary>
        /// <returns>List&lt;Category&gt;</returns>
        private static List<Category> GetCategories()
        {
            return new List<Category> { 
                new Category()
                {
                    Id = 1,
                    Name = "Football"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Tennis"
                },
                new Category()
                {
                    Id = 3,
                    Name = "PowerLifting"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Fitness"
                },
            };
        }

        /// <summary>
        /// Return initial products collection
        /// </summary>
        /// <returns>List&lt;Category&gt;</returns>
        private static List<Product> GetProducts()
        {
            return new List<Product> { 
                new Product()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "T-Shirt",
                    Description = "Real Madrid #7",
                    Price = 100m,
                    CategoryNav = _categories[0],
                },
                new Product()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Ball",
                    Description = "Retro-style soccer ball",
                    Price = 20m,
                    CategoryNav = _categories[0],
                },
                new Product()
                {
                    Id = 3,
                    CategoryId = 1,
                    Name = "Football boots",
                    Description = "Liverpool style boots",
                    Price = 30m,
                    CategoryNav = _categories[0],
                },
                new Product()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Wristband",
                    Description = "Wide white super",
                    Price = 15m,
                    CategoryNav = _categories[1],
                },
                new Product()
                {
                    Id = 5,
                    CategoryId = 2,
                    Name = "Racquet",
                    Description = "Tennis racket, Wilson brand",
                    Price = 200m,
                    CategoryNav = _categories[1],
                },
                new Product()
                {
                    Id = 6,
                    CategoryId = 2,
                    Name = "Green Ball",
                    Description = "World-wide best tennis ball",
                    Price = 26m,
                    CategoryNav = _categories[1],
                },
                new Product()
                {
                    Id = 7,
                    CategoryId = 3,
                    Name = "Belt",
                    Description = "Protect your back against hernia",
                    Price = 300m,
                    CategoryNav = _categories[2],
                },
                new Product()
                {
                    Id = 8,
                    CategoryId = 3,
                    Name = "Magnesia",
                    Description = "Your never drops iron when your pump it",
                    Price = 8m,
                    CategoryNav = _categories[2],
                },
                new Product()
                {
                    Id = 9,
                    CategoryId = 3,
                    Name = "Bandages",
                    Description = "They work - you have a rest",
                    Price = 50,
                    CategoryNav = _categories[2],
                },
                new Product()
                {
                    Id = 10,
                    CategoryId = 4,
                    Name = "Selfie-stick",
                    Description = "Everyone should know abaout your choice",
                    Price = 20m,
                    CategoryNav = _categories[3],
                },
                new Product()
                {
                    Id = 11,
                    CategoryId = 4,
                    Name = "Reebok T-Shirt",
                    Description = "Your are cool? like never before",
                    Price = 100m,
                    CategoryNav = _categories[3],
                },
                new Product()
                {
                    Id = 12,
                    CategoryId = 4,
                    Name = "Protein",
                    Description = "All you need is - protein",
                    Price = 100m,
                    CategoryNav = _categories[3],
                },
            };
        }
    }
}
