using ConsoleShop.Model;
using System;
using Xunit;

namespace ConsoleShop.Tests.ModelTests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Default_Values_Empty()
        {
            //Arrange
            //Act
            var p = new Product();
            //Assert
            Assert.Equal(0, p.Id);
            Assert.Equal(0, p.CategoryId);
            Assert.Equal(0m, p.Price);
            Assert.Equal(String.Empty, p.Name);
            Assert.Equal(String.Empty, p.Description);
            Assert.Null(p.CategoryNav);
        }

        [Fact]
        public void Product_Can_Update()
        {
            //Arrange
            var p = new Product { Id = 1, CategoryId = 2, Price = 3m, Name = "Ball", Description = "Desc" };
            var c = new Category { Id = 1, Name = "cat1" };
            //Act
            p.Id = 10;
            p.CategoryId = 10;
            p.Price = 10m;
            p.Name = "Tom";
            p.Description = "Jerry";
            p.CategoryNav = c;
            //Assert
            Assert.Equal(10, p.Id);
            Assert.Equal(10, p.CategoryId);
            Assert.Equal(10m, p.Price);
            Assert.Equal("Tom", p.Name);
            Assert.Equal("Jerry", p.Description);
            Assert.Same(c, p.CategoryNav);
        }
    }
}
