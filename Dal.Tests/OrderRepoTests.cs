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
    public class OrderRepoTests
    {

        [Fact]
        public void Can_Add_Order()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Orders).Returns(new List<Order>()
            {
                new Order { Id = 1, Name = "order1", Address = "addr1", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 2, Name = "order2", Address = "addr2", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 3, Name = "order3", Address = "addr3", UserId = 2, Status = OrderStatus.New},
                new Order { Id = 4, Name = "order4", Address = "addr4", UserId = 2, Status = OrderStatus.New},
            });
            mock.Setup(m => m.Users).Returns(new List<User>() {
                new User {Id = 1, Name = "user1"},
                new User {Id = 2, Name = "user2"},
                new User {Id = 3, Name = "user3"},
                new User {Id = 4, Name = "user4"},
            });

            OrderRepo or = new OrderRepo(mock.Object);
            Order o1 = new Order { Name = "new1", Address = "newAdd1", UserId = 3 };
            Order o2 = new Order { Name = "new2", Address = "newAdd2", UserId = 3 };
            Order o3 = new Order { Name = "new3", Address = "newAdd3", UserId = 4 };

            //Act
            var before_count = or.GetAll().ToArray().Length;

            or.Add(o1);
            or.Add(o2);
            or.Add(o3);

            var after_count = or.GetAll().ToArray().Length;
            var users = mock.Object.Users.ToArray();

            //Assert
            Assert.Equal(4, before_count);
            Assert.Equal(7, after_count);
            Assert.Equal(7, or.FindByUser(4).First().Id);
            Assert.True(users[2].Orders.Count == 2);
            Assert.True(users[2].Orders.Contains(o1));
            Assert.True(users[3].Orders.Contains(o3));

        }

        [Fact]
        public void Can_Find_Order_By_Id()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Orders).Returns(new List<Order>()
            {
                new Order { Id = 1, Name = "order1", Address = "addr1", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 2, Name = "order2", Address = "addr2", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 3, Name = "order3", Address = "addr3", UserId = 2, Status = OrderStatus.New},
                new Order { Id = 4, Name = "order4", Address = "addr4", UserId = 2, Status = OrderStatus.New},
            });
            mock.Setup(m => m.Users).Returns(new List<User>() {
                new User {Id = 1, Name = "user1"},
                new User {Id = 2, Name = "user2"},
                new User {Id = 3, Name = "user3"},
                new User {Id = 4, Name = "user4"},
            });

            OrderRepo or = new OrderRepo(mock.Object);

            //Act
            Order o1 = or.Find(1);
            Order o2 = or.Find(1);
            Order o3 = or.Find(2);
            Order o4 = or.Find(3);
            Order o5 = or.Find(10);

            //Assert
            Assert.Equal(o1, o2);
            Assert.Equal(2, o3.Id);
            Assert.Same(mock.Object.Orders[2], o4);
            Assert.Null(o5);
        }

        [Fact]
        public void Can_Find_Order_By_UserId()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Orders).Returns(new List<Order>()
            {
                new Order { Id = 1, Name = "order1", Address = "addr1", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 2, Name = "order2", Address = "addr2", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 3, Name = "order3", Address = "addr3", UserId = 2, Status = OrderStatus.New},
                new Order { Id = 4, Name = "order4", Address = "addr4", UserId = 2, Status = OrderStatus.New},
            });
            mock.Setup(m => m.Users).Returns(new List<User>() {
                new User {Id = 1, Name = "user1"},
                new User {Id = 2, Name = "user2"},
                new User {Id = 3, Name = "user3"},
                new User {Id = 4, Name = "user4"},
            });

            OrderRepo or = new OrderRepo(mock.Object);

            //Act
            Order[] o1 = or.FindByUser(1).ToArray();
            Order[] o2 = or.FindByUser(2).ToArray();
            var o3 = or.FindByUser(3);

            //Assert
            Assert.True(o1.Length == 2);
            Assert.Equal(1, o1[0].Id);
            Assert.Same(mock.Object.Orders[1], o1[1]);
            Assert.True(o2.Length == 2);
            Assert.Equal(3, o2[0].Id);
            Assert.Same(mock.Object.Orders[3], o2[1]);
            Assert.Empty(o3);
        }

        [Fact]
        public void Return_All_Orders()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Orders).Returns(new List<Order>()
            {
                new Order { Id = 1, Name = "order1", Address = "addr1", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 2, Name = "order2", Address = "addr2", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 3, Name = "order3", Address = "addr3", UserId = 2, Status = OrderStatus.New},
                new Order { Id = 4, Name = "order4", Address = "addr4", UserId = 2, Status = OrderStatus.New},
            });
            mock.Setup(m => m.Users).Returns(new List<User>() {
                new User {Id = 1, Name = "user1"},
                new User {Id = 2, Name = "user2"},
                new User {Id = 3, Name = "user3"},
                new User {Id = 4, Name = "user4"},
            });

            OrderRepo or = new OrderRepo(mock.Object);

            //Act
            Order[] orders = or.GetAll().ToArray();

            //Assert
            Assert.True(orders.Length == 4);
            Assert.Equal("order1", orders[0].Name);
            Assert.Equal(2, orders[1].Id);
            Assert.Same(mock.Object.Orders[2], orders[2]);
        }

        [Fact]
        public void Can_Set_Status()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Orders).Returns(new List<Order>()
            {
                new Order { Id = 1, Name = "order1", Address = "addr1", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 2, Name = "order2", Address = "addr2", UserId = 1, Status = OrderStatus.New},
                new Order { Id = 3, Name = "order3", Address = "addr3", UserId = 2, Status = OrderStatus.New},
                new Order { Id = 4, Name = "order4", Address = "addr4", UserId = 2, Status = OrderStatus.New},
            });
            mock.Setup(m => m.Users).Returns(new List<User>() {
                new User {Id = 1, Name = "user1"},
                new User {Id = 2, Name = "user2"},
                new User {Id = 3, Name = "user3"},
                new User {Id = 4, Name = "user4"},
            });

            OrderRepo or = new OrderRepo(mock.Object);

            //Act
            or.SetStatus(1, OrderStatus.Sent);
            or.SetStatus(2, OrderStatus.CancelledByUser);
            or.SetStatus(3, OrderStatus.PaymentRecieved);
            or.SetStatus(4, OrderStatus.CancelledByAdministrator);

            var orders = or.GetAll().ToArray();

            //Assert
            Assert.Throws<NotFoundException>(() => { or.SetStatus(10, OrderStatus.PaymentRecieved); });
            Assert.Equal(OrderStatus.Sent, orders[0].Status);
            Assert.Equal(OrderStatus.CancelledByUser, orders[1].Status);
            Assert.Equal(OrderStatus.PaymentRecieved, orders[2].Status);
            Assert.Equal(OrderStatus.CancelledByAdministrator, orders[3].Status);
        }
    }
}
