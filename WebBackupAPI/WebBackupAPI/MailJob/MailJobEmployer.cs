using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using Quartz;
using Quartz.Impl;

namespace WebBackupAPI.MailJob
{
    class MailJobEmployer
    {
        public async static void createMailJob(Config conf)
        {
            // 1. Create a scheduler Factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            // 2. Get and start a scheduler
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            string jobName = $"{typeof(EmailServiceJob).Name}-{conf.Id}";

            // Create a "key" for the job
            var jobKey = new JobKey(jobName);

            // 3. Create a job
            IJobDetail job = JobBuilder.Create<EmailServiceJob>()
                    .WithIdentity(jobName)
                    .WithDescription(Convert.ToString(conf.Id))
                    .Build();

            // 4. Create a trigger
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithIdentity($"{jobName}-trigger")
                .WithCronSchedule(conf.MailTimeCron)
                .Build();

            // 5. Schedule the job using the job and trigger 
            try
            {
                await scheduler.ScheduleJob(job, trigger);
            }
            catch
            {
                scheduler.RescheduleJob(new TriggerKey($"{jobName}-trigger"), trigger);
            }
        }
    }
}
