namespace DevPlatform.Data
{
    /// <summary>
    /// Represents a data provider manager
    /// </summary>
    public partial interface IDataProviderManager
    {
        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        IDevPlatformDataProvider DataProvider { get; }

        #endregion
    }
}
