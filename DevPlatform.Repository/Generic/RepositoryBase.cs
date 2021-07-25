﻿using DevPlatform.Core;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Common;
using DevPlatform.Core.Entities;
using DevPlatform.Core.Events;
using DevPlatform.Data;
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
    /// Represents the entity repository implementation
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IDevPlatformDataProvider _dataProvider;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public RepositoryBase(IEventPublisher eventPublisher,
            IDevPlatformDataProvider dataProvider,
            IStaticCacheManager staticCacheManager)
        {
            _eventPublisher = eventPublisher;
            _dataProvider = dataProvider;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="getAllAsync">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        protected virtual async Task<IList<TEntity>> GetEntitiesAsync(Func<Task<IList<TEntity>>> getAllAsync, Func<IStaticCacheManager, CacheKey> getCacheKey)
        {
            if (getCacheKey == null)
                return await getAllAsync();

            var cacheKey = getCacheKey(_staticCacheManager)
                           ?? _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<TEntity>.AllCacheKey);
            return await _staticCacheManager.GetAsync(cacheKey, getAllAsync);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="getAll">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>Entity entries</returns>
        protected virtual IList<TEntity> GetEntities(Func<IList<TEntity>> getAll, Func<IStaticCacheManager, CacheKey> getCacheKey)
        {
            if (getCacheKey == null)
                return getAll();

            var cacheKey = getCacheKey(_staticCacheManager)
                           ?? _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<TEntity>.AllCacheKey);

            return _staticCacheManager.Get(cacheKey, getAll);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="getAllAsync">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        protected virtual async Task<IList<TEntity>> GetEntitiesAsync(Func<Task<IList<TEntity>>> getAllAsync, Func<IStaticCacheManager, Task<CacheKey>> getCacheKey)
        {
            if (getCacheKey == null)
                return await getAllAsync();

            var cacheKey = await getCacheKey(_staticCacheManager)
                           ?? _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<TEntity>.AllCacheKey);
            return await _staticCacheManager.GetAsync(cacheKey, getAllAsync);
        }

        /// <summary>
        /// Adds "deleted" filter to query which contains <see cref="ISoftDeletedEntity"/> entries, if its need
        /// </summary>
        /// <param name="query">Entity entries</param>
        /// <param name="includeDeleted">Whether to include deleted items</param>
        /// <returns>Entity entries</returns>
        protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted)
        {
            if (includeDeleted)
                return query;

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletedEntity)) == null)
                return query;

            return query.OfType<ISoftDeletedEntity>().Where(entry => !entry.Deleted).OfType<TEntity>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the entity entry
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        public virtual async Task<TEntity> GetByIdAsync(int? id, Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
        {
            if (!id.HasValue || id == 0)
                return null;

            async Task<TEntity> getEntityAsync()
            {
                return await AddDeletedFilter(Table, includeDeleted).FirstOrDefaultAsync(entity => entity.Id == Convert.ToInt32(id));
            }

            if (getCacheKey == null)
                return await getEntityAsync();

            var cacheKey = getCacheKey(_staticCacheManager)
                ?? _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<TEntity>.ByIdCacheKey, id);

            return await _staticCacheManager.GetAsync(cacheKey, getEntityAsync);
        }

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public virtual async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();

            async Task<IList<TEntity>> getByIdsAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);

                //get entries
                var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

                //sort by passed identifiers
                var sortedEntries = new List<TEntity>();
                foreach (var id in ids)
                {
                    var sortedEntry = entries.FirstOrDefault(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }

                return sortedEntries;
            }

            if (getCacheKey == null)
                return await getByIdsAsync();

            var cacheKey = getCacheKey(_staticCacheManager)
                ?? _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<TEntity>.ByIdsCacheKey, ids);
            return await _staticCacheManager.GetAsync(cacheKey, getByIdsAsync);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public virtual async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, false);
                query = func != null ? func(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(getAllAsync, getCacheKey);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>Entity entries</returns>
        public virtual IList<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
        {
            IList<TEntity> getAll()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? func(query) : query;

                return query.ToList();
            }

            return GetEntities(getAll, getCacheKey);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public virtual async Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
            Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? await func(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(getAllAsync, getCacheKey);
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public virtual async Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
            Func<IStaticCacheManager, Task<CacheKey>> getCacheKey = null, bool includeDeleted = true)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? await func(query) : query;

                return await query.ToListAsync();
            }

            return await GetEntitiesAsync(getAllAsync, getCacheKey);
        }

        /// <summary>
        /// Get paged list of all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">Whether to get only the total number of entries without actually loading data</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the paged list of entity entries
        /// </returns>
        public virtual async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            query = func != null ? func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        /// <summary>
        /// Get paged list of all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">Whether to get only the total number of entries without actually loading data</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="DevPlatform.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the paged list of entity entries
        /// </returns>
        public virtual async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            query = func != null ? await func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
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
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
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
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the copy of the passed entity
        /// </returns>
        public virtual async Task<TEntity> LoadOriginalCopyAsync(TEntity entity)
        {
            return await (await _dataProvider.GetTableAsync<TEntity>())
                .FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(entity.Id));
        }

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
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
        /// Update entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
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
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(TEntity entity, bool publishEvent = true)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case ISoftDeletedEntity softDeletedEntity:
                    softDeletedEntity.Deleted = true;
                    await _dataProvider.UpdateEntityAsync(entity);
                    break;

                default:
                    await _dataProvider.DeleteEntityAsync(entity);
                    break;
            }

            //event notification
            if (publishEvent)
                await _eventPublisher.EntityDeletedAsync(entity);
        }

        /// <summary>
        /// Delete entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <param name="publishEvent">Whether to publish event notification</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.OfType<ISoftDeletedEntity>().Any())
            {
                foreach (var entity in entities)
                    if (entity is ISoftDeletedEntity softDeletedEntity)
                    {
                        softDeletedEntity.Deleted = true;
                        await _dataProvider.UpdateEntityAsync(entity);
                    }
            }
            else
                await _dataProvider.BulkDeleteEntitiesAsync(entities);

            //event notification
            if (!publishEvent)
                return;

            foreach (var entity in entities)
                await _eventPublisher.EntityDeletedAsync(entity);
        }

        /// <summary>
        /// Delete entity entries by the passed predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of deleted records
        /// </returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _dataProvider.BulkDeleteEntitiesAsync(predicate);
        }

        /// <summary>
        /// Executes SQL using System.Data.CommandType.StoredProcedure command type and returns results as collection of values of specified type
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public virtual async Task<IList<TEntity>> EntityFromSqlAsync(string procedureName, params DataParameter[] parameters)
        {
            return await _dataProvider.QueryProcAsync<TEntity>(procedureName, parameters?.ToArray());
        }

        /// <summary>
        /// Truncates database table
        /// </summary>
        /// <param name="resetIdentity">Performs reset identity column</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task TruncateAsync(bool resetIdentity = false)
        {
            await (await _dataProvider.GetTableAsync<TEntity>()).TruncateAsync(resetIdentity);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => _dataProvider.GetTable<TEntity>();

        #endregion
    }

}
