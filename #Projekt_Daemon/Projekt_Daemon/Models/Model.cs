using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Daemon
{
   public class Model
    {
        public int Id { get; set; }

        public string Alias { get; set; }

        //1 - full, 2 - differential, 3 - incremental
      
        public int BackupType { get; set; }

       
        public int PackageSize { get; set; }

       
        public int PackageCount { get; set; }

        public string TimePeriod { get; set; }

        public string Email { get; set; }

        public virtual List<SourceModel> Sources { get; set; }

        public virtual List<DestinationModel> Destinations { get; set; }

        public bool? Active { get; set; }
    }
}
