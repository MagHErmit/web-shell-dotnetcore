using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_shell_dotnetcore.Models;

namespace web_shell_dotnetcore
{
    public class AppContext : DbContext
    {
        public DbSet<PowerShellCM> PSCommands { get; set; }

        public AppContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=shellshistorydb;Trusted_Connection=True;");
        }
    }
}
