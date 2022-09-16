using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using ConsoleShop.Bll.Model;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class CartControllerTests : BaseTest
    {
        [Fact]
        public void Can_Add_To_Cart()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();
            var mockFactory3 = GetResultFactory();

            Mock<IProductRepo> prodRepo = new Mock<IProductRepo>();
            prodRepo.Setup(pr => pr.Find(It.Is<int>(i => i == 3))).Returns(new Product { Name = "test" });
            
            Mock<ISession> session = new Mock<ISession>();
            Mock<ICart> cart = new Mock<ICart>();
            session.Setup(s => s.Cart).Returns(cart.Object);
            session.Setup(s => s.User).Returns(new User { Name = "Tom" });

            //Act
            IActionResult result1 = new CartController(prodRepo.Object, null, session.Object, mockFactory1.Object)
                .AddToCart(3, -2);
            IActionResult result2 = new CartController(prodRepo.Object, null, session.Object, mockFactory2.Object)
                .AddToCart(10, -2);
            IActionResult result3 = new CartController(prodRepo.Object, null, session.Object, mockFactory3.Object)
                .AddToCart(3, 1);

            //Assert
            cart.Verify(c => c.AddItem(It.IsAny<Product>(), It.Is<int>(i => i > 0)), Times.Once());
            Assert.Equal(ActionResult.Warning, result1.Result);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Equal(ActionResult.Succes, result3.Result);
        }

        [Fact]
        public void Can_Show_Cart()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();

            Mock<ISession> session1 = new Mock<ISession>();
            Mock<ICart> cart1 = new Mock<ICart>();
            var cl1 = new CartLine { Id = 1, Product = new Product { Name = "1" }, Quantity = 1 };
            var cl2 = new CartLine { Id = 1, Product = new Product { Name = "2" }, Quantity = 2 };
            cart1.Setup(c => c.Lines).Returns(new List<CartLine>{ cl1, cl2});
            session1.Setup(s => s.Cart).Returns(cart1.Object);

            Mock<ISession> session2 = new Mock<ISession>();
            Mock<ICart> cart2 = new Mock<ICart>();
            cart2.Setup(c => c.Lines).Returns(new List<CartLine>{});
            session2.Setup(s => s.Cart).Returns(cart2.Object);

            //Act
            IActionResult result1 = new CartController(null, null, session1.Object, resultFactory1.Object)
                .ShowCart();
            IActionResult result2 = new CartController(null, null, session2.Object, resultFactory2.Object)
                .ShowCart();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Contains(cl1, result1.ResultBody);
            Assert.Contains(cl2, result1.ResultBody);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Clear_Cart()
        {
            //Arrange
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();

            Mock<ISession> session1 = new Mock<ISession>();
            Mock<ICart> cart1 = new Mock<ICart>();
            var cl1 = new CartLine { Id = 1, Product = new Product { Name = "1" }, Quantity = 1 };
            var cl2 = new CartLine { Id = 1, Product = new Product { Name = "2" }, Quantity = 2 };
            cart1.Setup(c => c.Lines).Returns(new List<CartLine> { cl1, cl2 });
            session1.Setup(s => s.Cart).Returns(cart1.Object);

            Mock<ISession> session2 = new Mock<ISession>();
            Mock<ICart> cart2 = new Mock<ICart>();
            cart2.Setup(c => c.Lines).Returns(new List<CartLine> { });
            session2.Setup(s => s.Cart).Returns(cart2.Object);

            //Act
            IActionResult result1 = new CartController(null, null, session1.Object, resultFactory1.Object).
                ClearCart();
            IActionResult result2 = new CartController(null, null, session2.Object, resultFactory2.Object).
                ClearCart();

            //Assert
            cart1.Verify(c => c.Clear(), Times.Once());
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
        }

        [Fact]
        public void Can_Make_Order()
        {
            //Assert
            var resultFactory1 = GetResultFactory();
            var resultFactory2 = GetResultFactory();
            var resultFactory3 = GetResultFactory();

            Mock<ISession> session1 = new Mock<ISession>();
            Mock<ICart> cart1 = new Mock<ICart>();
            var cl1 = new CartLine { Id = 1, Product = new Product { Name = "1" }, Quantity = 1 };
            var cl2 = new CartLine { Id = 1, Product = new Product { Name = "2" }, Quantity = 2 };
            cart1.Setup(c => c.Lines).Returns(new List<CartLine> { cl1, cl2 });
            session1.Setup(s => s.Cart).Returns(cart1.Object);
            session1.Setup(s => s.User).Returns(new User { Id = 1, Name = "name"});

            Mock<ISession> session2 = new Mock<ISession>();
            Mock<ICart> cart2 = new Mock<ICart>();
            cart2.Setup(c => c.Lines).Returns(new List<CartLine> { });
            session2.Setup(s => s.Cart).Returns(cart2.Object);

            Mock<IOrderRepo> orderRepo = new Mock<IOrderRepo>();

            //Act
            IActionResult result1 = new CartController(null, orderRepo.Object, session1.Object, resultFactory1.Object)
                .MakeOrder("Tom", "Jerry");
            IActionResult result2 = new CartController(null, orderRepo.Object, session2.Object, resultFactory2.Object)
                .MakeOrder("Tom", "");
            IActionResult result3 = new CartController(null, orderRepo.Object, session2.Object, resultFactory3.Object)
                .MakeOrder("Tom", "Jerry");

            //Assert
            cart1.Verify(c => c.Clear(), Times.Once());
            cart2.Verify(c => c.Clear(), Times.Never());
            orderRepo.Verify(or => or.Add(It.IsAny<Order>()), Times.Once());
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Equal(ActionResult.NotFound, result3.Result);
        }
    }
}
