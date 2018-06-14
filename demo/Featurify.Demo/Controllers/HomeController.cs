using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Featurify.Demo.Models;
using Featurify.Contracts;
using Featurify.Demo.Features;

namespace Featurify.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeaturifyServer server;

        public HomeController(IFeaturifyServer server)
        {
            this.server = server;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            ViewData["Message"] = "Your contact page.";
            var model = new ContactViewModel
            {
                CanImport = await server.Enabled<ImportFeature>()
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
