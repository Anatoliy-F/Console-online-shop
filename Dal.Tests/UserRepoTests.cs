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
    public class UserRepoTests
    {
        [Fact]
        public void Can_Find_User()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);

            //Act
            User u1 = ur.Find(1);
            User u2 = ur.Find(1);
            User u3 = ur.Find(2);
            User u4 = ur.Find(4);
            User u5 = ur.Find(10);

            //Assert
            Assert.Same(u1, u2);
            Assert.Equal("user2", u3.Name);
            Assert.Equal(UserRole.Administrator, u4.Role);
            Assert.Null(u5);
        }

        [Fact]
        public void Can_Find_User_By_UserName()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);

            //Act
            User u1 = ur.FindByUserName("user1");
            User u2 = ur.FindByUserName("user1");
            User u3 = ur.FindByUserName("user2");
            User u4 = ur.FindByUserName("user4");
            User u5 = ur.FindByUserName("user100");

            //Assert
            Assert.Same(u1, u2);
            Assert.Equal("user2", u3.Name);
            Assert.Equal(UserRole.Administrator, u4.Role);
            Assert.Null(u5);
        }

        [Fact]
        public void Return_All_Users()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);

            //Act
            User[] users = ur.GetAll().ToArray();

            //Assert
            Assert.True(users.Length == 4);
            Assert.Equal("user1", users[0].Name);
            Assert.Equal(2, users[1].Id);
            Assert.Same(mock.Object.Users[3], users[3]);
        }

        [Fact]
        public void Can_Add_User()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);
            User u1 = new User { Name = "newUser1", Role = UserRole.RegisteredUser, PasswordHash = "newPass1" };
            User u2 = new User { Name = "newUser2", Role = UserRole.RegisteredUser, PasswordHash = "newPass2" };
            User u3 = new User { Name = "newUser3", Role = UserRole.RegisteredUser, PasswordHash = "newPass3" };

            //Act
            var before_count = ur.GetAll().ToArray().Length;

            ur.Add(u1);
            ur.Add(u2);
            ur.Add(u3);

            User[] all = ur.GetAll().ToArray();

            //Assert
            Assert.Equal(4, before_count);
            Assert.Equal(7, all.Length);
            Assert.Equal(7, ur.FindByUserName("newUser3").Id);
            Assert.Contains(u1, all);
            Assert.Contains(u2, all);
            Assert.Contains(u3, all);
        }

        [Fact]
        public void Can_Update_User()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);
            User user1 = new User { Name = "newUser1" };
            User user2 = new User { PasswordHash = "newPass1" };
            User user3 = new User { Name = "newUser3", PasswordHash = "newPass3" };
            User user4 = new User();

            //Act
            var before_count = ur.GetAll().ToArray().Length;

            ur.Update(1, user1);
            ur.Update(2, user2);
            ur.Update(3, user3);
            ur.Update(4, user4);

            var all = ur.GetAll().ToArray();

            //Assert
            Assert.Equal(before_count, all.Length);
            Assert.Equal("newUser1", ur.Find(1).Name);
            Assert.Equal("newPass1", ur.Find(2).PasswordHash);
            Assert.Equal("newUser3", ur.Find(3).Name);
            Assert.Equal("newPass3", ur.Find(3).PasswordHash);
            Assert.Equal("user4", ur.Find(4).Name);
            Assert.Equal("qwerty4", ur.Find(4).PasswordHash);
            Assert.Throws<NotFoundException>(() => { ur.Update(5, user4); });
        }

        [Fact]
        public void Can_Update_User_ByName()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User {Id = 1, Name = "user1", Role = UserRole.RegisteredUser, PasswordHash = "qwerty1"},
                new User {Id = 2, Name = "user2", Role = UserRole.RegisteredUser, PasswordHash = "qwerty2"},
                new User {Id = 3, Name = "user3", Role = UserRole.RegisteredUser, PasswordHash = "qwerty3"},
                new User {Id = 4, Name = "user4", Role = UserRole.Administrator, PasswordHash = "qwerty4"},
            });

            UserRepo ur = new UserRepo(mock.Object);
            User user1 = new User { Name = "newUser1" };
            User user2 = new User { PasswordHash = "newPass1" };
            User user3 = new User { Name = "newUser3", PasswordHash = "newPass3" };
            User user4 = new User();

            //Act
            var before_count = ur.GetAll().ToArray().Length;

            ur.UpdateByName("user1", user1);
            ur.UpdateByName("user2", user2);
            ur.UpdateByName("user3", user3);
            ur.UpdateByName("user4", user4);

            var all = ur.GetAll().ToArray();

            //Assert
            Assert.Equal(before_count, all.Length);
            Assert.Equal("newUser1", ur.Find(1).Name);
            Assert.Equal("newPass1", ur.Find(2).PasswordHash);
            Assert.Equal("newUser3", ur.Find(3).Name);
            Assert.Equal("newPass3", ur.Find(3).PasswordHash);
            Assert.Equal("user4", ur.Find(4).Name);
            Assert.Equal("qwerty4", ur.Find(4).PasswordHash);
            Assert.Throws<NotFoundException>(() => { ur.UpdateByName("Tom", user4); });
        }
    }
}
