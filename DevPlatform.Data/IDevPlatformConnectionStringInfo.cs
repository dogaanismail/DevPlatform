﻿namespace DevPlatform.Data
{
    /// <summary>
    /// Represents a connection string info
    /// </summary>
    public interface IDevPlatformConnectionStringInfo
    {
        /// <summary>
        /// DatabaseName
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Server name or IP adress
        /// </summary>
        string ServerName { get; set; }

        /// <summary>
        /// Integrated security
        /// </summary>
        bool IntegratedSecurity { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        string Password { get; set; }
    }
}
