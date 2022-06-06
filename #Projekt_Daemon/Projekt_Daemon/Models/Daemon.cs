using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Models
{
    public class Daemon
    {
        public int Id { get; set; }

        //[Column("Nejm")]
        public string Alias { get; set; }

        public string IP_Address { get; set; }

        public int Status { get; set; }

        public string Password { get; set; }
    }
}
