using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;
using WebBackupAPI.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DestinationsController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<DestinationsController>
        [HttpGet]
        public List<Destination> Get()
        {
            return context.Destinations.ToList();
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id}")]
        public List<Destination> GetConf(int id)
        {
            return context.Destinations.Where(x => x.ConfigId == id).ToList();
        }

        [HttpGet("dest/{id}")]
        public Destination GetDest(int id)
        {
            return context.Destinations.Find(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public Destination Post(Destination destination)
        {
            context.Destinations.Add(destination);
            context.SaveChanges();
            return destination;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public Destination Update(int id, Destination newDest)
        {
            Destination dbDest = context.Destinations.Find(id);
            dbDest.SaveType = newDest.SaveType;
            dbDest.IP_Address = newDest.IP_Address;
            dbDest.Username = newDest.Username;
            dbDest.Password = newDest.Password;
            dbDest.Path = newDest.Path;
            dbDest.SaveFile = newDest.SaveFile;

            context.SaveChanges();

            return dbDest;
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Destination destination = context.Destinations.Find(id);
            if (destination is null) return;
            context.Destinations.Remove(destination);
            context.SaveChanges();
        }
    }
}
