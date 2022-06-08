using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Text;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using WebBackupAPI.Models;
using System.Threading;

namespace WebBackupAPI.MailJob
{
    public class EmailServiceJob : IJob
    {
        private MyContext dbContext = new MyContext();

        private readonly SmtpSender sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
        {
            UseDefaultCredentials = false,
            Port = 587,
            Credentials = new NetworkCredential("backupapiemail@gmail.com", "123456Ab"),
            EnableSsl = true,
        });

        //private readonly SmtpSender sender = new SmtpSender(() => new SmtpClient("localhost")
        //{
        //    EnableSsl = false,
        //    DeliveryMethod = SmtpDeliveryMethod.Network,
        //    Port = 25
        //});

        public async Task Execute(IJobExecutionContext context)
        {
            int confId = Convert.ToInt32(context.JobDetail.Description);
            Config curConf = dbContext.Configs.Find(confId);
            //Thread.Sleep(1000 * 10 * curConf.PackageCount);

            //wait for all backups
            List<Connection> connections = dbContext.Connections.Where(con => con.ConfigId == confId && con.Connected).ToList();
            bool backingup = true;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            while (backingup)
            {
                backingup = false;
                foreach (Connection connection in connections)
                {
                    Thread.Sleep(1000);
                    MyContext updatedDbContext = new MyContext();
                    List<myEvent> events = updatedDbContext.Events.Where(e => e.ConnectionId == connection.Id
                                                                       && (e.Type == 0 || e.Type == 1))
                                                           .ToList();
                    events = events.Where(e => e.Time.ToString("yyyy-MM-dd") == today).ToList();
                    if (events.Count <= 0) backingup = true;
                }
            }

            GenerateMail mailGen = new GenerateMail();
            string template = mailGen.Generate(curConf);

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = Email
                .From("backupapiemail@gmail.com", "Backup API")
                .To(curConf.Email, "Admin")
                .Subject($"Backups summary ({DateTime.Now.ToString("dd-MM-yyyy")})")
                .UsingTemplate(template, new { })
                .Send();
        }
    }
}
