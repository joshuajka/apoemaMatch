using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();
            return View(encomendas.Select(e => e.Converta()));
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Cadastrar(EncomendaViewModel encomendaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(encomendaViewModel);
            }

            await _service.AddAsync(encomendaViewModel.Converta());
            return RedirectToAction(nameof(Index));
        }

        public IActionResult FormularioAvaliacao()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int Id)
        {
            Encomenda encomenda = await _service.GetByIdAsync(Id);
            return View(encomenda.Converta());
        }
    }
}
