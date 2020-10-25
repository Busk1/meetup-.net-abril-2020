using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MeetupAbril.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", optional: false)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

            var builder = WebHost
                .CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>();

            return builder;
        }
    }
}
