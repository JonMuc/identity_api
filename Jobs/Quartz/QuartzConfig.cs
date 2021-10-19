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

            //JOB G1 NOTICIAS PRINCIPAIS
            //IJobDetail job = JobBuilder.Create<ImportarNoticiasG1PrincipaisJob>()
            //    .WithIdentity("myJob", "group1")
            //    .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(20)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job, trigger);


            //JOB GOOGLE NEWS PRINCIPAIS
            //IJobDetail job2 = JobBuilder.Create<ImportarNoticiasGooglePrincipaisJob>()
            //    .WithIdentity("myJob2", "group2")
            //    .Build();

            //ITrigger trigger2 = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger2", "group2")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(30)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job2, trigger2);


            //JOB GOOGLE NEWS CIENCIA E TECNOLOGIA
            //IJobDetail job3 = JobBuilder.Create<ImportarNoticiasGoogleCienciaTecnologiaJob>()
            //    .WithIdentity("myJob3", "group3")
            //    .Build();

            //ITrigger trigger3 = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger3", "group3")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(30)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job3, trigger3);


            //JOB GOOGLE NEWS MUNDO
            //IJobDetail job4 = JobBuilder.Create<ImportarNoticiasGoogleMundoJob>()
            //    .WithIdentity("myJob4", "group4")
            //    .Build();

            //ITrigger trigger4 = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger4", "group4")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(30)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job4, trigger4);


            //JOB GOOGLE NEWS NEGOCIOS
            //IJobDetail job5 = JobBuilder.Create<ImportarNoticiasGoogleNegociosJob>()
            //    .WithIdentity("myJob5", "group5")
            //    .Build();

            //ITrigger trigger5 = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger5", "group5")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(30)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job5, trigger5);


            //JOB GOOGLE NEWS ENTRETENIMENTO
            //IJobDetail job6 = JobBuilder.Create<ImportarNoticiasGoogleEntretenimentoJob>()
            //    .WithIdentity("myJob6", "group6")
            //    .Build();

            //ITrigger trigger6 = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger6", "group6")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(30)
            //        .RepeatForever())
            //    .Build();
            //scheduler.Result.ScheduleJob(job6, trigger6);


            //JOB GOOGLE NEWS ESPORTES
            IJobDetail job7 = JobBuilder.Create<ImportarNoticiasGoogleEsportesJob>()
                .WithIdentity("myJob7", "group7")
                .Build();

            ITrigger trigger7 = TriggerBuilder.Create()
                .WithIdentity("myTrigger7", "group7")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
            scheduler.Result.ScheduleJob(job7, trigger7);


            scheduler.Result.Start();
        }
    }
}
