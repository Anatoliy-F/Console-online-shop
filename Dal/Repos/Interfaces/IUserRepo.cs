using ConsoleShop.Model;

namespace ConsoleShop.Dal.Repos.Interfaces
{
    /// <summary>
    /// Interface defines the operations applicable to the ConsoleShop.Model.User entities
    /// </summary>
    public interface IUserRepo : IRepo<User>
    {
        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="id">int id - id of existing User</param>
        /// <param name="updateUser">User instance, all non-empty properties replaces properties of
        /// existing User</param>
        /// <returns>int id - id of updated User</returns>
        /// <exception cref="Dal.Exception.NotFoundException">Thrown when user with <paramref name="id"/>
        /// didn't exist</exception>
        public int Update(int id, User updateUser);

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="name">string name - name of existing User</param>
        /// <param name="updateUser">User instance, all non-empty properties replaces properties of
        /// existing User</param>
        /// <returns>int id - id of updated User</returns>
        /// <exception cref="Dal.Exception.NotFoundException">Thrown when user with <paramref name="name"/>
        /// didn't exist</exception>
        public int UpdateByName(string name, User updateUser);

        /// <summary>
        /// Return User from collection searching him by name
        /// </summary>
        /// <param name="userName">string name</param>
        /// <returns>User <see cref="ConsoleShop.Model.User"/></returns>
        public User FindByUserName(string userName);
    }
}
