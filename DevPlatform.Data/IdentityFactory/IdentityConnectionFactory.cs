using DevPlatform.LinqToDB.Identity;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using System.Collections.Generic;

namespace DevPlatform.Data.IdentityFactory
{
    /// <summary>
    /// IdentityConnection Factory implementation
    /// </summary>
    public class IdentityConnectionFactory : IConnectionFactory
    {
        #region Fields
        private static readonly Dictionary<string, HashSet<string>> _tables = new Dictionary<string, HashSet<string>>();
        private readonly string _configuration;
        private readonly string _connectionString;
        private readonly string _key;
        private readonly IDataProvider _provider;
        #endregion

        #region Ctor
        public IdentityConnectionFactory(IDataProvider provider, string configuration, string connectionString)
        {
            _provider = provider;
            Configuration.Linq.AllowMultipleQuery = true;
            //DataConnection.AddConfiguration(configuration, connectionString, provider);
            _configuration = configuration;
            _connectionString = connectionString;
            _key = _configuration + "$$" + _connectionString;
        }
        #endregion

        #region Methods

        public IDataContext GetContext()
        {
            return new DataContext(_provider, _connectionString);
        }

        public DataConnection GetConnection()
        {
            return new DataConnection(_provider, _connectionString);
        }

        #endregion
    }
}
