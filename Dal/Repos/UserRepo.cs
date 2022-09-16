using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Dal.Repos
{

    /// <inheritdoc />
    public class UserRepo : IUserRepo
    {
        /// <summary>
        /// Users list
        /// </summary>
        private readonly List<User> _users;

        /// <summary>
        /// Initialize new instance of UserRepository to manage saving, updating and retrieving User's entities 
        /// </summary>
        /// <param name="shopContext"></param>
        public UserRepo(IShopContext shopContext)
        {
            _users = shopContext.Users;
        }

        /// <inheritdoc />
        public int Add(User entity)
        {
            int id = _users.Max(u => u.Id) + 1;
            entity.Id = id;
            _users.Add(entity);
            return id;

        }

        /// <inheritdoc />
        public User Find(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        /// <inheritdoc />
        public User FindByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.Name == userName);
        }

        /// <inheritdoc />
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        /// <inheritdoc />
        public int Update(int id, User updateUser)
        {
            User user = _users.FirstOrDefault(u => u.Id == id);

            if(user == null)
            {
                throw new NotFoundException($"No user with id: {id} in our shop");
            }

            if(updateUser.Name != String.Empty)
            {
                user.Name = updateUser.Name;
            }
            if(updateUser.PasswordHash != String.Empty)
            {
                user.PasswordHash = updateUser.PasswordHash;
            }

            return id;
        }

        /// <inheritdoc />
        public int UpdateByName(string name, User updateUser)
        {
            User user = _users.FirstOrDefault(u => u.Name == name);

            if(user == null)
            {
                throw new NotFoundException($"No user with userName: {name} in our shop");
            }

            int id = user.Id;

            if (updateUser.Name != String.Empty)
            {
                user.Name = updateUser.Name;
            }
            if (updateUser.PasswordHash != String.Empty)
            {
                user.PasswordHash = updateUser.PasswordHash;
            }

            return id;
        }
    }
}
