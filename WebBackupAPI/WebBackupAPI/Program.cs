using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using WebBackupAPI.Models;
using WebBackupAPI.MailJob;

namespace WebBackupAPI
{
    public class Program
    {
        private MyContext dbContext = new MyContext();

        public static async Task Main(string[] args)
        {
            AddAllJobs procedure = new AddAllJobs();
            procedure.run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
