using ConsoleShop.Dal;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos;
using ConsoleShop.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.Dal
{
    public class ProductRepoTests
    {
        [Fact]
        public void Can_Add_Product()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category> {
                new Category{ Id = 1, Name = "cat1"},
                new Category{ Id = 2, Name = "cat2"},
                new Category{ Id = 3, Name = "cat3"},
                new Category{ Id = 4, Name = "cat4"},
            });
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "prod1", Description = "desc1", Price = 10m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 2, Name = "prod2", Description = "desc2", Price = 20m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 3, Name = "prod3", Description = "desc3", Price = 30m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 4, Name = "prod4", Description = "desc4", Price = 40m, CategoryId = 2, CategoryNav = mock.Object.Categories.First(c => c.Id == 2)},
                new Product {Id = 5, Name = "prod5", Description = "desc5", Price = 50m, CategoryId = 3, CategoryNav = mock.Object.Categories.First(c => c.Id == 3)},
                new Product {Id = 6, Name = "prod6", Description = "desc6", Price = 60m, CategoryId = 4, CategoryNav = mock.Object.Categories.First(c => c.Id == 4)},
            });

            ProductRepo pr = new ProductRepo(mock.Object);
            Product prod1 = new Product { Name = "new1", Description = "newDesc1", Price = 10m, CategoryId = 2 };
            Product prod2 = new Product { Name = "new2", Description = "newDesc2", Price = 20m, CategoryId = 2 };
            Product prod3 = new Product { Name = "new3", Description = "newDesc3", Price = 30m, CategoryId = 3 };

            //Action
            var before_count = pr.GetAll().ToArray().Length;

            pr.Add(prod1);
            pr.Add(prod2);
            pr.Add(prod3);

            var after_count = pr.GetAll().ToArray().Length;


            //Assert
            Assert.Equal(6, before_count);
            Assert.Equal(9, after_count);
            Assert.Equal(7, pr.FindByName("new1").First().Id);
            Assert.Equal(9, pr.FindByName("new3").First().Id);
            Assert.Same(pr.Find(7).CategoryNav, pr.Find(8).CategoryNav);
            Assert.Throws<NotFoundException>(() => { pr.Add(new Product { Name = "a", Description = "b", Price = 10m, CategoryId = 10 }); });
            Assert.Throws<CustomRepoException>(() => { pr.Add(new Product { Name = "a", Description = "b", Price = 0m, CategoryId = 1 }); });
            Assert.Throws<CustomRepoException>(() => { pr.Add(new Product { Name = "a", Description = "", Price = 10m, CategoryId = 1 }); });
            Assert.Throws<CustomRepoException>(() => { pr.Add(new Product { Name = "", Description = "b", Price = 10m, CategoryId = 1 }); });
        }

        [Fact]
        public void Can_Update_Product()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category> {
                new Category{ Id = 1, Name = "cat1"},
                new Category{ Id = 2, Name = "cat2"},
                new Category{ Id = 3, Name = "cat3"},
                new Category{ Id = 4, Name = "cat4"},
            });
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "prod1", Description = "desc1", Price = 10m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 2, Name = "prod2", Description = "desc2", Price = 20m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 3, Name = "prod3", Description = "desc3", Price = 30m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 4, Name = "prod4", Description = "desc4", Price = 40m, CategoryId = 2, CategoryNav = mock.Object.Categories.First(c => c.Id == 2)},
                new Product {Id = 5, Name = "prod5", Description = "desc5", Price = 50m, CategoryId = 3, CategoryNav = mock.Object.Categories.First(c => c.Id == 3)},
                new Product {Id = 6, Name = "prod6", Description = "desc6", Price = 60m, CategoryId = 4, CategoryNav = mock.Object.Categories.First(c => c.Id == 4)},
            });

            ProductRepo pr = new ProductRepo(mock.Object);

            Product prod1 = new Product { Description = "newDesc1", CategoryId = 2 };
            Product prod2 = new Product { Name = "new2", Price = 90m };
            Product prod3 = new Product {  Price = 100m };

            //Act
            var before_count = pr.GetAll().ToArray().Length;

            pr.Update(1, prod1);
            pr.Update(2, prod2);
            pr.Update(3, prod3);

            var after_count = pr.GetAll().ToArray().Length;

            //Assert
            Assert.Equal(before_count, after_count);
            Assert.Equal("newDesc1", pr.Find(1).Description);
            Assert.Same(mock.Object.Categories.First(c => c.Id == 2), pr.Find(1).CategoryNav);
            Assert.Equal("new2", pr.Find(2).Name);
            Assert.Equal(90m, pr.Find(2).Price);
            Assert.Equal(100m, pr.Find(3).Price);
            Assert.Throws<NotFoundException>( () => { pr.Update(100, new Product { Name = "Tom" }); });
            Assert.Throws<CustomRepoException>( () => { pr.Update(4, new Product { CategoryId = 10 }); });
        }

        [Fact]
        public void Can_Get_All()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category> { 
                new Category{ Id = 1, Name = "cat1"}, 
                new Category{ Id = 2, Name = "cat2"}, 
                new Category{ Id = 3, Name = "cat3"}, 
                new Category{ Id = 4, Name = "cat4"}, 
            });
            mock.Setup(m => m.Products).Returns(new List<Product> { 
                new Product {Id = 1, Name = "prod1", Description = "desc1", Price = 10m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 2, Name = "prod2", Description = "desc2", Price = 20m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 3, Name = "prod3", Description = "desc3", Price = 30m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 4, Name = "prod4", Description = "desc4", Price = 40m, CategoryId = 2, CategoryNav = mock.Object.Categories.First(c => c.Id == 2)},
                new Product {Id = 5, Name = "prod5", Description = "desc5", Price = 50m, CategoryId = 3, CategoryNav = mock.Object.Categories.First(c => c.Id == 3)},
                new Product {Id = 6, Name = "prod6", Description = "desc6", Price = 60m, CategoryId = 4, CategoryNav = mock.Object.Categories.First(c => c.Id == 4)},
            });

            ProductRepo pr = new ProductRepo(mock.Object);
            
            //Act
            var products = pr.GetAll().ToArray();

            //Assert
            Assert.True(products.Length == 6);
            Assert.Equal(2, products[1].Id);
            Assert.Equal("prod6", products[5].Name);
        }

        [Fact]
        public void Can_Find_By_Id()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category> {
                new Category{ Id = 1, Name = "cat1"},
                new Category{ Id = 2, Name = "cat2"},
                new Category{ Id = 3, Name = "cat3"},
                new Category{ Id = 4, Name = "cat4"},
            });
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "prod1", Description = "desc1", Price = 10m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 2, Name = "prod2", Description = "desc2", Price = 20m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 3, Name = "prod3", Description = "desc3", Price = 30m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 4, Name = "prod4", Description = "desc4", Price = 40m, CategoryId = 2, CategoryNav = mock.Object.Categories.First(c => c.Id == 2)},
                new Product {Id = 5, Name = "prod5", Description = "desc5", Price = 50m, CategoryId = 3, CategoryNav = mock.Object.Categories.First(c => c.Id == 3)},
                new Product {Id = 6, Name = "prod6", Description = "desc6", Price = 60m, CategoryId = 4, CategoryNav = mock.Object.Categories.First(c => c.Id == 4)},
            });

            ProductRepo pr = new ProductRepo(mock.Object);

            //Act
            var pr1 = pr.Find(1);
            var pr2 = pr.Find(2);
            var pr3 = pr.Find(10);

            //Assert
            Assert.Null(pr3);
            Assert.Equal(1, pr1.Id);
            Assert.Same(mock.Object.Products.First(p => p.Id == 2), pr2);
        }

        [Fact]
        public void Can_Find_By_Name()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category> {
                new Category{ Id = 1, Name = "cat1"},
                new Category{ Id = 2, Name = "cat2"},
                new Category{ Id = 3, Name = "cat3"},
                new Category{ Id = 4, Name = "cat4"},
            });
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "prod1", Description = "desc1", Price = 10m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 2, Name = "prod2", Description = "desc2", Price = 20m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 3, Name = "prod3", Description = "desc3", Price = 30m, CategoryId = 1, CategoryNav = mock.Object.Categories.First(c => c.Id == 1)},
                new Product {Id = 4, Name = "prod4", Description = "desc4", Price = 40m, CategoryId = 2, CategoryNav = mock.Object.Categories.First(c => c.Id == 2)},
                new Product {Id = 5, Name = "prod5", Description = "desc5", Price = 50m, CategoryId = 3, CategoryNav = mock.Object.Categories.First(c => c.Id == 3)},
                new Product {Id = 6, Name = "prod6", Description = "desc6", Price = 60m, CategoryId = 4, CategoryNav = mock.Object.Categories.First(c => c.Id == 4)},
            });

            ProductRepo pr = new ProductRepo(mock.Object);

            //Act
            var prod1 = pr.FindByName("prOd").ToArray();
            var prod2 = pr.FindByName("pRod4").ToArray();
            var prod3 = pr.FindByName("Tom");

            //Assert
            Assert.True(prod1.Length == 6);
            Assert.Contains(prod1, p => p.Id == 3);
            Assert.True(prod2.Length == 1);
            Assert.Same(mock.Object.Products.First(p => p.Id == 4), prod2[0]);
            Assert.Empty(prod3);
        }
    }
}
