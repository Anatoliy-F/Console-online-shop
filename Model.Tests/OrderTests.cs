using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConsoleShop.Tests.ModelTests
{
    public class OrderTests
    {
        [Fact]
        public void Order_Default_Values_Empty()
        {
            //Arrange
            //Act
            var o = new Order();
            //Assert
            Assert.Equal(0, o.Id);
            Assert.Equal(String.Empty, o.Name);
            Assert.Equal(0, o.UserId);
            Assert.Equal(String.Empty, o.Address);
            Assert.Equal(OrderStatus.New, o.Status);
            Assert.Null(o.Lines);
        }

        [Fact]
        public void Order_Can_Update()
        {
            //Arrange
            var o = new Order { Id = 5, Name = "Tom", UserId = 5, Address = "addr1" };
            List<CartLine> cl = new List<CartLine> { new CartLine() { Id = 3 }, new CartLine() { Id = 6 } };
            //Act
            o.Id = 10;
            o.Name = "Jerry";
            o.UserId = 10;
            o.Address = "addr5";
            o.Status = OrderStatus.Completed;
            o.Lines = cl;
            //Assert
            Assert.Equal(10, o.Id);
            Assert.Equal("Jerry", o.Name);
            Assert.Equal(10, o.UserId);
            Assert.Equal("addr5", o.Address);
            Assert.Equal(OrderStatus.Completed, o.Status);
            Assert.Same(cl, o.Lines);
        }
    }
}
