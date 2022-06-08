using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbConfigs")]
    public class Config
    {
        public int Id { get; set; }

        public string Alias { get; set; }

        //1 - full, 2 - differential, 3 - incremental
        [Column("Backup_Type")]
        public int BackupType { get; set; }

        [Column("Package_size")]
        public int PackageSize { get; set; }

        [Column("Package_count")]
        public int PackageCount { get; set; }

        public string TimePeriod { get; set; }

        public string Email { get; set; }

        public string MailTimeCron { get; set; }

        public virtual List<Source> Sources { get; set; }

        public virtual List<Destination> Destinations { get; set; }

        public bool? Active { get; set; }
    }
}