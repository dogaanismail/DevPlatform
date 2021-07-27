using System.ComponentModel;
using System.Runtime.Serialization;

namespace DevPlatform.Data
{
    /// <summary>
    /// Represents data provider type enumeration
    /// </summary>
    [DefaultValue(SqlServer)]
    public enum DataProviderType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "")]
        Unknown,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        [EnumMember(Value = "sqlserver")]
        SqlServer,

        /// <summary>
        /// MySQL
        /// </summary>
        [EnumMember(Value = "mysql")]
        MySql,

        /// <summary>
        /// MySQL
        /// </summary>
        [EnumMember(Value = "postgresql")]
        PostgreSQL       
    }
}
