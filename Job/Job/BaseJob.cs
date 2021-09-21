using Quartz;
using System;
using System.Threading.Tasks;

namespace Job.Job
{
    public abstract class BaseJob : IJob, IDisposable
    {
        protected bool IsRunning = false;

        public void Dispose()
        {
            CloseIfOpen();
        }
        protected void CloseIfOpen()
        {
            //NHibernateSession.CloseIfOpen(_session);
            //NHibernateSession.CloseIfOpen(_statelessSession);
        }

        protected abstract void Init();

        public abstract void Process();

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                BeginExecution(context);

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
            finally
            {
                CloseExecution();
                CloseIfOpen();
            }
        }

        protected virtual void CloseExecution()
        {
            //ITransaction transaction = _statelessSession.BeginTransaction();
            //_execucaoService.FinalizarExecucao(_currentExecution.Id);
            //transaction.Commit();
            //_statelessSession.Clear();
        }

        protected virtual void BeginExecution(IJobExecutionContext context)
        {
            //_currentConfiguration = GetQuartzConfiguration(context);
            //ITransaction transaction = _statelessSession.BeginTransaction();
            //_currentExecution = _execucaoService.IniciarExecucao(_currentConfiguration.Id);
            //transaction.Commit();
            //_statelessSession.Clear();
        }
        //protected QuartzConfiguration GetQuartzConfiguration(IJobExecutionContext context)
        //{
        //    // Try Get From JobDataMap.config
        //    object objQuartz;
        //    if (context.JobDetail.JobDataMap.TryGetValue("config", out objQuartz))
        //    {
        //        return (QuartzConfiguration)objQuartz;
        //    }

        //    // Try get from JobDataMap.id
        //    long idQuartzConfiguration = context.JobDetail.JobDataMap.GetLongValue("id");
        //    QuartzConfiguration quartz = _statelessSession.Get<QuartzConfiguration>(idQuartzConfiguration);
        //    if (quartz != null)
        //    {
        //        _statelessSession.Evict(quartz);
        //    }
        //    return quartz;
        //}
    }
}
