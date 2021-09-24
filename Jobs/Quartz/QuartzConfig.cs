using Jobs.Job;
using Quartz;
using Quartz.Impl;
using System;

namespace Jobs.Quartz
{
    public class QuartzConfig
    {
        private static IServiceProvider _serviceProvider;


        public QuartzConfig() { }


        public static void Config(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            QuartzConfig config = new QuartzConfig();
            config.InitConfig();
        }

        public void InitConfig()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Result.JobFactory = new JobFactory(_serviceProvider);



            //CRIANDO JOB
            IJobDetail job = JobBuilder.Create<ImportarNoticiasG1PrincipaisJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(20)
                    .RepeatForever())
                .Build();
            scheduler.Result.ScheduleJob(job, trigger);

            IJobDetail job2 = JobBuilder.Create<ImportarNoticiasGooglePrincipaisJob>()
                .WithIdentity("myJob2", "group2")
                .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("myTrigger2", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
            scheduler.Result.ScheduleJob(job2, trigger2);



            scheduler.Result.Start();
        }
    }
}
