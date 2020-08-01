using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Middlewares;
using DevPlatform.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System.Linq;

namespace DevPlatform.Framework.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring MVC on application startup
    /// </summary>
    public class DevPlatformMvcStartup : IDevPlatformStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddResponseCompression(
               options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                               {
                                    "image/jpeg",
                                    "image/png",
                                    "image/gif"
                               }));

            //Angular
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist/"; });

            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
                //opt.Filters.Add(typeof(ValidateModelAttribute));
            });
            //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
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

            application.UseMiddleware<ErrorHandlingMiddleware>();

            application.UseAuthentication();

            application.UseHttpsRedirection()
              .UseResponseCompression()
              .UseStaticFiles(new StaticFileOptions
              {
                  // 6 hour cache
                  OnPrepareResponse =
                      _ => _.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=21600"
              })
              .UseSpaStaticFiles();

            application.UseMvc()
          .UseSpa(spa =>
          {
              // To learn more about options for serving an Angular SPA from ASP.NET Core,
              // see https://go.microsoft.com/fwlink/?linkid=864501

              spa.Options.SourcePath = "ClientApp";

              if (env.IsDevelopment())
              {
                  spa.UseAngularCliServer(npmScript: "start");
              }
          });
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 1000; //MVC should be loaded last
    }
}
