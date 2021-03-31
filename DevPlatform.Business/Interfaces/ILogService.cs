using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Logging;
using DevPlatform.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface ILogService
    {
        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteLogAsync(Log log);

        /// <summary>
        /// Deletes a log items
        /// </summary>
        /// <param name="logs">Log items</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteLogsAsync(IList<Log> logs);

        /// <summary>
        /// Clears a log
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task ClearLogAsync();

        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the log item
        /// </returns>
        Task<Log> GetLogByIdAsync(int logId);

        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the log items
        /// </returns>
        Task<IList<Log>> GetLogByIdsAsync(int[] logIds);

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="appUser">The appUser to associate log record with</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains a log item
        /// </returns>
        Task<Log> InsertLogAsync(LogLevel logLevel, string shortMessage, string fullMessage = "", AppUser appUser = null);

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="appUser">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InformationAsync(string message, Exception exception = null, AppUser appUser = null);

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="appUser">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task WarningAsync(string message, Exception exception = null, AppUser appUser = null);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="appUser">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task ErrorAsync(string message, Exception exception = null, AppUser appUser = null);
    }
}
