using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace MeetupAbril.Api
{
    public class Startup
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, bool isDevelopment) =>
            services
                .AddApplicationInsightsTelemetry(configuration)
                .AddResponseCompression(configureOptions => configureOptions.EnableForHttps = true)
                .AddCustomController()
                .AddCustomApiVersioning(configuration)
                .ConfigureCustomBehavior()
                .AddProblemDetails(configure => configure.IncludeExceptionDetails = (_, __) => isDevelopment)
                .AddCustomServices();

        public static IApplicationBuilder Configure(IApplicationBuilder app, bool isDevelopment) =>
            app
                .UseIf(isDevelopment, appBuilder => appBuilder.UseDeveloperExceptionPage())
                .UseIf(!isDevelopment, appBuilder => appBuilder.UseExceptionHandler("/Error"))
                .UseProblemDetails()
                .UseCustomCors()
                .UseResponseCompression()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());            
        
    }
}
