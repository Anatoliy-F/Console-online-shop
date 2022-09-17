using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.View.Factories;
using Moq;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class ViewFactoriesTests
    {
        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_CartView(ActionResult actResult, string msg)
        {
            //Arrange
            CartLine cl1 = new CartLine { Id = 1, Product = new Product { Name = "name1"}, Quantity = 2 };
            CartLine cl2 = new CartLine { Id = 2, Product = new Product { Name = "name2"}, Quantity = 1 };

            //Act
            IActionResult result1 = new CartViewFactory().GetResultRender(actResult, msg, new[] {cl1, cl2});
            IActionResult result2 = new CartViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(cl1, result1.ResultBody);
            Assert.Contains(cl2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_CategoryView(ActionResult actResult, string msg)
        {
            //Arrange
            Category c1 = new Category { Name = "cat1" };
            Category c2 = new Category { Name = "cat2" };

            //Act
            IActionResult result1 = new CategoryViewFactory().GetResultRender(actResult, msg, new[] { c1, c2 });
            IActionResult result2 = new CategoryViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(c1, result1.ResultBody);
            Assert.Contains(c2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_ErrorView(ActionResult actResult, string msg)
        {
            //Arrange
            //Act
            IActionResult result = new ErrorViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result.Result);
            Assert.Equal(msg, result.Message);
            Assert.Null(result.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_HelpView(ActionResult actResult, string msg)
        {
            //Arrange
            Mock<ICommand> c1 = new Mock<ICommand>();
            Mock<ICommand> c2 = new Mock<ICommand>();

            //Act
            IActionResult result1 = new HelpViewFactory().GetResultRender(actResult, msg, new[] { c1.Object, c2.Object });
            IActionResult result2 = new HelpViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(c1.Object, result1.ResultBody);
            Assert.Contains(c2.Object, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_LoginView(ActionResult actResult, string msg)
        {
            //Arrange
            User u1 = new User { Name = "user1" };
            User u2 = new User { Name = "user2" };

            //Act
            IActionResult result1 = new LoginViewFactory().GetResultRender(actResult, msg, new[] { u1, u2 });
            IActionResult result2 = new LoginViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(u1, result1.ResultBody);
            Assert.Contains(u2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_OrderView(ActionResult actResult, string msg)
        {
            //Arrange
            Order o1 = new Order { Name = "order1" };
            Order o2 = new Order { Name = "order2" };

            //Act
            IActionResult result1 = new OrderViewFactory().GetResultRender(actResult, msg, new[] { o1, o2 });
            IActionResult result2 = new OrderViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(o1, result1.ResultBody);
            Assert.Contains(o2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Succes")]
        [InlineData(ActionResult.Warning, "Warning")]
        [InlineData(ActionResult.Error, "Error")]
        [InlineData(ActionResult.NotFound, "NotFound")]
        public void Return_ProductView(ActionResult actResult, string msg)
        {
            //Arrange
            Product p1 = new Product { Name = "pord1" };
            Product p2 = new Product { Name = "pord2" };

            //Act
            IActionResult result1 = new ProductViewFactory().GetResultRender(actResult, msg, new[] { p1, p2 });
            IActionResult result2 = new ProductViewFactory().GetResultRender(actResult, msg);

            //Arrange
            Assert.Equal(actResult, result1.Result);
            Assert.Equal(actResult, result2.Result);
            Assert.Equal(msg, result1.Message);
            Assert.Equal(msg, result2.Message);
            Assert.Contains(p1, result1.ResultBody);
            Assert.Contains(p2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }
    }
}
