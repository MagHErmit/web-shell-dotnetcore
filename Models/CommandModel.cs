using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_shell_dotnetcore.Models
{
    public abstract class CommandModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Command { get; set; }

        public CommandModel()
        {
            Date = DateTime.Now;
        }
    }
}
