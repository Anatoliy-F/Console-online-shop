using ConsoleShop.Model.BaseEntity;
using System.Collections.Generic;

namespace ConsoleShop.Dal.Repos.Interfaces
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepo<T> where T : IEntity
    {
        /// <summary>
        /// Add IEntity to collection
        /// </summary>
        /// <param name="entity">ConsoleShop.Model.BaseEntity.IEntity entity</param>
        /// <returns>Id that assigned to new entity</returns>
        /// <exception cref="Dal.Exception.CustomRepoException">Thrown when <paramref name="entity"/>
        /// not validated</exception>
        /// <exception cref="Dal.Exception.NotFoundException">Thrown when <paramref name="entity"/>
        /// has dependencies that not exists in current contex</exception>
        int Add(T entity);

        /// <summary>
        /// Return IEntity from collection searching it by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Return ConsoleShop.Model.BaseEntity.IEntity object or default value,
        /// if collection did not contain object with this id </returns>
        T Find(int id);

        /// <summary>
        /// Return all entities that persisted in store
        /// </summary>
        /// <returns>IEnumerable&lt;ConsoleShop.Model.BaseEntity.IEntity&gt;</returns>
        IEnumerable<T> GetAll();
    }
}
