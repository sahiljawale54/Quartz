using Quartz;
using System;
using System.Threading.Tasks;

namespace MyQuartzWorkerService
{
    public class WorkerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Quartz job executed at: " + DateTime.Now);
            await Task.CompletedTask; // Ensure Task.CompletedTask is returned to indicate job completion
        }
    }
}
