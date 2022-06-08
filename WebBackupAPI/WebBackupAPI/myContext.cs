using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebBackupAPI.Models;

namespace WebBackupAPI
{
    public class MyContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Config> Configs { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<Deamon> Deamons { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<myEvent> Events { get; set; }

        public DbSet<PendingChange> PendigChanges { get; set; }

        public DbSet<Source> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b1_slachtapetr_db2;user=slachtapetr;password=123456;SslMode=none");
        }
    }
}
