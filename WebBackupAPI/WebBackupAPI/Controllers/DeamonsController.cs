using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using WebBackupAPI;
using WebBackupAPI.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeamonsController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<ValuesController>
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public List<Deamon> Get()
        {
            return context.Deamons.ToList();
        }

        // GET api/<ValuesController>/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public Deamon Get(int id)
        {
            return context.Deamons.Find(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public Deamon Post(Deamon deamon)
        {
            context.Deamons.Add(deamon);
            context.SaveChanges();
            return deamon;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Update(int id, Deamon newDeam)
        {
            Deamon dbDeam = context.Deamons.Find(id);
            dbDeam.Alias = newDeam.Alias;
            dbDeam.IP_Address = newDeam.IP_Address;
            dbDeam.Status = newDeam.Status;

            context.SaveChanges();
        }

        // DELETE - when client leaves
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Deamon deamon = context.Deamons.Find(id);
            if (deamon is null) return;
            context.Deamons.Remove(deamon);
            context.SaveChanges();
        }
    }
}
