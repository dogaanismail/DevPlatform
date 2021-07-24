using System.Threading.Tasks;
using DevPlatform.Business.Configuration.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DevPlatform.Api
{
    public class Program
    {
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
               .UseDefaultServiceProvider(options =>
               {
                   options.ValidateScopes = false;
                   options.ValidateOnBuild = false;
               })
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .ConfigureAppConfiguration(config =>
                    {
                        config
                            .AddJsonFile(DevPlatformConfigurationDefaults.AppConfigsFilePath, true, true)
                            .AddEnvironmentVariables();
                    })
                    .UseStartup<Startup>())
                .Build();

            //start the program, a task will be completed when the host starts
            await host.StartAsync();

            //a task will be completed when shutdown is triggered
            await host.WaitForShutdownAsync();
        }
    }
}
