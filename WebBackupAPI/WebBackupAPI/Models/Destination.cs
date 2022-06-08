using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbDestinations")]
    public class Destination
    {
        public int Id { get; set; }

        [Column("Config_ID")]
        public int ConfigId { get; set; }

        [Column("Save_Type")]
        public int SaveType { get; set; }

        public string IP_Address { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Path { get; set; }

        [Column("Save_File")]
        public string SaveFile { get; set; }
    }
}
