using MeetupAbril.Api.Interfaces;
using MeetupAbril.Api.Models;
using MeetupAbril.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAbril.Api.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class DependenciesController: ControllerBase
    {
        private readonly DependencyService _dependencyService;
        private readonly IDependencyTransient _transientDependency;
        private readonly IDependencyScoped _scopedDependency;
        private readonly IDependencySingleton _singletonDependency;
        private readonly IConfiguration _configuration;

        public DependenciesController(DependencyService DependencyService, IDependencyTransient transientDependency, IDependencyScoped scopedDependency, IDependencySingleton singletonDependency, IConfiguration configuration)
        {
            _dependencyService = DependencyService;
            _transientDependency = transientDependency;
            _scopedDependency = scopedDependency;
            _singletonDependency = singletonDependency;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> DependencyInjection()
        {
                return Ok(new
                {
                    transient = _transientDependency.DependencyId,
                    scoped = _scopedDependency.DependencyId,
                    singleton = _singletonDependency.DependencyId,
                    service = new
                    {
                        transient = _dependencyService._transientDependency,
                        scoped = _dependencyService._scopedDependency,
                        singleton = _dependencyService._singletonDependency
                    }
                });
        }

        [HttpGet]
        public async Task<IActionResult> Configuration()
        {
            return Ok(new
            {
                secret = _configuration["instagram"],
                environment = _configuration["linkedin"],
                appsettings = _configuration["Twitter"],                
                valor = _configuration["Algo:Algo2:Algo3:valor"]
            });
        }
    }
}
