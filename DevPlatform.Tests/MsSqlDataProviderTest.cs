using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DevPlatform.Core.Entities;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using DevPlatform.Data.Mapping;
using DevPlatform.Data.Migrations;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Mapping;
using LinqToDB.Tools;

namespace DevPlatform.Tests
{
    /// <summary>
    /// Represents the MsSQL data provider
    /// </summary>
    public partial class MsSqlDataProviderTest : IDevPlatformDataProvider
    {
        #region Utils

        /// <summary>
        /// Gets an additional mapping schema
        /// </summary>
        private MappingSchema GetMappingSchema()
        {
            if (Singleton<MappingSchema>.Instance is null)
            {
                Singleton<MappingSchema>.Instance = new MappingSchema(ConfigurationName)
                {
                    MetadataReader = new FluentMigratorMetadataReader()
                };
            }

            return Singleton<MappingSchema>.Instance;
        }

        private static void UpdateParameterValue(DataConnection dataConnection, DataParameter parameter)
        {
            if (dataConnection is null)
                throw new ArgumentNullException(nameof(dataConnection));

            if (parameter is null)
                throw new ArgumentNullException(nameof(parameter));

            if (dataConnection.Command is IDbCommand command &&
                command.Parameters.Count > 0 &&
                command.Parameters.Contains(parameter.Name) &&
                command.Parameters[parameter.Name] is IDbDataParameter param)
            {
                parameter.Value = param.Value;
            }
        }

        private static void UpdateOutputParameters(DataConnection dataConnection, DataParameter[] dataParameters)
        {
            if (dataParameters is null || dataParameters.Length == 0)
                return;

            foreach (var dataParam in dataParameters.Where(p => p.Direction == ParameterDirection.Output))
            {
                UpdateParameterValue(dataConnection, dataParam);
            }
        }

        /// <summary>
        /// Gets a connection to the database for a current data provider
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        protected static DbConnection GetInternalDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(null, nameof(connectionString));

            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<DataConnection> CreateDataConnectionAsync()
        {
            return await CreateDataConnectionAsync(LinqToDbDataProvider);
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        protected virtual DataConnection CreateDataConnection()
        {
            return CreateDataConnection(LinqToDbDataProvider);
        }

        protected virtual SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            var connectionString = DataSettingsManager.LoadSettings().ConnectionString;

            return new SqlConnectionStringBuilder(connectionString);
        }


        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the database connection
        /// </returns>
        protected virtual async Task<DataConnection> CreateDataConnectionAsync(IDataProvider dataProvider)
        {
            if (dataProvider is null)
                throw new ArgumentNullException(nameof(dataProvider));

            var dataContext = new DataConnection(dataProvider, await CreateDbConnectionAsync(), GetMappingSchema())
            {
                CommandTimeout = await DataSettingsManager.GetSqlCommandTimeoutAsync()
            };

            return dataContext;
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <returns>Database connection</returns>
        protected virtual DataConnection CreateDataConnection(IDataProvider dataProvider)
        {
            if (dataProvider is null)
                throw new ArgumentNullException(nameof(dataProvider));

            var dataContext = new DataConnection(dataProvider, CreateDbConnection(), GetMappingSchema())
            {
                CommandTimeout = DataSettingsManager.GetSqlCommandTimeout()
            };

            return dataContext;
        }

        /// <summary>
        /// Creates a connection to a database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the connection to a database
        /// </returns>
        protected virtual async Task<IDbConnection> CreateDbConnectionAsync(string connectionString = null)
        {
            var dbConnection = GetInternalDbConnection(!string.IsNullOrEmpty(connectionString) ? connectionString : await GetCurrentConnectionStringAsync());

            return dbConnection;
        }

        /// <summary>
        /// Creates a connection to a database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        protected virtual IDbConnection CreateDbConnection(string connectionString = null)
        {
            var dbConnection = GetInternalDbConnection(!string.IsNullOrEmpty(connectionString) ? connectionString : GetCurrentConnectionString());

            return dbConnection;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of database provider
        /// </summary>
        public string ConfigurationName => LinqToDbDataProvider.Name;

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        public int SupportedLengthOfBinaryHash { get; } = 8000;

        /// <summary>
        /// Sql server data provider
        /// </summary>
        public static IDataProvider LinqToDbDataProvider => new SqlServerDataProvider(ProviderName.SqlServer, SqlServerVersion.v2017);

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        public virtual bool BackupSupported => true;

        /// <summary>
        /// Database connection string
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected async Task<string> GetCurrentConnectionStringAsync()
        {
            return (await DataSettingsManager.LoadSettingsAsync()).ConnectionString;
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        protected static string GetCurrentConnectionString()
        {
            return DataSettingsManager.LoadSettings().ConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a backup of the database
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task BackupDatabaseAsync(string fileName)
        {
            using var currentConnection = await CreateDataConnectionAsync();
            var commandText = $"BACKUP DATABASE [{currentConnection.Connection.Database}] TO DISK = '{fileName}' WITH FORMAT";
            await currentConnection.ExecuteAsync(commandText);
        }

        /// <summary>
        /// Build the connection string
        /// </summary>
        /// <param name="nopConnectionString">Connection string info</param>
        /// <returns>Connection string</returns>
        public string BuildConnectionString(IDevPlatformConnectionStringInfo nopConnectionString)
        {
            if (nopConnectionString is null)
                throw new ArgumentNullException(nameof(nopConnectionString));

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = nopConnectionString.ServerName,
                InitialCatalog = nopConnectionString.DatabaseName,
                PersistSecurityInfo = false,
                IntegratedSecurity = nopConnectionString.IntegratedSecurity
            };

            if (!nopConnectionString.IntegratedSecurity)
            {
                builder.UserID = nopConnectionString.Username;
                builder.Password = nopConnectionString.Password;
            }

            return builder.ConnectionString;
        }

        /// <summary>
        /// Performs delete records in a table
        /// </summary>
        /// <param name="entities">Entities for delete operation</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task BulkDeleteEntitiesAsync<TEntity>(IList<TEntity> entities) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            if (entities.All(entity => entity.Id == 0))
                foreach (var entity in entities)
                    await dataContext.DeleteAsync(entity);
            else
                await dataContext.GetTable<TEntity>()
                    .Where(e => e.Id.In(entities.Select(x => x.Id)))
                    .DeleteAsync();
        }

        /// <summary>
        /// Performs delete records in a table by a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of deleted records
        /// </returns>
        public async Task<int> BulkDeleteEntitiesAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            return await dataContext.GetTable<TEntity>()
                .Where(predicate)
                .DeleteAsync();
        }

        /// <summary>
        /// Performs bulk insert operation for entity colllection.
        /// </summary>
        /// <param name="entities">Entities for insert operation</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task BulkInsertEntitiesAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync(LinqToDbDataProvider);
            await dataContext.BulkCopyAsync(new BulkCopyOptions(), entities.RetrieveIdentity(dataContext));
        }

        /// <summary>
        /// Create the database
        /// </summary>
        /// <param name="collation">Collation</param>
        /// <param name="triesToConnect">Count of tries to connect to the database after creating; set 0 if no need to connect after creating</param>
        public void CreateDatabase(string collation, int triesToConnect = 10)
        {
            if (DatabaseExists())
                return;

            var builder = GetConnectionStringBuilder();

            //gets database name
            var databaseName = builder.InitialCatalog;

            //now create connection string to 'master' dabatase. It always exists.
            builder.InitialCatalog = "master";

            using (var connection = GetInternalDbConnection(builder.ConnectionString))
            {
                var query = $"CREATE DATABASE [{databaseName}]";
                if (!string.IsNullOrWhiteSpace(collation))
                    query = $"{query} COLLATE {collation}";

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Connection.Open();

                command.ExecuteNonQuery();
            }

            if (triesToConnect <= 0)
                return;

            for (var i = 0; i <= triesToConnect; i++)
            {
                if (i == triesToConnect)
                    throw new Exception("Unable to connect to the new database. Please try one more time");

                if (!DatabaseExists())
                    Thread.Sleep(1000);
                else
                    break;
            }
        }

        /// <summary>
        /// Gets the name of a foreign key
        /// </summary>
        /// <param name="foreignTable">Foreign key table</param>
        /// <param name="foreignColumn">Foreign key column name</param>
        /// <param name="primaryTable">Primary table</param>
        /// <param name="primaryColumn">Primary key column name</param>
        /// <returns>Name of a foreign key</returns>
        public string CreateForeignKeyName(string foreignTable, string foreignColumn, string primaryTable, string primaryColumn)
        {
            return $"FK_{foreignTable}_{foreignColumn}_{primaryTable}_{primaryColumn}";
        }

        /// <summary>
        /// Creates a new temporary storage and populate it using data from provided query
        /// </summary>
        /// <param name="storeKey">Name of temporary storage</param>
        /// <param name="query">Query to get records to populate created storage with initial data</param>
        /// <typeparam name="TItem">Storage record mapping class</typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the iQueryable instance of temporary storage
        /// </returns>
        public async Task<ITempDataStorage<TItem>> CreateTempDataStorageAsync<TItem>(string storeKey, IQueryable<TItem> query)
            where TItem : class
        {
            return new TempSqlDataStorage<TItem>(storeKey, query, await CreateDataConnectionAsync());
        }

        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <returns>Returns true if the database exists.</returns>
        public bool DatabaseExists()
        {
            try
            {
                using var connection = GetInternalDbConnection(GetCurrentConnectionString());
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the returns true if the database exists.
        /// </returns>
        public async Task<bool> DatabaseExistsAsync()
        {
            try
            {
                await using var connection = GetInternalDbConnection(await GetCurrentConnectionStringAsync());
                await connection.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes record in table. Record to delete identified
        /// by match on primary key value from obj value.
        /// </summary>
        /// <param name="entity">Entity for delete operation</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            await dataContext.DeleteAsync(entity);
        }

        /// <summary>
        /// Executes command asynchronously and returns number of affected records
        /// </summary>
        /// <param name="sql">Command text</param>
        /// <param name="dataParameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of records, affected by command execution.
        /// </returns>
        public async Task<int> ExecuteNonQueryAsync(string sql, params DataParameter[] dataParameters)
        {
            using var dataContext = await CreateDataConnectionAsync();
            var command = new CommandInfo(dataContext, sql, dataParameters);
            var affectedRecords = await command.ExecuteAsync();
            UpdateOutputParameters(dataContext, dataParameters);
            return affectedRecords;
        }

        /// <summary>
        /// Returns mapped entity descriptor.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Mapping descriptor</returns>
        public EntityDescriptor GetEntityDescriptor<TEntity>() where TEntity : BaseEntity
        {
            return GetMappingSchema()?.GetEntityDescriptor(typeof(TEntity));
        }


        /// <summary>
        /// Gets the name of an index
        /// </summary>
        /// <param name="targetTable">Target table name</param>
        /// <param name="targetColumn">Target column name</param>
        /// <returns>Name of an index</returns>
        public string GetIndexName(string targetTable, string targetColumn)
        {
            return $"IX_{targetTable}_{targetColumn}";
        }

        /// <summary>
        /// Returns queryable source for specified mapping class for current connection,
        /// mapped to database table or view.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the queryable source
        /// </returns>
        public async Task<ITable<TEntity>> GetTableAsync<TEntity>() where TEntity : BaseEntity
        {
            return new DataContext(LinqToDbDataProvider, await GetCurrentConnectionStringAsync()) { MappingSchema = GetMappingSchema() }
                .GetTable<TEntity>();
        }

        /// <summary>
        /// Returns queryable source for specified mapping class for current connection,
        /// mapped to database table or view.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Queryable source</returns>
        public ITable<TEntity> GetTable<TEntity>() where TEntity : BaseEntity
        {
            return new DataContext(LinqToDbDataProvider, GetCurrentConnectionString()) { MappingSchema = GetMappingSchema() }
                .GetTable<TEntity>();
        }
        /// <summary>
        /// Get the current identity value
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the integer identity; null if cannot get the result
        /// </returns>
        public async Task<int?> GetTableIdentAsync<TEntity>() where TEntity : BaseEntity
        {
            using var currentConnection = await CreateDataConnectionAsync();
            var tableName = GetEntityDescriptor<TEntity>().TableName;

            var result = currentConnection.Query<decimal?>($"SELECT IDENT_CURRENT('[{tableName}]') as Value")
                .FirstOrDefault();

            return result.HasValue ? Convert.ToInt32(result) : 1;
        }

        /// <summary>
        /// Initialize database
        /// </summary>
        public void InitializeDatabase()
        {
            var migrationManager = EngineContext.Current.Resolve<IMigrationManager>();
            migrationManager.ApplyUpMigrations(typeof(DevPlatformDbStartup).Assembly);
        }

        /// <summary>
        /// Inserts record into table. Returns inserted entity with identity
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>Inserted entity</returns>
        public TEntity InsertEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            using var dataContext = CreateDataConnection();
            entity.Id = dataContext.InsertWithInt32Identity(entity);
            return entity;
        }

        /// <summary>
        /// Inserts record into table. Returns inserted entity with identity
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the inserted entity
        /// </returns>
        public async Task<TEntity> InsertEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            entity.Id = await dataContext.InsertWithInt32IdentityAsync(entity);
            return entity;
        }

        /// <summary>
        /// Executes command using System.Data.CommandType.StoredProcedure command type and
        /// returns results as collection of values of specified type
        /// </summary>
        /// <typeparam name="T">Result record type</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the returns collection of query result records
        /// </returns>
        public async Task<IList<T>> QueryProcAsync<T>(string procedureName, params DataParameter[] parameters)
        {
            using var dataContext = await CreateDataConnectionAsync();
            var command = new CommandInfo(dataContext, procedureName, parameters);
            var rez = command.QueryProc<T>().ToList();
            UpdateOutputParameters(dataContext, parameters);
            return rez;
        }

        /// <summary>
        /// Executes SQL command and returns results as collection of values of specified type
        /// </summary>
        /// <typeparam name="T">Type of result items</typeparam>
        /// <param name="sql">SQL command text</param>
        /// <param name="parameters">Parameters to execute the SQL command</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the collection of values of specified type
        /// </returns>
        public async Task<IList<T>> QueryAsync<T>(string sql, params DataParameter[] parameters)
        {
            using var dataContext = await CreateDataConnectionAsync();
            return dataContext.Query<T>(sql, parameters).ToList();
        }

        /// <summary>
        /// Re-index database tables
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task ReIndexTablesAsync()
        {
            using var currentConnection = await CreateDataConnectionAsync();
            var commandText = $@"
                    DECLARE @TableName sysname 
                    DECLARE cur_reindex CURSOR FOR
                    SELECT table_name
                    FROM [{currentConnection.Connection.Database}].information_schema.tables
                    WHERE table_type = 'base table'
                    OPEN cur_reindex
                    FETCH NEXT FROM cur_reindex INTO @TableName
                    WHILE @@FETCH_STATUS = 0
                        BEGIN
                            exec('ALTER INDEX ALL ON [' + @TableName + '] REBUILD')
                            FETCH NEXT FROM cur_reindex INTO @TableName
                        END
                    CLOSE cur_reindex
                    DEALLOCATE cur_reindex";

            await currentConnection.ExecuteAsync(commandText);
        }

        /// <summary>
        /// Restores the database from a backup
        /// </summary>
        /// <param name="backupFileName">The name of the backup file</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task RestoreDatabaseAsync(string backupFileName)
        {
            using var currentConnection = await CreateDataConnectionAsync();
            var commandText = string.Format(
                "DECLARE @ErrorMessage NVARCHAR(4000)\n" +
                "ALTER DATABASE [{0}] SET OFFLINE WITH ROLLBACK IMMEDIATE\n" +
                "BEGIN TRY\n" +
                "RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH REPLACE\n" +
                "END TRY\n" +
                "BEGIN CATCH\n" +
                "SET @ErrorMessage = ERROR_MESSAGE()\n" +
                "END CATCH\n" +
                "ALTER DATABASE [{0}] SET MULTI_USER WITH ROLLBACK IMMEDIATE\n" +
                "IF (@ErrorMessage is not NULL)\n" +
                "BEGIN\n" +
                "RAISERROR (@ErrorMessage, 16, 1)\n" +
                "END",
                currentConnection.Connection.Database,
                backupFileName);

            await currentConnection.ExecuteAsync(commandText);
        }

        /// <summary>
        /// Set table identity (is supported)
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="ident">Identity value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SetTableIdentAsync<TEntity>(int ident) where TEntity : BaseEntity
        {
            using var currentConnection = await CreateDataConnectionAsync();
            var currentIdent = await GetTableIdentAsync<TEntity>();
            if (!currentIdent.HasValue || ident <= currentIdent.Value)
                return;

            var tableName = GetEntityDescriptor<TEntity>().TableName;

            await currentConnection.ExecuteAsync($"DBCC CHECKIDENT([{tableName}], RESEED, {ident})");
        }


        /// <summary>
        /// Updates records in table, using values from entity parameter. 
        /// Records to update are identified by match on primary key value from obj value.
        /// </summary>
        /// <param name="entities">Entities with data to update</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task UpdateEntitiesAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            await dataContext.GetTable<TEntity>()
                .Merge()
                .Using(entities)
                .OnTargetKey()
                .UpdateWhenMatched()
                .MergeAsync();
        }

        /// <summary>
        /// Updates record in table, using values from entity parameter. 
        /// Record to update identified by match on primary key value from obj value.
        /// </summary>
        /// <param name="entity">Entity with data to update</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            using var dataContext = await CreateDataConnectionAsync();
            await dataContext.UpdateAsync(entity);
        }

        #endregion
    }
}
