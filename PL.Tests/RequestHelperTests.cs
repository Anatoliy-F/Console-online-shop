using ConsoleShop.Commands.Base;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class RequestHelperTests
    {
        [Fact]
        public void Can_Parse_Request()
        {
            //Arrange
            Mock<ICommand> command1 = new Mock<ICommand>();
            command1.Setup(c => c.Name).Returns("Help");
            Mock<ICommand> command2 = new Mock<ICommand>();
            command2.Setup(c => c.Name).Returns("Void");
            Mock<ICommand> command3 = new Mock<ICommand>();
            command3.Setup(c => c.Name).Returns("Action");
            Mock<ICommand> command4 = new Mock<ICommand>();
            command4.Setup(c => c.Name).Returns("");

            IEnumerable<ICommand> commands = new[] { command1.Object, command2.Object, command3.Object, command4.Object };

            RequestHelper helper = new RequestHelper(commands);

            //Act
            (ICommand result1, string resultArgs1) = helper.ReadRequest("hElp please");
            (ICommand result2, string resultArgs2) = helper.ReadRequest("hElp");
            (ICommand result3, string resultArgs3) = helper.ReadRequest("vOId  3 5 7");
            (ICommand result4, string resultArgs4) = helper.ReadRequest("Action  \"sd sd\" \"as ds\"");
            (ICommand result5, string resultArgs5) = helper.ReadRequest("please");
            (ICommand result6, string resultArgs6) = helper.ReadRequest(" ");

            //Assert
            Assert.Same(command1.Object, result1);
            Assert.Equal("please", resultArgs1);
            Assert.Same(command1.Object, result2);
            Assert.Equal("", resultArgs2);
            Assert.Same(command2.Object, result3);
            Assert.Equal(" 3 5 7", resultArgs3);
            Assert.Same(command3.Object, result4);
            Assert.Equal(" \"sd sd\" \"as ds\"", resultArgs4);
            Assert.Same(command4.Object, result5);
            Assert.Equal("Unknow command, please use help", resultArgs5);
            Assert.Same(command4.Object, result6);
            Assert.Equal("Unknow command, please use help", resultArgs6);
        }
    }
}
