using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Services;
using Xunit;


namespace ConsoleApp1.Tests
{
    public class SessionTests
    {
        [Fact]
        public void Can_Set_User()
        {
            //Arrange
            ISession session = new ShopSession();
            User user = new User { Name = "Tom"};

            //Act
            session.SetUser(user);

            //Assert
            Assert.Same(user, session.User);
        }

        [Fact]
        public void Clear_Cart_When_Set_User()
        {
            //Arrange
            ISession session = new ShopSession();
            var pr = new Product { Id = 3 };
            session.Cart.AddItem(pr);
            var linesBefore = session.Cart.Lines.Count;
            User user = new User { Name = "Tom" };

            //Act
            session.SetUser(user);

            //Assert
            Assert.Equal(1, linesBefore);
            Assert.Empty(session.Cart.Lines);
        }
    }
}
