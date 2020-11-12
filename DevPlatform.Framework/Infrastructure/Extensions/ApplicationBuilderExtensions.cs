using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Middlewares;
using DevPlatform.Framework.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerUI;

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
            application.UseMiddleware<ErrorHandlingMiddleware>();
        }

        /// <summary>
        /// Add environment
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformEnvironment(this IApplicationBuilder application)
        {
            var env = EngineContext.Current.Resolve<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }
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

                spa.Options.SourcePath = "DevPlatformSpa";

                if (webHostEnvironment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformSwagger(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevPlatform.Api V1");
                c.DocumentTitle = "Title";
                c.DisplayOperationId();
                c.DocExpansion(DocExpansion.None);

            });
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformAuthentication(this IApplicationBuilder application)
        {
            application.UseAuthentication();
        }

        /// <summary>
        /// Adds DevPlatform Signalr Hubs
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevPlatformSignalR(this IApplicationBuilder application)
        {
            application.UseSignalR(routes =>
            {
                routes.MapHub<VideoNotificationHub>("/notificationHub");
                //TODO: can be added other hubs
            });
        }
    }
}
