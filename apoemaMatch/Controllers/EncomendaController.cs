using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace apoemaMatch.Controllers
{
    public class EncomendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        
        public IActionResult FormularioAvaliacao()
        {
            return View();
        }
    }
}
