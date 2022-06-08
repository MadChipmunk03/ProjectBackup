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
    [Authorize]
    public class EventsController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<EventsController>
        [HttpGet]
        public List<myEvent> Get()
        {
            return context.Events.ToList();
        }

        /*// GET api/<EventsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ValuesController>
        [HttpPost]
        public myEvent Post(myEvent myEv)
        {
            context.Events.Add(myEv);
            context.SaveChanges();
            return myEv;
        }

        /*// PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
