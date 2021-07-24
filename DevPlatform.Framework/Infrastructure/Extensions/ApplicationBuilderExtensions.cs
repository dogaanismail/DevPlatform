using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net;
using static DevPlatform.Framework.Hubs.SignalRHubs;

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
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();

            application.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return;

                    try
                    {
                        if (await DataSettingsManager.IsDatabaseInstalledAsync())
                            await EngineContext.Current.Resolve<ILogService>().ErrorAsync(exception.Message, exception);
                    }
                    finally
                    {
                        var code = HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)code;
                        await context.Response.WriteAsync(exception.Message);
                    }
                });
            });
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

            //using cors
            application.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
        /// Configure authentication
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformAuthentication(this IApplicationBuilder application)
        {
            application.UseAuthentication();
        }

        /// <summary>
        /// Configure authorization
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformAuthorization(this IApplicationBuilder application)
        {
            application.UseAuthorization();
        }

        /// <summary>
        /// Configure routging
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseDevPlatformRouting(this IApplicationBuilder application)
        {
            application.UseRouting();
        }

        /// <summary>
        /// Adds DevPlatform Signalr Hubs
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevPlatformSignalR(this IApplicationBuilder application)
        {
            application.UseEndpoints(routes =>
            {
                routes.MapHub<VideoNotificationHub>("/notificationHub");
                //TODO: can be added other hubs
            });
        }
    }
}
