using Microsoft.Extensions.Hosting;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace MyQuartzWorkerService
{
    public class QuartzWorker : BackgroundService
    {
        private readonly IScheduler _scheduler;

        public QuartzWorker(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _scheduler.Start(stoppingToken);

            var job = JobBuilder.Create<WorkerJob>()
                .WithIdentity("workerJob", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("workerTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10) // Adjust the interval as needed
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger, stoppingToken);
        }
    }
}
