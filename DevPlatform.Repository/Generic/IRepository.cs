using DevPlatform.Core.Entities;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevPlatform.Repository.Generic
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Get entity by identifier with include entities
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity GetById(int id, Func<IIncludable<TEntity>, IIncludable> includes = null);

        /// <summary>
        /// Finds an entity and returns with includes
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IIncludable<TEntity>, IIncludable> includes = null);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);


        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAsync(IList<TEntity> entities);

        /// <summary>
        /// Loads the original copy of the entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Copy of the passed entity</returns>
        TEntity LoadOriginalCopy(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAsync(IList<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(IList<TEntity> entities);

        /// <summary>
        /// Executes command using System.Data.CommandType.StoredProcedure command type
        /// and returns results as collection of values of specified type
        /// </summary>
        /// <param name="storeProcedureName">Store procedure name</param>
        /// <param name="dataParameters">Command parameters</param>
        /// <returns>Collection of query result records</returns>
        IList<TEntity> EntityFromSql(string storeProcedureName, params DataParameter[] dataParameters);

        /// <summary>
        /// Truncates database table
        /// </summary>
        /// <param name="resetIdentity">Performs reset identity column</param>
        void Truncate(bool resetIdentity = false);

        /// <summary>
        /// Returns entities with includes
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IIncludable<TEntity>, IIncludable> includes = null);

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        #endregion
    }
}
