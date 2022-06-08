using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using WebBackupAPI.Authorization;
using WebBackupAPI.MailJob;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigsController : ControllerBase
    {
        private MyContext context = new MyContext ();

        // GET: api/<ConfigsController>
        [HttpGet]
        public List<Config> Get()
        {
            return context.Configs.ToList();
        }

        //GET api/<ConfigsController>/5
        [HttpGet("{id}")]
        public Config Get(int id)
        {
            return context.Configs.Find(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public Config Post(Config config)
        {
            context.Configs.Add(config);
            config.Active = true;
            context.SaveChanges();
            if(config.Alias == null)
            {
                config.Alias = ($"Config{config.Id}");
                context.SaveChanges();
            }

            foreach(Deamon deamon in context.Deamons)
            {
                Connection connection = new Connection();
                context.Connections.Add(connection);
                connection.ConfigId = config.Id;
                connection.DeamonId = deamon.Id;
                connection.Connected = false;
            }
            context.SaveChanges();

            MailJobEmployer.createMailJob(config);
            return config;
        }


        // PUT api/<ConfigsController>/5
        [HttpPut("{id}")]
        public Config Update(int id, Config newConf)
        {
            Config dbConf = this.context.Configs.Find(id);
            dbConf.Alias = newConf.Alias;
            dbConf.BackupType = newConf.BackupType;
            dbConf.PackageSize = newConf.PackageSize;
            dbConf.PackageCount = newConf.PackageCount;
            dbConf.TimePeriod = newConf.TimePeriod;
            dbConf.Email = newConf.Email;
            dbConf.MailTimeCron = newConf.MailTimeCron;
            dbConf.Active = newConf.Active;

            context.SaveChanges();

            foreach (Connection connection in context.Connections.Where(con => con.ConfigId == id && con.Connected).ToList())
            {
                context.Database.ExecuteSqlRaw($"delete from tbPendingChanges where DeamonId = {connection.DeamonId} AND ConfigId = {id}");
                context.Database.ExecuteSqlRaw($"INSERT INTO `tbPendingChanges`(`DeamonId`, `ConfigId`, `ChangeType`) VALUES ({connection.DeamonId},{id},0);");
            }

            MailJobEmployer.createMailJob(newConf);
            return newConf;
        }

        

        // DELETE api/<ConfigsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Config config = context.Configs.Find(id);
            if (config is null) return;

            foreach (Connection connection in context.Connections.Where(con => con.ConfigId == id && con.Connected).ToList())
            {
                context.Database.ExecuteSqlRaw($"delete from tbPendingChanges where DeamonId = {connection.DeamonId} AND ConfigId = {id}");
                context.Database.ExecuteSqlRaw($"INSERT INTO `tbPendingChanges`(`DeamonId`, `ConfigId`, `ChangeType`) VALUES ({connection.DeamonId},{id},2);");
            }

            context.Configs.Remove(config);
            context.SaveChanges();
        }
    }
}
