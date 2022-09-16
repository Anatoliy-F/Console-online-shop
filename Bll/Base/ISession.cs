using ConsoleShop.Model;
using ConsoleShop.Bll.Model;

namespace ConsoleShop.Controller.Base
{
    /// <summary>
    /// Describes an object that persists the state of a user's session between requests.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Authorized user
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Shopping cart
        /// </summary>
        public ICart Cart { get; }

        /// <summary>
        /// Method that sets an instance of an authorized user
        /// </summary>
        /// <param name="user">Authorized user</param>
        public void SetUser(User user);
    }
}
