using DevPlatform.Core.Domain.Identity;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Identity;

namespace DevPlatform.Data
{
    public class DevIdentityDataConnection : IdentityDataConnection<AppUser, AppRole, int>
    {

    }

    public class IdentityDefaultConnection : IConnectionFactory
    {
        public DataConnection GetConnection()
        {
            return new DevIdentityDataConnection();
        }

        public IDataContext GetContext()
        {
            return new DataContext();
        }
    }
}
