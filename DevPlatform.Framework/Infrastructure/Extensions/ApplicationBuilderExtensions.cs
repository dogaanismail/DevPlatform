using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevPlatform.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformExceptionHandler(this IApplicationBuilder application)
        {
            var devPlatformConfig = EngineContext.Current.Resolve<DevPlatformConfig>();
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();
            var useDetailedExceptionPage = devPlatformConfig.DisplayFullErrorStack || webHostEnvironment.IsDevelopment();

            if (useDetailedExceptionPage)
            {
                //get detailed exceptions for developing and testing purposes
                application.UseDeveloperExceptionPage();
            }
            else
            {
                //or use special exception handler
                application.UseExceptionHandler("/Error/Error");
            }

            application.UseMiddleware<ErrorHandlingMiddleware>();
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformStaticFiles(this IApplicationBuilder application)
        {
            application.UseHttpsRedirection()
             .UseResponseCompression()
             .UseStaticFiles(new StaticFileOptions
             {
                 // 6 hour cache
                 OnPrepareResponse =
                     _ => _.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=21600"
             })
             .UseSpaStaticFiles();
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAngular(this IApplicationBuilder application)
        {
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();

            application.UseMvc()
            .UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "FriendFinderSpa";

                if (webHostEnvironment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        /// Adds the authentication middleware, which enables authentication capabilities.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseDevAuthentication(this IApplicationBuilder application)
        //{
        //    //check whether database is installed
        //    if (!DataSettingsManager.DatabaseIsInstalled)
        //        return;

        //    application.UseMiddleware<AuthenticationMiddleware>();
        //}


    }
}
