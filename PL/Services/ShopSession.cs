using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Bll.Model;

namespace ConsoleShop.Services
{
    /// <summary>
    /// Object that persists the state of a user's session between requests.
    /// </summary>
    public class ShopSession : ISession
    {
        private User _user = new User();

        /// <summary>
        /// Authorized user
        /// </summary>
        public User User {
            get => _user;
            private set => _user = value;    
        }

        /// <summary>
        /// Shopping cart
        /// </summary>
        public ICart Cart { get; private set; } = new Cart();

        /// <summary>
        /// Method that sets an instance of an authorized user
        /// </summary>
        /// <param name="user">Authorized user</param>
        public void SetUser(User user)
        {
            Cart.Clear();
            User = user;
        }
    }
}
