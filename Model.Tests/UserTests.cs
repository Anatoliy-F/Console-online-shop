using ConsoleShop.Model;
using System;
using Xunit;

namespace ConsoleShop.Tests.ModelTests
{
    public class UserTests
    {
        [Fact]
        public void User_Default_Values_Empty()
        {
            //Arrange
            //Act
            var u = new User();
            //Assert
            Assert.Equal(0, u.Id);
            Assert.Equal(String.Empty, u.Name);
            Assert.Equal(UserRole.Guest, u.Role);
            Assert.Equal(String.Empty, u.PasswordHash);
            Assert.Empty(u.Orders);
        }

        [Fact]
        public void User_Can_Update()
        {
            //Arrange
            var u = new User { Id = 5, Name = "Tom", Role = UserRole.Guest, PasswordHash = "1234567890" };
            var o1 = new Order { Name = "o1" };
            var o2 = new Order { Name = "o2" };
            //Act
            u.Id = 10;
            u.Name = "Jerry";
            u.Role = UserRole.Administrator;
            u.PasswordHash = "0987654321";
            u.Orders.Add(o1);
            u.Orders.Add(o2);
            //Assert
            Assert.Equal(10, u.Id);
            Assert.Equal("Jerry", u.Name);
            Assert.Equal(UserRole.Administrator, u.Role);
            Assert.Equal("0987654321", u.PasswordHash);
            Assert.Collection(u.Orders, o => Assert.Same(o, o1), o => Assert.Same(o, o2));
        }
    }
}
