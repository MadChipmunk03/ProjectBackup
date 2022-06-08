using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebBackupAPI
{
    public class EmailService
    {
        private SmtpSender sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
        {
            UseDefaultCredentials = false,
            Port = 587,
            Credentials = new NetworkCredential("backupapiemail@gmail.com", "123456Ab"),
            EnableSsl = true,
        });

        public void SendEmail(string msg = "Hello from API email service here!.")
        {
            StringBuilder template = new();
            template.AppendLine("Notifikace z API");
            template.AppendLine("<p>dnes to bude ještě náročné...</p>");
            template.AppendLine("Vaše Backup service");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = Email
                .From("backupapiemail@gmail.com", "Backup API")
                .To("petaslachta@gmail.com", "Petr")
                .Subject("Email service works!")
                //.Body(msg)
                .UsingTemplate(template.ToString(), new { })
                .Send();
        }
    }
}
