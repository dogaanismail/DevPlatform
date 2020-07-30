using DevPlatform.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevPlatform.Repository.Generic
{
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Finds an entity with filter and can get a relation entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> filter = null, Func<IIncludable<T>, IIncludable> includes = null);

        /// <summary>
        /// Returns an entity by id and can get a relation entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetById(int id, Func<IIncludable<T>, IIncludable> includes = null);

        /// <summary>
        /// Returns an entity list by id and can get a relation entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IIncludable<T>, IIncludable> includes = null);

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="forceDelete"></param>
        void Delete(int id, bool forceDelete = true);

        /// <summary>
        /// Deletes all entity
        /// </summary>
        /// <param name="entities"></param>
        void DeleteAll(IEnumerable<T> entities);
    }
}
