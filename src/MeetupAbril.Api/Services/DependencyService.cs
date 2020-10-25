using MeetupAbril.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetupAbril.Api.Services
{
    public class DependencyService
    {
        public Guid _scopedDependency;
        public Guid _singletonDependency;
        public Guid _transientDependency;

        public DependencyService(IDependencyTransient transientDependency, IDependencyScoped scopedDependency, IDependencySingleton singletonDependency)
        {
            _scopedDependency = scopedDependency.DependencyId;
            _singletonDependency = singletonDependency.DependencyId;
            _transientDependency = transientDependency.DependencyId;
        }
    }
}
