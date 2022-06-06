using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Models
{
    public class Files
    {
        public int id { get; set; }
        public string Type { get; set; }
        public List<string> Rest { get; set; }
    }
}
