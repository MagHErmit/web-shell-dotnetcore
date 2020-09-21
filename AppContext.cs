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

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
