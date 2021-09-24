using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Jobs.Job
{
    public abstract class BaseJob : IJob, IDisposable
    {
        protected bool IsRunning = false;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ConcurrentDictionary<IJob, IServiceScope> _scopes = new ConcurrentDictionary<IJob, IServiceScope>();

        public void Dispose() { }

        protected abstract void Init();

        public abstract void Process();

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                this.Init();

                if (IsRunning)
                {
                    return;
                }
                IsRunning = true;

                this.Process();

                IsRunning = false;
            }
            catch (Exception e)
            {
                IsRunning = false;
                throw e;
            }
        }
    }
}
