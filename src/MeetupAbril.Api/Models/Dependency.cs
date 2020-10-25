using MeetupAbril.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace MeetupAbril.Api.Models
{
    public class Dependency : IDependencyScoped, IDependencySingleton, IDependencyTransient
    {
        Guid _guid;
        public Dependency()
        {
            _guid = Guid.NewGuid();
        }

        public Guid DependencyId => _guid;

    }
}
