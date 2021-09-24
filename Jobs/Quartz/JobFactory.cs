﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Concurrent;

namespace Jobs.Quartz
{
    public class JobFactory : IJobFactory, IDisposable
    {
        protected readonly IServiceScope _scope;

        public JobFactory(IServiceProvider container)
        {
            _scope = container.CreateScope();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var res = _scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            return res;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
