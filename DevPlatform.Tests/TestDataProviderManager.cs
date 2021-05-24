using DevPlatform.Data;

namespace DevPlatform.Tests
{
    /// <summary>
    /// Represents the data provider manager
    /// </summary>
    public partial class TestDataProviderManager : IDataProviderManager
    {
        #region Methods

        /// <summary>
        /// Gets the data provider
        /// </summary>
        public IDevPlatformDataProvider DataProvider => new MsSqlDataProvider();

        #endregion
    }
}
