using DevPlatform.LinqToDB.Identity;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using System.Collections.Generic;

namespace DevPlatform.Data.IdentityFactory
{
    public class IdentityConnectionFactory : IConnectionFactory
    {
        private static readonly Dictionary<string, HashSet<string>> _tables = new Dictionary<string, HashSet<string>>();
        private readonly string _configuration;
        private readonly string _connectionString;
        private readonly string _key;
        private readonly IDataProvider _provider;

        public IdentityConnectionFactory(IDataProvider provider, string configuration, string connectionString)
        {
            _provider = provider;
            Configuration.Linq.AllowMultipleQuery = true;
            //DataConnection.AddConfiguration(configuration, connectionString, provider);
            _configuration = configuration;
            _connectionString = connectionString;
            _key = _configuration + "$$" + _connectionString;
        }
        public IDataContext GetContext()
        {
            return new DataContext(_provider, _connectionString);
        }

        public DataConnection GetConnection()
        {
            var db = new IdentityDataConnection(_provider, _connectionString);

            return new DataConnection(_provider, _connectionString);
        }
    }
}
