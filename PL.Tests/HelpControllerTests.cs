using ConsoleApp1.Tests.Helper;
using ConsoleShop.Commands.Base;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class HelpControllerTests : BaseTest
    {
        [Fact]
        public void Can_Show_All_Commands()
        {
            //Arrange
            var responseFactory1 = getResultFactory();
            var responseFactory2 = getResultFactory();
            var responseFactory3 = getResultFactory();

            Mock<ISession> session1 = new Mock<ISession>();
            session1.Setup(s => s.User).Returns(new User { Role = UserRole.Administrator });
            Mock<ISession> session2 = new Mock<ISession>();
            session2.Setup(s => s.User).Returns(new User { Role = UserRole.RegisteredUser });
            Mock<ISession> session3 = new Mock<ISession>();
            session3.Setup(s => s.User).Returns(new User { Role = UserRole.Guest });

            Mock<ICommand> command1 = new Mock<ICommand>();
            command1.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator });
            Mock<ICommand> command2 = new Mock<ICommand>();
            command2.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator, UserRole.RegisteredUser });
            Mock<ICommand> command3 = new Mock<ICommand>();
            command3.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator, UserRole.RegisteredUser,
                UserRole.Guest});

            IEnumerable<ICommand> commands = new[] { command1.Object, command2.Object, command3.Object };

            //Act
            IActionResult result1 = new HelpController(commands, session1.Object, responseFactory1.Object)
                .ViewCommands();
            IActionResult result2 = new HelpController(commands, session2.Object, responseFactory2.Object)
                .ViewCommands();
            IActionResult result3 = new HelpController(commands, session3.Object, responseFactory3.Object)
                .ViewCommands();

            //Arrange
            Assert.Contains(command1.Object, result1.ResultBody);
            Assert.Contains(command2.Object, result1.ResultBody);
            Assert.Contains(command3.Object, result1.ResultBody);
            Assert.Equal(3, result1.ResultBody.Count());
            Assert.Contains(command2.Object, result2.ResultBody);
            Assert.Contains(command3.Object, result2.ResultBody);
            Assert.Equal(2, result2.ResultBody.Count());
            Assert.Contains(command3.Object, result3.ResultBody);
            Assert.Single(result3.ResultBody);
        }

        [Fact]
        public void Can_Show_Command()
        {
            //Arrange
            var responseFactory1 = getResultFactory();
            var responseFactory2 = getResultFactory();
            var responseFactory3 = getResultFactory();

            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(new User { Role = UserRole.RegisteredUser });

            Mock<ICommand> command1 = new Mock<ICommand>();
            command1.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator });
            command1.Setup(c => c.Name).Returns("First");
            Mock<ICommand> command2 = new Mock<ICommand>();
            command2.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator, UserRole.RegisteredUser });
            command2.Setup(c => c.Name).Returns("Second");
            Mock<ICommand> command3 = new Mock<ICommand>();
            command3.Setup(c => c.Roles).Returns(new[] { UserRole.Administrator, UserRole.RegisteredUser,
                UserRole.Guest});
            command3.Setup(c => c.Name).Returns("Third");

            IEnumerable<ICommand> commands = new[] { command1.Object, command2.Object, command3.Object };

            //Act
            IActionResult result1 = new HelpController(commands, session.Object, responseFactory1.Object).ShowCommand("First");
            IActionResult result2 = new HelpController(commands, session.Object, responseFactory2.Object).ShowCommand("SeCoND");
            IActionResult result3 = new HelpController(commands, session.Object, responseFactory3.Object).ShowCommand("Tom");

            //Assert
            Assert.Contains(command2.Object, result2.ResultBody);
            Assert.Equal(ActionResult.Succes, result2.Result);
            Assert.Equal(ActionResult.Warning, result1.Result);
            Assert.Equal(ActionResult.Warning, result3.Result);
        }
    }
}
