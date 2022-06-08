using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbConnections")]
    public class Connection
    {
        public int Id { get; set; }

        [Column("Deamon_ID")]
        public int DeamonId { get; set; }

        [Column("Config_ID")]
        public int ConfigId { get; set; }

        public bool Connected { get; set; }

        public virtual List<myEvent> Events { get; set; }
    }
}
