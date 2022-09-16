using ConsoleShop.Model;
using System;
using Xunit;

namespace ConsoleShop.Tests.ModelTests
{
    public class CategoryTests
    {
        [Fact]
        public void Category_Default_Values_Empty()
        {
            //Arrange
            //Act
            var c = new Category();
            Assert.Equal(0, c.Id);
            Assert.Equal(String.Empty, c.Name);
        }

        [Fact]
        public void Category_Can_Update()
        {
            //Arrange
            var c = new Category { Id = 5, Name = "Tom" };
            //Act
            c.Id = 10;
            c.Name = "Jerry";
            //Assert
            Assert.Equal(10, c.Id);
            Assert.Equal("Jerry", c.Name);
        }
    }
}
