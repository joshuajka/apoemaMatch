using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> Cadastrar(EncomendaViewModel encomendaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), encomendaViewModel);
            }

            await _service.AddAsync(encomendaViewModel.Converta());
            return RedirectToAction(nameof(Index));
        }

        public IActionResult FormularioAvaliacao()
        {
            return View();
        }
    }
}
