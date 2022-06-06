using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Models
{
    public class Connection
    {
        public int Id { get; set; }

       
        public int DeamonId { get; set; }

      
        public int ConfigId { get; set; }

        public bool Connected { get; set; }

        public virtual List<Event> Events { get; set; }
    }
}
