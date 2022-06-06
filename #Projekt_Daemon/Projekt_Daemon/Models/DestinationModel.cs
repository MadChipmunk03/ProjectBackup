using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon
{
    public class DestinationModel
    {
        public int Id { get; set; }

      
        public int ConfigId { get; set; }

     
        public int SaveType { get; set; }

        public string IP_Address { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Path { get; set; }

       
        public string SaveFile { get; set; }
    }
}
