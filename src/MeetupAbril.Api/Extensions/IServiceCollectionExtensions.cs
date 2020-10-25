using Hellang.Middleware.ProblemDetails;
using MeetupAbril.Api;
using MeetupAbril.Api.Interfaces;
using MeetupAbril.Api.Models;
using MeetupAbril.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCustomBehavior(this IServiceCollection services) =>
            services
            .Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal)
            .Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = false;
                config.SuppressInferBindingSourcesForParameters = false;

                config.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "hola 🙂🙂"
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                         {
                                "application/problem+json",
                                "application/problem+xml"
                         }
                    };
                };
            });

        public static IServiceCollection AddCustomController(this IServiceCollection services) =>
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }).Services;

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services, IConfiguration configuration) =>
            services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(configuration.GetValue<int>("ApiVersion:Major"), configuration.GetValue<int>("ApiVersion:Minor"));
                setup.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-version"), new QueryStringApiVersionReader("api-version"));
            });

        public static IServiceCollection AddCustomServices(this IServiceCollection services) =>
            services
                .AddTransient<IDependencyTransient,  Dependency>()
                .AddScoped<IDependencyScoped, Dependency>()
                .AddSingleton<IDependencySingleton, Dependency>()
                .AddScoped<DependencyService, DependencyService>();
        
    }
}
