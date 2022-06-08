using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbEvents")]
    public class myEvent
    {
        public int Id { get; set; }

        [Column("Connection_ID")]
        public int ConnectionId { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        //status: true - active, false - outdated
        public bool Status { get; set; }

        //types: 0 - successfulBackup, 1 - error, 2- info
        public int Type { get; set; }

    }
}
