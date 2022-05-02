using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon.Models
{
    public class PendingChange
    {
        public int Id { get; set; }

        public int DeamonId { get; set; }

        public int ConfigId { get; set; }

        // 0 - config changed, 1 - connection added, 2 - config deleted / connection removed
        public int ChangeType { get; set; }
    }
}
