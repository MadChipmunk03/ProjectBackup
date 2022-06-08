using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBackupAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBackupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingChangesController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<PendingChangesController>
        //[HttpGet]
        //public List<PendingChange> Get()
        //{
        //    return context.PendigChanges.ToList();
        //}

        // GET api/<PendingChangesController>/5
        //[HttpGet("{id}")]
        //public List<PendingChange> Get(int id)
        //{
        //    return context.PendigChanges.Where((pc) => pc.Id == id).ToList();
        //}

        // POST that all deamons configs were synced - return if it is true
        /*[HttpPost("{id}")]
        public bool Post(int id)
        {
            return context.PendigChanges.Where((ch) => ch.Id == id) is null;
        }*/

        /*
        // PUT api/<PendingChangesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<PendingChangesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    PendingChange pendingChange = context.PendigChanges.Find(id);
        //    if (pendingChange is null) return;
        //    context.PendigChanges.Remove(pendingChange);
        //    context.SaveChanges();
        //}
    }
}
