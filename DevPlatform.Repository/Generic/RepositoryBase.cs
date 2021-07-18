using DevPlatform.Core.Entities;
using DevPlatform.Core.Events;
using DevPlatform.Data;
using DevPlatform.Repository.Extensions;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace DevPlatform.Repository.Generic
{
    /// <summary>
    /// Represents the Entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly IDevPlatformDataProvider _dataProvider;
        private ITable<TEntity> _entities;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        public RepositoryBase(IDevPlatformDataProvider dataProvider,
            IEventPublisher eventPublisher)
        {
            _dataProvider = dataProvider;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(object id)
        {
            return Entities.FirstOrDefault(e => e.Id == Convert.ToInt32(id));
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dataProvider.InsertEntity(entity);

            //event notification
            if (publishEvent)
                _ = _eventPublisher.EntityInsertedAsync(entity);
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using (var transaction = new TransactionScope())
            {
                _dataProvider.BulkInsertEntities(entities);
                transaction.Complete();
            }

            if (!publishEvent)
                return;

            //event notification
            foreach (var entity in entities)
                _ = _eventPublisher.EntityInsertedAsync(entity);
        }

        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dataProvider.InsertEntityAsync(entity);

            //event notification
            if (publishEvent)
                await _eventPublisher.EntityInsertedAsync(entity);
        }

        /// <summary>
        /// Insert entity entries
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _dataProvider.BulkInsertEntitiesAsync(entities);
            transaction.Complete();

            if (!publishEvent)
                return;

            //event notification
            foreach (var entity in entities)
                await _eventPublisher.EntityInsertedAsync(entity);
        }

        /// <summary>
        /// Loads the original copy of the entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Copy of the passed entity</returns>
        public virtual TEntity LoadOriginalCopy(TEntity entity)
        {
            return _dataProvider.GetTable<TEntity>()
                .FirstOrDefault(e => e.Id == Convert.ToInt32(entity.Id));
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dataProvider.UpdateEntity(entity);

            //event notification
            if (publishEvent)
                _ = _eventPublisher.EntityUpdatedAsync(entity);
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                Update(entity, publishEvent);
            }
        }

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dataProvider.UpdateEntityAsync(entity);

            //event notification
            if (publishEvent)
                await _eventPublisher.EntityUpdatedAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!entities.Any())
                return;

            await _dataProvider.UpdateEntitiesAsync(entities);

            //event notification
            if (!publishEvent)
                return;

            foreach (var entity in entities)
                await _eventPublisher.EntityUpdatedAsync(entity);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dataProvider.DeleteEntity(entity);

            //event notification
            if (publishEvent)
                _ = _eventPublisher.EntityDeletedAsync(entity);
        }


        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _dataProvider.BulkDeleteEntities(entities.ToList());

            //event notification
            if (!publishEvent)
                return;

            foreach (var entity in entities)
                _ = _eventPublisher.EntityDeletedAsync(entity);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            _dataProvider.BulkDeleteEntities(predicate);
        }

        /// <summary>
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dataProvider.DeleteEntityAsync(entity);

            //event notification
            if (publishEvent)
                await _eventPublisher.EntityDeletedAsync(entity);
        }

        /// <summary>
        /// Delete entity entries
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await _dataProvider.BulkDeleteEntitiesAsync(entities);

            foreach (var entity in entities)
                await _eventPublisher.EntityDeletedAsync(entity);
        }

        /// <summary>
        /// Executes command using System.Data.CommandType.StoredProcedure command type
        /// and returns results as collection of values of specified type
        /// </summary>
        /// <param name="storeProcedureName">Store procedure name</param>
        /// <param name="dataParameters">Command parameters</param>
        /// <returns>Collection of query result records</returns>
        public virtual IList<TEntity> EntityFromSql(string storeProcedureName, params DataParameter[] dataParameters)
        {
            return _dataProvider.QueryProc<TEntity>(storeProcedureName, dataParameters?.ToArray());
        }

        /// <summary>
        /// Truncates database table
        /// </summary>
        /// <param name="resetIdentity">Performs reset identity column</param>
        public virtual void Truncate(bool resetIdentity = false)
        {
            _dataProvider.GetTable<TEntity>().Truncate(resetIdentity);
        }

        /// <summary>
        /// Returns an entity with includes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public TEntity GetById(int id, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = _dataProvider.GetTable<TEntity>().AsQueryable();

            if (includes != null)
                query = query.IncludeMultiple(includes);

            return query.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Returns entities with includes
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = _dataProvider.GetTable<TEntity>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = query.IncludeMultiple(includes);

            return query.ToList();
        }

        /// <summary>
        /// Finds an entity and returns with includes
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = _dataProvider.GetTable<TEntity>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = query.IncludeMultiple(includes);

            return query;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets an entity set
        /// </summary>
        public virtual ITable<TEntity> Entities => _entities ??= _dataProvider.GetTable<TEntity>();

        #endregion
    }

}
