using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Models
{
    public class Event
    {
        public int Id { get; set; }

     
        public int ConnectionId { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        //status: true - active, false - outdated
        public bool Status { get; set; }

        //types: 0 - successfulBackup, 1 - error, 2- info
        public int Type { get; set; }
    }
}
