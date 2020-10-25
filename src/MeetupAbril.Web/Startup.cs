using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace MeetupAbril.Web
{
    public class Startup
    {
        private static readonly string WebDirectory = "../MeetupAbril.Web/ClientApp";
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().Services
                    .AddSpaStaticFiles(configuration => configuration.RootPath = Path.Combine(Directory.GetCurrentDirectory(), WebDirectory + "/build"));
            return services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static IApplicationBuilder Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), WebDirectory);
                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });
            return app;
        }
    }
}
