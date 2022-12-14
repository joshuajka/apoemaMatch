using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "joshuajka@gmail.com" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Joshua")
            //    }
            //};

            //await _emailService.SendTestEmail(options);

            return View();
        }

        public IActionResult Logado()
        {
            if (User.IsInRole("Demandante"))
            {
                return RedirectToAction("MinhasEncomendasDemandante", "Encomenda");
            }
            else if (User.IsInRole("Solucionador"))
            {
                return RedirectToAction("MinhasEncomendasSolucionador", "Encomenda");
            }
            else
            {
                return RedirectToAction("Index", "Encomenda");
            }
        }

        public async Task<IActionResult> Privacy()
        {

            return View();
        }

        public async Task<IActionResult> Sobre()
        {

            return View();
        }

    }
}
