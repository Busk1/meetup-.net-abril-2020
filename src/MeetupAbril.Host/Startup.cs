using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetupAbril.Host.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeetupAbril.Host
{
    public class Startup
    {
        static public IConfiguration Configuration { get; set; }
        static public IHostEnvironment Environment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Api.Startup.ConfigureServices(services, Configuration, Environment.IsDevelopment());
            Db.Startup.ConfigureServices(services, Configuration);     
            Web.Startup.ConfigureServices(services);
            services.AddHostedService<BackgroundProcess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection()
                .UseHsts();
            Api.Startup.Configure(app, env.IsDevelopment());
            Web.Startup.Configure(app, env);
        }
    }
}
