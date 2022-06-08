using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbSources")]
    public class Source
    {
        public int Id { get; set; }

        [Column("Config_ID")]
        public int ConfigId { get; set; }

        [Column("Source")]
        public string SourcePath { get; set; }
    }
}