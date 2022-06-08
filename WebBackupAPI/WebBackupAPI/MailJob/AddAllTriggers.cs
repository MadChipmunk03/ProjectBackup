using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using Quartz;
using Quartz.Impl;

namespace WebBackupAPI.MailJob
{
    public class AddAllJobs
    {
        private MyContext dbContext = new MyContext();

        public async void run()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            // 2. Get and start a scheduler
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            List<Config> configs = dbContext.Configs.ToList();
            foreach (Config config in configs)
            {
                string jobName = $"{typeof(EmailServiceJob).Name}-{config.Id}";

                // Create a "key" for the job
                var jobKey = new JobKey(jobName);

                // 3. Create a job
                IJobDetail job = JobBuilder.Create<EmailServiceJob>()
                        .WithIdentity(jobName)
                        .WithDescription(Convert.ToString(config.Id))
                        .Build();

                // 4. Create a trigger
                ITrigger trigger = TriggerBuilder.Create()
                    .ForJob(jobKey)
                    .WithIdentity($"{jobName}-trigger")
                    .WithCronSchedule(config.MailTimeCron)
                    .Build();

                // 5. Schedule the job using the job and trigger 
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
