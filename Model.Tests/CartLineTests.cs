using ConsoleShop.Model;
using Xunit;

namespace ConsoleShop.Tests.ModelTests
{
    public class CartLineTests
    {
        [Fact]
        public void CartLine_Default_Values_Empty()
        {
            //Arrange
            //Act
            var cl = new CartLine();
            Assert.Equal(0, cl.Id);
            Assert.Equal(0, cl.Quantity);
            Assert.Null(cl.Product);
        }

        [Fact]
        public void CartLine_Can_Update()
        {
            //Arrange
            var cl = new CartLine { Id = 5, Quantity = 5 };
            var p = new Product();
            //Act
            cl.Id = 10;
            cl.Quantity = 10;
            cl.Product = p;
            //Assert
            Assert.Equal(10, cl.Id);
            Assert.Equal(10, cl.Quantity);
            Assert.Same(p, cl.Product);
        }
    }
}
