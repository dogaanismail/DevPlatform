﻿using System.Runtime.Serialization;

namespace DevPlatform.Data
{
    /// <summary>
    /// Represents data provider type enumeration
    /// </summary>
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
        MySql
    }
}
