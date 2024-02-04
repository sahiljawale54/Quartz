using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

namespace MyQuartzWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register IScheduler service
                    var schedulerFactory = new StdSchedulerFactory();
                    var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
                    services.AddSingleton<IScheduler>(scheduler);

                    services.AddHostedService<QuartzWorker>();
                });
    }
}
