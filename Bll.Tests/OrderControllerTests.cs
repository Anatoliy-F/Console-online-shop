using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class OrderControllerTests : BaseTest
    {
        [Fact]
        public void Can_Show_User_Orders()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();

            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(new User { Id = 3 });

            Order o1 = new Order { Id = 1, UserId = 3 };
            Order o2 = new Order { Id = 2, UserId = 3 };
            Mock<IOrderRepo> orderRepo1 = new Mock<IOrderRepo>();
            orderRepo1.Setup(or => or.FindByUser(It.Is<int>(i => i == 3)))
                .Returns(new List<Order> { o1, o2 });
            Mock<IOrderRepo> orderRepo2 = new Mock<IOrderRepo>();

            //Act
            IActionResult result1 = new OrderController(orderRepo1.Object, resultFactory1.Object, session.Object)
                .ShowOrders();
            IActionResult result2 = new OrderController(orderRepo2.Object, resultFactory2.Object, session.Object)
                .ShowOrders();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Contains(o1, result1.ResultBody);
            Assert.Contains(o2, result1.ResultBody);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Show_All_Orders()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();

            Order o1 = new Order { Id = 1, UserId = 1 };
            Order o2 = new Order { Id = 2, UserId = 2 };
            Mock<IOrderRepo> orderRepo1 = new Mock<IOrderRepo>();
            orderRepo1.Setup(or => or.GetAll())
                .Returns(new List<Order> { o1, o2 });
            Mock<IOrderRepo> orderRepo2 = new Mock<IOrderRepo>();

            //Act
            IActionResult result1 = new OrderController(orderRepo1.Object, resultFactory1.Object, null)
                .ShowAllOrders();
            IActionResult result2 = new OrderController(orderRepo2.Object, resultFactory2.Object, null)
                .ShowAllOrders();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Contains(o1, result1.ResultBody);
            Assert.Contains(o2, result1.ResultBody);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void User_Can_Confirm_His_Order()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();
            var resultFactory3 = GetResultFactory();

            Mock<IOrderRepo> orderRepo = new Mock<IOrderRepo>();
            Order o1 = new Order { Id = 3, UserId = 3 };
            Order o2 = new Order { Id = 4, UserId = 3 };
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 3)))
                .Returns(o1);
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 4)))
                .Returns(o2);
            orderRepo.Setup(or => or.SetStatus(It.Is<int>(i => i != 3), It.IsAny<OrderStatus>()))
                .Throws(new NotFoundException("ex"));

            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(new User { Id = 3 });

            //Act
            IActionResult result1 = new OrderController(orderRepo.Object, resultFactory1.Object, session.Object)
                .ConfirmReceipt(3);
            IActionResult result2 = new OrderController(orderRepo.Object, resultFactory2.Object, session.Object)
                .ConfirmReceipt(4);
            IActionResult result3 = new OrderController(orderRepo.Object, resultFactory3.Object, session.Object)
                .ConfirmReceipt(5);

            //Assert
            orderRepo.Verify(or => or.Find(It.IsAny<int>()), Times.Exactly(3));
            orderRepo.Verify(or => or.SetStatus(It.IsAny<int>(), It.Is<OrderStatus>(o => o == OrderStatus.Received)),
                Times.Exactly(2));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Same(o1, result1.ResultBody.First());
            Assert.Equal(ActionResult.Error, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.NotFound, result3.Result);
            Assert.Null(result3.ResultBody);
        }

        [Fact]
        public void Can_Cancell_Order()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();
            var resultFactory3 = GetResultFactory();
            var resultFactory4 = GetResultFactory();

            Mock<IOrderRepo> orderRepo = new Mock<IOrderRepo>();
            Order o1 = new Order { Id = 3, UserId = 3 };
            Order o2 = new Order { Id = 4, UserId = 3 };
            Order o3 = new Order { Id = 5, UserId = 3, Status = OrderStatus.Received };
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 3)))
                .Returns(o1);
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 4)))
                .Returns(o2);
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 5)))
                .Returns(o3);
            orderRepo.Setup(or => or.SetStatus(It.Is<int>(i => i == 4), It.IsAny<OrderStatus>()))
                .Throws(new NotFoundException("ex"));

            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(new User { Id = 3 });

            //Act
            IActionResult result1 = new OrderController(orderRepo.Object, resultFactory1.Object, session.Object)
                .CancellOrder(3);
            IActionResult result2 = new OrderController(orderRepo.Object, resultFactory2.Object, session.Object)
                .CancellOrder(4);
            IActionResult result3 = new OrderController(orderRepo.Object, resultFactory3.Object, session.Object)
                .CancellOrder(5);
            IActionResult result4 = new OrderController(orderRepo.Object, resultFactory4.Object, session.Object)
                .CancellOrder(6);

            //Assert
            orderRepo.Verify(or => or.Find(It.IsAny<int>()), Times.Exactly(4));
            orderRepo.Verify(or => or.SetStatus(It.IsAny<int>(), It.Is<OrderStatus>(o => o == OrderStatus.CancelledByUser)),
                Times.Exactly(2));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Same(o1, result1.ResultBody.First());
            Assert.Equal(ActionResult.Error, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Null(result3.ResultBody);
            Assert.Equal(ActionResult.NotFound, result4.Result);
            Assert.Null(result4.ResultBody);
        }

        [Fact]
        public void Can_Change_Order_Status()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();
            var resultFactory3 = GetResultFactory();
            var resultFactory4 = GetResultFactory();

            Mock<IOrderRepo> orderRepo = new Mock<IOrderRepo>();
            Order o1 = new Order { Id = 3, UserId = 3 };
            orderRepo.Setup(or => or.Find(It.Is<int>(i => i == 3)))
                .Returns(o1);
            orderRepo.Setup(or => or.SetStatus(It.Is<int>(i => i == 6), It.IsAny<OrderStatus>()))
                .Throws(new NotFoundException("ex"));

            //Act
            IActionResult result1 = new OrderController(orderRepo.Object, resultFactory1.Object, null)
                .ChangeOrderStatus(3, OrderStatus.Sent);
            IActionResult result2 = new OrderController(orderRepo.Object, resultFactory2.Object, null)
                .ChangeOrderStatus(4, OrderStatus.New);
            IActionResult result3 = new OrderController(orderRepo.Object, resultFactory3.Object, null)
                .ChangeOrderStatus(5, OrderStatus.CancelledByUser);
            IActionResult result4 = new OrderController(orderRepo.Object, resultFactory4.Object, null)
                .ChangeOrderStatus(6, OrderStatus.PaymentRecieved);


            //Assert
            orderRepo.Verify(or => or.Find(It.IsAny<int>()), Times.Once());
            orderRepo.Verify(or => or.SetStatus(It.IsAny<int>(), It.IsAny<OrderStatus>()),
                Times.Exactly(2));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Same(o1, result1.ResultBody.First());
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Null(result3.ResultBody);
            Assert.Equal(ActionResult.Error, result4.Result);
            Assert.Null(result4.ResultBody);
        }
    }
}
