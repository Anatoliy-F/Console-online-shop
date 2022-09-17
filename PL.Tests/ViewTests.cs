using ConsoleApp1.Tests.Helper;
using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.View;
using Moq;
using System;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class ViewTests
    {
        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test_Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4", "Name1_4", "Name2_4")]
        public void Print_Cart_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            CartLine cl1 = new CartLine { Id = 1, Product = new Product { Name = name1 }, Quantity = 2 };
            CartLine cl2 = new CartLine { Id = 2, Product = new Product { Name = name2 }, Quantity = 1 };

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new CartView(actRes, text, new[] { cl1, cl2 });

            //Assert
            using(var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test_Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4", "Name1_4", "Name2_4")]
        public void Print_Category_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            Category c1 = new Category { Name = name1 };
            Category c2 = new Category { Name = name2 };

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new CategoryView(actRes, text, new[] { c1, c2 });

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2")]
        [InlineData(ActionResult.Error, "Test_Text_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4")]
        public void Print_Error_View(ActionResult actRes, string text)
        {
            //Arrange
            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new ErrorView(actRes, text);

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test Pattern: Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test Pattern: Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test Pattern: Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test Pattern: Text_4", "Name1_4", "Name2_4")]
        public void Print_Help_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            Mock<ICommand> c1 = new Mock<ICommand>();
            c1.Setup(c1 => c1.Name).Returns(name1);
            Mock<ICommand> c2 = new Mock<ICommand>();
            c2.Setup(c2 => c2.Name).Returns(name2);

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new HelpView(actRes, text, new[] { c1.Object, c2.Object });

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test_Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4", "Name1_4", "Name2_4")]
        public void Print_Login_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            User u1 = new User { Name = name1 };
            User u2 = new User { Name = name2 };

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new LoginView(actRes, text, new[] { u1, u2 });

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test_Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4", "Name1_4", "Name2_4")]
        public void Print_Order_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            Order o1 = new Order { Name = name1 };
            Order o2 = new Order { Name = name2 };

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new OrderView(actRes, text, new[] { o1, o2 });

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Theory]
        [InlineData(ActionResult.Succes, "Test_Text_1", "Name1_1", "Name2_1")]
        [InlineData(ActionResult.Warning, "Test_Text_2", "Name1_2", "Name2_2")]
        [InlineData(ActionResult.Error, "Test_Text_3", "Name1_3", "Name2_3")]
        [InlineData(ActionResult.NotFound, "Test_Text_4", "Name1_4", "Name2_4")]
        public void Print_Product_View(ActionResult actRes, string text, string name1, string name2)
        {
            //Arrange
            Product p1 = new Product()
            {
                Id = 1,
                CategoryId = 1,
                Name = name1,
                Description = "Real Madrid #7",
                Price = 100m,
                CategoryNav = new Category { Name = "name"},
            };
            Product p2 = new Product()
            {
                Id = 2,
                CategoryId = 1,
                Name = "name",
                Description = "Retro-style soccer ball",
                Price = 20m,
                CategoryNav = new Category { Name = name2 },
            };

            var currentConsoleOut = Console.Out;

            //Act
            IActionResult cv = new ProductView(actRes, text, new[] { p1, p2 });

            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                cv.RenderResult();
                Assert.Contains(text, consoleOutput.GetOuput());
                Assert.Contains(actRes.ToString(), consoleOutput.GetOuput());
                Assert.Contains(name1, consoleOutput.GetOuput());
                Assert.Contains(name2, consoleOutput.GetOuput());
            }
            Assert.Equal(currentConsoleOut, Console.Out);
        }
    }
}
