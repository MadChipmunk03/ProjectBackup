using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBackupAPI.Models;

namespace WebBackupAPI.MailJob
{
    public class GenerateMail
    {
        private MyContext dbContext = new MyContext();

        public string Generate(Config curConf)
        {
            List<myEvent> backups = new List<myEvent>();
            List<Connection> confsConnections = dbContext.Connections.Where(con => con.ConfigId == curConf.Id).ToList();
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (Connection connection in confsConnections)
            {
                List<myEvent> backupEvents = dbContext.Events.Where(ev => connection.Id == ev.ConnectionId && (ev.Type == 0 || ev.Type == 1)).ToList(); //
                backupEvents = backupEvents.Where(e => e.Time.ToString("yyyy-MM-dd") == today).ToList();
                backups.AddRange(backupEvents);
            }

            //values to be inserted into mail
            string notification = "After backup";
            if (curConf.TimePeriod == "0 0 0 ? * Mon") notification = "Week summary";
            List<Source> sources = dbContext.Sources.Where(src => src.ConfigId == curConf.Id).ToList();
            List<Destination> destinations = dbContext.Destinations.Where(dest => dest.ConfigId == curConf.Id).ToList();

            StringBuilder template = new();
            //template.AppendLine("<!DOCTYPE html>");
            //template.AppendLine("<html>");
            //template.AppendLine("<style>table, th, td {border:1px solid black;border-collapse: collapse;}</style>");
            //template.AppendLine("<body>");
            template.AppendLine("<h1>Notifikace z API</h1>");
            template.AppendLine($"<h2>Dané skrnutí se týká backupu: {curConf.Alias}</h2>");
            //info about config
            template.AppendLine($"<ul><li>Id: {curConf.Id}</li><li>Package count: {curConf.PackageCount}</li><li>Package size: {curConf.PackageSize}</li>");
            template.AppendLine($"<li>Čas zálohy: {curConf.TimePeriod}</li><li>Posílání mailů: {notification}</li><li>Srouces: </li><ul>");
            foreach (Source source in sources)
            {
                template.AppendLine($"<li>{source.SourcePath}</li>");
            }
            template.AppendLine("</ul><li>Destinations: </li><ul>");
            foreach (Destination destination in destinations)
            {
                string saveType = "Unknown saveType";
                if (destination.SaveType == 0) saveType = "LOC";
                if (destination.SaveType == 1) saveType = "NET";
                if (destination.SaveType == 2) saveType = "FTP";
                template.AppendLine($"<li>{saveType} | {destination.Path}</li>");
            }
            template.AppendLine("</ul></ul>");
            //table with backup events
            template.AppendLine($"<h2>Eventy z backupů:</h2>");
            template.AppendLine("<table>");
            template.AppendLine("<tr>");
            template.AppendLine("<th> Deamon ID </th>");
            template.AppendLine("<th> Deamon </th>");
            template.AppendLine("<th> Message </th>");
            template.AppendLine("<th> Time </th>");
            template.AppendLine("<th> Type </th>");
            template.AppendLine("</tr>");
            foreach (myEvent ev in backups)
            {
                string evType = "Unknown type";
                if (ev.Type == 0) evType = "Success ✅";
                if (ev.Type == 1) evType = "Error ⛔";
                if (ev.Type == 2) evType = "Info 📣";
                Connection connection = dbContext.Connections.Find(ev.ConnectionId);
                Deamon deamon = dbContext.Deamons.Find(connection.DeamonId);

                template.AppendLine("<tr>");
                template.AppendLine($"<td> {deamon.Id} </td>");
                template.AppendLine($"<td> {deamon.Alias} </td>");
                template.AppendLine($"<td> {ev.Message} </td>");
                template.AppendLine($"<td> {ev.Time} </td>");
                template.AppendLine($"<td> {evType} </td>");
                template.AppendLine("</tr>");
            }
            template.AppendLine("</table>");
            template.AppendLine("<p> - Vaše Backup service</p>");
            //template.AppendLine("</body>");
            //template.AppendLine("</html>");

            return Convert.ToString(template);
        }
    }
}
