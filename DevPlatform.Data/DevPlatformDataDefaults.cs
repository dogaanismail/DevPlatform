namespace DevPlatform.Data
{
    /// <summary>
    /// Represents default values related to DevPlatformDataDefaults 
    /// </summary>
    public static partial class DevPlatformDataDefaults
    {
        /// <summary>
        /// Gets a path to the file that contains script to create SQL Server stored procedures
        /// </summary>
        public static string SqlServerStoredProceduresFilePath => "~/App_Data/Install/SqlServer.StoredProcedures.sql";

        /// <summary>
        /// Gets a path to the file that contains script to create MySQL stored procedures
        /// </summary>
        public static string MySQLStoredProceduresFilePath => "~/App_Data/Install/MySQL.StoredProcedures.sql";
    }
}
