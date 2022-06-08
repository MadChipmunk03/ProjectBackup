using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Models
{
    [Table("tbDeamons")]
    public class Deamon
    {
        public int Id { get; set; }

        //[Column("Nejm")]
        public string Alias { get; set; }

        public string IP_Address { get; set; }

        //0 - unsynced, 1 - synced, 2 - disabeled while unsynced, 3 - disabeled while synced
        public int Status { get; set; }

        public string Password { get; set; }
    }
}
