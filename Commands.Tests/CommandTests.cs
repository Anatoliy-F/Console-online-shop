using ConsoleApp1.Tests.Helper;
using ConsoleShop.Commands;
using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp1.Tests
{
    public class CommandTests : BaseTest
    {
        [Theory]
        [InlineData(new[] { UserRole.Guest }, UserRole.RegisteredUser, "Access Denied!")]
        [InlineData(new[] { UserRole.Guest }, UserRole.Administrator, "Access Denied!")]
        [InlineData(new[] { UserRole.Guest }, UserRole.Guest, "Invoked")]
        [InlineData(new[] { UserRole.Guest, UserRole.RegisteredUser }, UserRole.RegisteredUser, "Invoked")]
        [InlineData(new[] { UserRole.Guest, UserRole.RegisteredUser }, UserRole.Guest, "Invoked")]
        [InlineData(new[] { UserRole.RegisteredUser }, UserRole.RegisteredUser, "Invoked")]
        [InlineData(new[] { UserRole.Guest, UserRole.RegisteredUser }, UserRole.Administrator, "Access Denied!")]
        [InlineData(new[] { UserRole.System }, UserRole.Administrator, "Access Denied!")]
        [InlineData(new[] { UserRole.Administrator }, UserRole.Administrator, "Invoked")]
        public void Can_Check_Permissions(IEnumerable<UserRole> roles, UserRole role, string expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command("test", "test", roles, () =>
            {
                return response.GetResultRender(ActionResult.Succes, "Invoked");
            });

            User user = new User { Role = role };

            //Act
            IActionResult result = command.Execute(user, "");

            //Assert
            Assert.Contains(expected, result.Message);
        }

        [Theory]
        [InlineData("", ActionResult.Succes)]
        [InlineData(" ", ActionResult.Succes)]
        [InlineData("  ", ActionResult.Succes)]
        [InlineData("a d", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData("4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]

        public void Validate_Signature(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command("test", "test", new[] { UserRole.RegisteredUser },
                () =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("asd dsa", ActionResult.Succes)]
        [InlineData("asd  2", ActionResult.Succes)]
        [InlineData(" asd sd23 ", ActionResult.Succes)]
        [InlineData("\"asd dsa asd\"", ActionResult.Succes)]
        [InlineData("", ActionResult.Error)]
        [InlineData("  ", ActionResult.Error)]
        public void Validate_Signature_Str_Arg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new CommandStrArg("test", "test", new[] { UserRole.RegisteredUser },
                (i) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("asd dsa asd", ActionResult.Succes)]
        [InlineData("asd 23sd 2", ActionResult.Succes)]
        [InlineData(" asd sd23  d3", ActionResult.Succes)]
        [InlineData(" 1  43 7", ActionResult.Succes)]
        [InlineData(" 33", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData("a 44", ActionResult.Error)]
        [InlineData("a sd", ActionResult.Error)]
        [InlineData("4.4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]
        [InlineData(" ", ActionResult.Error)]
        public void Validate_Signature_3WordArg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command3WordArg("test", "test", new[] { UserRole.RegisteredUser },
                (i, j, k) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("asd dsa", ActionResult.Succes)]
        [InlineData("asd  2", ActionResult.Succes)]
        [InlineData(" asd sd23 ", ActionResult.Succes)]
        [InlineData(" 1  437", ActionResult.Succes)]
        [InlineData("asd dsa asd", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData(" 1  43 7", ActionResult.Error)]
        [InlineData("asd 23sd 2", ActionResult.Error)]
        [InlineData("4.4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]
        [InlineData(" ", ActionResult.Error)]
        public void Validate_Signature_2WordArg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command2WordArg("test", "test", new[] { UserRole.RegisteredUser },
                (i, j) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("\"1\" \"3\"", ActionResult.Succes)]
        [InlineData("\"asd dsa\" \"asd dsa asd\"", ActionResult.Succes)]
        [InlineData("\"aasd dsa\" \"3\"", ActionResult.Succes)]
        [InlineData("\"asd \" \"asd 44 dsa\"", ActionResult.Succes)]
        [InlineData("100 23 ", ActionResult.Error)]
        [InlineData(" 1  afds dfsdfg", ActionResult.Error)]
        [InlineData("1", ActionResult.Error)]
        [InlineData("a ds", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData("4.4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]
        [InlineData(" ", ActionResult.Error)]
        public void Validate_Signature_2StrArg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command2StrArg("test", "test", new[] { UserRole.RegisteredUser },
                (i, j) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("1 3", ActionResult.Succes)]
        [InlineData("100 23 ", ActionResult.Succes)]
        [InlineData(" 1  43", ActionResult.Succes)]
        [InlineData("1", ActionResult.Error)]
        [InlineData("13 ", ActionResult.Error)]
        [InlineData(" 33", ActionResult.Error)]
        [InlineData("a ds", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData("4.4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]
        [InlineData(" ", ActionResult.Error)]
        public void Validate_Signature_2NumArg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command2NumArg("test", "test", new[] { UserRole.RegisteredUser },
                (i, j) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Theory]
        [InlineData("1", ActionResult.Succes)]
        [InlineData("13 ", ActionResult.Succes)]
        [InlineData(" 33", ActionResult.Succes)]
        [InlineData("a d", ActionResult.Error)]
        [InlineData("a", ActionResult.Error)]
        [InlineData("4.4", ActionResult.Error)]
        [InlineData("false", ActionResult.Error)]
        [InlineData(" ", ActionResult.Error)]
        public void Validate_Signature_1NumArg(string args, ActionResult expected)
        {
            //Arrange
            BaseCommand.ErrorViewFactory = getResultFactory().Object;
            IActionResultFactory response = getResultFactory().Object;
            ICommand command = new Command1NumArg("test", "test", new[] { UserRole.RegisteredUser },
                (i) =>
                {
                    return response.GetResultRender(ActionResult.Succes, "Invoked");
                });
            User user = new User { Role = UserRole.RegisteredUser };

            //Act
            IActionResult result = command.Execute(user, args);

            //Assert
            Assert.Equal(expected, result.Result);
        }
    }
}
