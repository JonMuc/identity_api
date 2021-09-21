using System;

namespace Job.Job
{
    public class ImportarNoticiasG1PrincipaisJob : BaseJob
    {
        public override void Process()
        {
            Console.Out.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!");
        }

        protected override void Init()
        {
            Console.Out.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!");
        }
        //public async Task Execute(IJobExecutionContext context)
        //{
        //    await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        //}
    }
}
