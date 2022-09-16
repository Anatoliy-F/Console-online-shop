using ConsoleShop.Controller.Base;

namespace ConsoleShop.Controller.Interfaces
{
    /// <summary>
    /// Defines operations applicable with user object
    /// </summary>
    public interface IUserController : IController
    {
        /// <summary>
        /// User session termination
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult LogOut();

        /// <summary>
        /// User authorization
        /// </summary>
        /// <param name="userName">Unique user identifier</param>
        /// <param name="password">User password</param>
        /// <returns>Response object</returns>
        public IActionResult Login(string userName, string password);

        /// <summary>
        /// Change user identificator by user
        /// </summary>
        /// <param name="userName">New user name</param>
        /// <param name="password">User password</param>
        /// <returns>Response object</returns>
        public IActionResult ChangeName(string userName, string password);

        /// <summary>
        /// Сhange username out of user session by administrator
        /// </summary>
        /// <param name="userName">Current user name</param>
        /// <param name="newUserName">New user name</param>
        /// <returns>Response object</returns>
        public IActionResult ChangeUserName(string userName, string newUserName);

        /// <summary>
        /// Сhange user password out of user session by administrator
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">New password</param>
        /// <param name="repeatPassword">Repeat new password</param>
        /// <returns>Response object</returns>
        public IActionResult ChangeUserPassword(string userName, string password, string repeatPassword);

        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>Response object</returns>
        public IActionResult ShowAllUsers();

        /// <summary>
        /// Return user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Response object</returns>
        public IActionResult ShowUserById(int id);

        /// <summary>
        /// Change user password by user
        /// </summary>
        /// <param name="password">Current user password</param>
        /// <param name="newPassword">New user password</param>
        /// <param name="repeatNewPassword">Repeat new user password</param>
        /// <returns>Response object</returns>
        public IActionResult ChangePassword(string password, string newPassword, string repeatNewPassword);

        /// <summary>
        /// User account registration
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <param name="repeatPassword">Repeat user password</param>
        /// <returns>Response object</returns>
        public IActionResult RegisterAcount(string userName, string password, string repeatPassword);
    }
}
