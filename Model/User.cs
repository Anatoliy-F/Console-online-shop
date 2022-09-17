using ConsoleShop.Model.BaseEntity;
using System;
using System.Collections.Generic;

namespace ConsoleShop.Model
{
    /// <summary>
    /// Represent available user roles.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// User by default, i.e. unknown user, information about him didn't persists.
        /// </summary>
        Guest = 1,
        /// <summary>
        /// Represent user after registration
        /// </summary>
        RegisteredUser = 2,
        /// <summary>
        /// Have access for CRUD operations
        /// </summary>
        Administrator = 4,
        /// <summary>
        /// "superuser" for implementing redirect of users request
        /// </summary>
        System = 8,
    }

    /// <summary>
    /// User - entity that represent customer or employee 
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// represent unique identificator for entity
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// represent name of user, his login. It should be unique for authorization.
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// represent user access level
        /// </summary>
        public UserRole Role { get; set; } = UserRole.Guest;

        /// <summary>
        /// stores hashed user password
        /// </summary>
        public string PasswordHash { get; set; } = String.Empty;

        /// <summary>
        /// navigation property, that represent collection of user orders
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
