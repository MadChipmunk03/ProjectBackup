using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using WebBackupAPI.MailJob;
using System.Net;

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForDeamonController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet("connections/{id}")]
        public List<Connection> GetConnections(int id)
        {
            return context.Connections.Where(con => con.DeamonId == id && con.Connected).ToList();
        }

        // GET deamons changes
        [HttpGet("pendingchanges/{id}")]
        public List<PendingChange> GetChanges(int id)
        {
            //get the changes
            List<PendingChange> changes = context.PendigChanges.Where((pc) => pc.DeamonId == id).ToList(); //pc - pendingChange

            //update deamon status
            if (changes.Count() > 0)
                context.Deamons.Find(id).Status = (context.Deamons.Find(id).Status + 1 + 4) % 4;

            //delete the changes
            foreach (PendingChange change in changes)
            {
                context.PendigChanges.Remove(change);
            }

            context.SaveChanges();

            //return the changes
            return changes;
        }

        //add event
        [HttpPost("event")]
        public myEvent Post(myEvent myEv)
        {
            context.Events.Add(myEv);
            context.SaveChanges();
            return myEv;
        }

        // GET yourself (don't know why tho)
        //[EnableCors("AllowOrigin")]
        //[HttpGet("{id}")]
        //public Deamon GetDeamon(int id)
        //{
        //    return context.Deamons.Find(id);
        //}

        //get config
        [HttpGet("config/{id}")]
        public Config GetConfig(int id)
        {
            return context.Configs.Find(id);
        }

        //deamon
        [EnableCors("AllowOrigin")]
        [HttpGet("register")]
        public Deamon Register(string IP_Address)
        {
            Deamon newDeamon = new Deamon();

            ////IP validation
            //IPAddress ip;
            //bool valid = IPAddress.TryParse(IP_Address, out ip);
            //if (!valid) throw new Exception("Invalid IP address!".ToUpper());

            context.Deamons.Add(newDeamon);
            context.SaveChanges();

            newDeamon.Alias = ($"Deamon{newDeamon.Id}");
            newDeamon.IP_Address = IP_Address;
            newDeamon.Status = 1;
            newDeamon.Password = HandlePassword.RandomPassword(16);
            context.SaveChanges();

            Deamon returnDeamon = new Deamon();
            returnDeamon.Id = newDeamon.Id;
            returnDeamon.Alias = newDeamon.Alias;
            returnDeamon.IP_Address = newDeamon.IP_Address;
            returnDeamon.Status = newDeamon.Status;
            returnDeamon.Password = newDeamon.Password;

            newDeamon.Password = BCrypt.Net.BCrypt.HashPassword(newDeamon.Password + newDeamon.Id);

            //create related connections
            foreach (Config config in context.Configs)
            {
                Connection connection = new Connection();
                context.Connections.Add(connection);
                connection.ConfigId = config.Id;
                connection.DeamonId = returnDeamon.Id;
                connection.Connected = false;
            }

            context.SaveChanges();

            return returnDeamon;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("verify")]
        public bool Verify(int Id, string password)
        {
            Deamon deamon = context.Deamons.Find(Id);
            bool verified = BCrypt.Net.BCrypt.Verify(password + deamon.Id, deamon.Password);
            return verified;
        }
    }
}
