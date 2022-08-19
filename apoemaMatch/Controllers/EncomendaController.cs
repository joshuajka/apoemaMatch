using apoemaMatch.Data.Services;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace apoemaMatch.Controllers
{
    public class EncomendaController : Controller
    {
        private readonly IEncomendaService _service;

        public EncomendaController(IEncomendaService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar(Encomenda encomenda)
        {
            if (!ModelState.IsValid)
            {
                return View(encomenda);
            }

            _service.AddAsync(encomenda);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult FormularioAvaliacao()
        {
            return View();
        }
    }
}
