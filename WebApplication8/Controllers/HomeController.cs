using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("run")]
        public IActionResult run()
        {
            var startinfo = new ProcessStartInfo();
            startinfo.FileName = "ConsoleApp1";
            startinfo.Arguments = "tst tst";
            //startinfo.RedirectStandardInput = true;
            try
            {
                using (var process = Process.Start(startinfo))
                {
                    Console.WriteLine("Something output from here!!");
                    process.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter exception");
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }
    }
}
