using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class LoginControllerTests : BaseTest
    {
        [Fact]
        public void Can_Logout()
        {   
            //Arrange
            var responseFactory = GetResultFactory();

            Mock<ISession> session = new Mock<ISession>();
            User u1 = new User { Name = "Tom" };
            session.Setup(s => s.User).Returns(u1);

            //Act
            IActionResult result = new UserController(null, session.Object, responseFactory.Object).LogOut();

            //Assert
            session.Verify(s => s.SetUser(It.Is<User>(u => u.Id == 0 && u.Name == "")), Times.Once);
            Assert.Equal(ActionResult.Succes, result.Result);
            Assert.Contains("Tom", result.Message);
            Assert.Null(result.ResultBody);
        }

        [Fact]
        public void Can_Login()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();
            var responseFactory3 = GetResultFactory();

            var user = new User()
            {
                Id = 1,
                Role = UserRole.RegisteredUser,
                Name = "user",
                PasswordHash = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", //qwerty
            };

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.FindByUserName(It.Is<String>(s => s == "user")))
                .Returns(user);
            Mock<ISession> session = new Mock<ISession>();

            //Act
            IActionResult result1 = new UserController(userRepo.Object, session.Object, responseFactory1.Object)
                .Login("user", "qwerty");
            IActionResult result2 = new UserController(userRepo.Object, session.Object, responseFactory2.Object)
                .Login("user", "qwerto");
            IActionResult result3 = new UserController(userRepo.Object, session.Object, responseFactory3.Object)
                .Login("usor", "qwerty");

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Contains("user", result1.Message);
            Assert.Null(result1.ResultBody);
            session.Verify(s => s.SetUser(It.Is<User>(u => u == user)), Times.Once);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.NotFound, result3.Result);
            Assert.Contains("usor", result3.Message);
            Assert.Null(result3.ResultBody);
        }

        [Fact]
        public void User_Can_Change_Name()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();
            var responseFactory3 = GetResultFactory();

            var user = new User()
            {
                Id = 1,
                Role = UserRole.RegisteredUser,
                Name = "user",
                PasswordHash = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", //qwerty
            };

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.FindByUserName(It.Is<String>(s => s == "Tom")))
                .Returns(new User { Name = "Tom"});
            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(user);

            //Act
            IActionResult result1 = new UserController(userRepo.Object, session.Object, responseFactory1.Object)
                .ChangeName("newUser", "qwerty");
            IActionResult result2 = new UserController(userRepo.Object, session.Object, responseFactory2.Object)
                .ChangeName("Tom", "qwerto");
            IActionResult result3 = new UserController(userRepo.Object, session.Object, responseFactory3.Object)
                .ChangeName("Tom", "qwerty");

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Null(result1.ResultBody);
            session.Verify(s => s.SetUser(It.IsAny<User>()), Times.Once());
            userRepo.Verify(ur => ur.Find(It.Is<int>(i => user.Id == 1)), Times.Once);
            userRepo.Verify(ur => ur.UpdateByName(It.Is<string>(s => s == user.Name),
                It.Is<User>(u => u.Name == "newUser")), Times.Once);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Contains("Tom", result3.Message);
            Assert.Null(result3.ResultBody);
        }

        [Fact]
        public void Can_Change_User_Name()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.UpdateByName(It.Is<string>(s => s == "Tom"), It.IsAny<User>()))
                .Throws(new NotFoundException());

            //Act
            IActionResult result1 = new UserController(userRepo.Object, null, responseFactory1.Object)
                .ChangeUserName("user", "Jerry");
            IActionResult result2 = new UserController(userRepo.Object, null, responseFactory2.Object)
                .ChangeUserName("Tom", "Jerry");

            //Assert
            userRepo.Verify(ur => ur.UpdateByName(It.IsAny<string>(), It.IsAny<User>()), Times.Exactly(2));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Change_User_Password()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();
            var responseFactory3 = GetResultFactory();

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.UpdateByName(It.Is<string>(s => s == "Tom"), It.IsAny<User>()))
                .Throws(new NotFoundException());

            //Act
            IActionResult result1 = new UserController(userRepo.Object, null, responseFactory1.Object)
                .ChangeUserPassword("user", "newPass", "newPass");
            IActionResult result2 = new UserController(userRepo.Object, null, responseFactory2.Object)
                .ChangeUserPassword("user", "newPass", "newPoss");
            IActionResult result3 = new UserController(userRepo.Object, null, responseFactory3.Object)
                .ChangeUserPassword("Tom", "newPass", "newPass");

            //Assert
            userRepo.Verify(ur => ur.UpdateByName(It.IsAny<string>(), It.IsAny<User>()), Times.Exactly(2));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.NotFound, result3.Result);
            Assert.Null(result3.ResultBody);
        }

        [Fact]
        public void Can_Show_All_Users()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            var u1 = new User { Name = "Tom" };
            var u2 = new User { Name = "Jerry" };
            userRepo.Setup(ur => ur.GetAll()).Returns(new[] {u1, u2});

            Mock<IUserRepo> emptyRepo = new Mock<IUserRepo>();
            emptyRepo.Setup(ur => ur.GetAll()).Returns(new User[] {});

            //Act
            IActionResult result1 = new UserController(userRepo.Object, null, responseFactory1.Object)
                .ShowAllUsers();
            IActionResult result2 = new UserController(emptyRepo.Object, null, responseFactory2.Object)
                .ShowAllUsers();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal(2, result1.ResultBody.Count());
            Assert.Contains(u1, result1.ResultBody);
            Assert.Contains(u2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Show_User_ById()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            var u1 = new User { Name = "Tom" };
            userRepo.Setup(ur => ur.Find(It.Is<int>(i => i == 3))).Returns(u1);

            //Act
            IActionResult result1 = new UserController(userRepo.Object, null, responseFactory1.Object)
                .ShowUserById(3);
            IActionResult result2 = new UserController(userRepo.Object, null, responseFactory2.Object)
                .ShowUserById(4);

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.NotNull(result1.ResultBody);
            Assert.Contains(u1, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void User_Can_Change_Password()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();
            var responseFactory3 = GetResultFactory();
            var responseFactory4 = GetResultFactory();

            var user1 = new User()
            {
                Id = 1,
                Name = "user",
                PasswordHash = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", //qwerty
            };
            var user2 = new User()
            {
                Id = 2,
                Name = "user",
                PasswordHash = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", //qwerty
            };

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.Update(It.Is<int>(i => i == 2), It.IsAny<User>())).Throws(new NotFoundException("ex"));
            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.User).Returns(user1);
            Mock<ISession> wrongSession = new Mock<ISession>();
            wrongSession.Setup(s => s.User).Returns(user2);


            //Act
            IActionResult result1 = new UserController(userRepo.Object, session.Object, responseFactory1.Object)
                .ChangePassword("qwerty", "qwerty1", "qwerty1");
            IActionResult result2 = new UserController(userRepo.Object, session.Object, responseFactory2.Object)
                .ChangePassword("qwerto", "qwerty2", "qwerty2");
            IActionResult result3 = new UserController(userRepo.Object, session.Object, responseFactory3.Object)
                .ChangePassword("qwerty", "qwerty1", "qwerty2");
            IActionResult result4 = new UserController(userRepo.Object, wrongSession.Object, responseFactory4.Object)
                .ChangePassword("qwerty", "qwerty1", "qwerty1");

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Null(result1.ResultBody);
            session.VerifyGet(s => s.User, Times.Exactly(4));
            wrongSession.VerifyGet(s => s.User, Times.Exactly(2));
            userRepo.Verify(ur => ur.Update(It.IsAny<int>(), It.IsAny<User>()), Times.Exactly(2));
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Null(result3.ResultBody);
            Assert.Equal(ActionResult.NotFound, result4.Result);
            Assert.Null(result4.ResultBody);
        }

        [Fact]
        public void Can_Register_Account()
        {
            //Arrange
            var responseFactory1 = GetResultFactory();
            var responseFactory2 = GetResultFactory();
            var responseFactory3 = GetResultFactory();
            var responseFactory4 = GetResultFactory();

            Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
            userRepo.Setup(ur => ur.FindByUserName(It.Is<string>(s => s == "Tom")))
                .Returns(new User { Name = "Tom"});

            //Act
            IActionResult result1 = new UserController(userRepo.Object, null, responseFactory1.Object)
                .RegisterAcount("Jerry", "qwerty", "qwerty");
            IActionResult result2 = new UserController(userRepo.Object, null, responseFactory2.Object)
                .RegisterAcount("Tom", "qwerty", "qwerty");
            IActionResult result3 = new UserController(userRepo.Object, null, responseFactory3.Object)
                .RegisterAcount("Jerry", "asd", "asd");
            IActionResult result4 = new UserController(userRepo.Object, null, responseFactory4.Object)
                .RegisterAcount("Jerry", "qwerty1", "qwerty2");

            //Assert
            userRepo.Verify(ur => ur.Add(It.IsAny<User>()), Times.Once());
            userRepo.Verify(ur => ur.FindByUserName(It.IsAny<string>()), Times.Exactly(4));
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Contains("Jerry", result1.Message);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Contains("Tom", result2.Message);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Null(result3.ResultBody);
            Assert.Contains("Jerry", result3.Message);
            Assert.Equal(ActionResult.Warning, result4.Result);
            Assert.Null(result4.ResultBody);
            Assert.Contains("Jerry", result4.Message);
        }
    }
}
