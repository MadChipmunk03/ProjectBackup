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
    public class SourcesController : ControllerBase
    {
        private MyContext context = new MyContext();

        // GET: api/<SourcesController>
        [HttpGet]
        public List<Source> Get()
        {
            return context.Sources.ToList();
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id}")]
        public List<Source> Get(int id)
        {
            return context.Sources.Where(x => x.ConfigId == id).ToList();
        }

        //// GET api/<SourcesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ValuesController>
        [HttpPost]
        public Source Post(Source source)
        {
            context.Sources.Add(source);
            context.SaveChanges();
            return source;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Update(int id, Source newSrc)
        {
            Source dbSrc = context.Sources.Find(id);
            dbSrc.SourcePath = newSrc.SourcePath;

            context.SaveChanges();
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Source source = context.Sources.Find(id);
            if (source is null) return;
            context.Sources.Remove(source);
            context.SaveChanges();
        }
    }
}
