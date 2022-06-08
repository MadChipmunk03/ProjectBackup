using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using WebBackupAPI.Authorization;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConnectionsController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<ConnectionsController>
        [HttpGet]
        public List<Connection> Get()
        {
            return context.Connections.ToList();
        }

        // GET api/<ConnectionsController>/5
        [HttpGet("{id}")]
        public Connection Get(int id)
        {
            return context.Connections.Find(id);
        }

        // GET api/<ConnectionsController>/5
        [HttpGet("deamon/{id}")]
        public List<Connection> GetDeamConn(int id)
        {
            return context.Connections.Where(x => x.DeamonId == id).ToList();
        }

        // GET api/<ConnectionsController>/5
        [HttpGet("config/{id}")]
        public List<Connection> GetConfConn(int id)
        {
            return context.Connections.Where(x => x.ConfigId == id).ToList();
        }

        //connected / disconnected
        [HttpPut("{id}")]
        public Connection Put(Connection newConn)
        {
            Connection connection = context.Connections.Find(newConn.Id);

            //pending changes
            context.Database.ExecuteSqlRaw($"delete from tbPendingChanges where DeamonId = {connection.DeamonId} AND ConfigId = {connection.ConfigId}");
            int changeType = 2;
            if (newConn.Connected) changeType = 1;
            context.Database.ExecuteSqlRaw($"INSERT INTO `tbPendingChanges`(`DeamonId`, `ConfigId`, `ChangeType`) VALUES ({connection.DeamonId},{connection.ConfigId},{changeType});");

            //update database
            connection.DeamonId = newConn.DeamonId;
            connection.ConfigId = newConn.ConfigId;
            connection.Connected = newConn.Connected;
            context.SaveChanges();

            return connection;
        }

        /*
        // POST api/<ConnectionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/<ConnectionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
