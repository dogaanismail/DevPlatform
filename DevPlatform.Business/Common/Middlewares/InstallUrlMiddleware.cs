using DevPlatform.Core;
using DevPlatform.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.Middlewares
{
    /// <summary>
    /// Represents middleware that checks whether database is installed and redirects to installation URL in otherwise
    /// </summary>
    public class InstallUrlMiddleware
    {
        #region Constants
        public static string InstallPath => "api/InstallDatabase/install";

        #endregion

        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public InstallUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke middleware actions
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="webHelper">Web helper</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InvokeAsync(HttpContext context, IWebHelper webHelper)
        {
            //whether database is installed
            if (!await DataSettingsManager.IsDatabaseInstalledAsync())
            {
                var installUrl = $"{webHelper.GetStoreLocation()}{InstallPath}";
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.Redirect(installUrl);
                    return;
                }
            }
            await _next(context);
        }

        #endregion
    }
}
