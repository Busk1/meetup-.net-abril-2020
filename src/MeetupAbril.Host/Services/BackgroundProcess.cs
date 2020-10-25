using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeetupAbril.Host.Services
{
    public class BackgroundProcess : IHostedService, IDisposable
    {
        private Timer _timer;
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer((object state) =>
            {
                //System.IO.File.AppendAllTextAsync(@"C:\MeetupAbril\BackgroundProcess.txt", "prueba");
            }, null, 0, 1000);
                
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
