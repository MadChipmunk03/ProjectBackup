using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbPendingChanges")]
    public class PendingChange
    {
        public int Id { get; set; }

        public int DeamonId { get; set; }

        public int ConfigId { get; set; }

        // 0 - config changed, 1 - connection added, 2 - config deleted / connection removed
        public int ChangeType { get; set; }
    }
}
