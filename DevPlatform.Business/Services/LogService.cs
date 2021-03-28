using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Logging;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// LogService class implementation 
    /// </summary>
    public partial class LogService : ILogService
    {
        #region Fields
        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public LogService(IRepository<Log> logRepository,
           IWebHelper webHelper)
        {
            _logRepository = logRepository;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        public Task ClearLogAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteLogAsync(Log log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            await _logRepository.DeleteAsync(log);
        }


        /// <summary>
        /// Deletes a log items
        /// </summary>
        /// <param name="logs">Log items</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteLogsAsync(IList<Log> logs)
        {
            await _logRepository.DeleteAsync(logs);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="customer">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ErrorAsync(string message, Exception exception = null, AppUser appUser = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Error))
                await InsertLogAsync(LogLevel.Error, message, exception?.ToString() ?? string.Empty, appUser);
        }


        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the log item
        /// </returns>
        public virtual async Task<Log> GetLogByIdAsync(int logId)
        {
            //TODO: GetByIdAsync must be added into system. (To BaseDataProvider)
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the log items
        /// </returns>
        public Task<IList<Log>> GetLogByIdsAsync(int[] logIds)
        {
            //TODO: GetByIdAsync must be added into system. (To BaseDataProvider)
            throw new NotImplementedException();
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="appUser">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InformationAsync(string message, Exception exception = null, AppUser appUser = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Information))
                await InsertLogAsync(LogLevel.Information, message, exception?.ToString() ?? string.Empty, appUser);
        }

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
        public virtual async Task<Log> InsertLogAsync(LogLevel logLevel, string shortMessage, string fullMessage = "", AppUser appUser = null)
        {

            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                CustomerId = appUser?.Id,
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            await _logRepository.InsertAsync(log);

            return log;
        }

        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => false,
                _ => true,
            };
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        /// <param name="appUser">AppUser</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task WarningAsync(string message, Exception exception = null, AppUser appUser = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Warning))
                await InsertLogAsync(LogLevel.Warning, message, exception?.ToString() ?? string.Empty, appUser);
        }

        #endregion
    }
}
