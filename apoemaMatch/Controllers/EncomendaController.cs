using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> VincularSolucionador(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);
            
            if (encomenda == null)
            {
                return View("NotFound");
            }

            var encomendaDropDown = await _service.GetSolucionadoresDropDown(encomenda);

            ViewBag.Solucinadores = new SelectList(encomendaDropDown.Solucionadores, "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VincularSolucionador(int id, EncomendaViewModel novaEncomenda)
        {
            if (id != novaEncomenda.Id)
            {
                return View("NotFound");
            }

            var encomenda = await _service.GetByIdAsync(id);

            novaEncomenda.RealizaProcessoSeletivo = encomenda.RealizaProcessoSeletivo;
            novaEncomenda.SegmentoDeMercado = encomenda.SegmentoDeMercado;
            novaEncomenda.Titulo = encomenda.Titulo;
            novaEncomenda.AreaSolucaoBuscada = encomenda.AreaSolucaoBuscada;
            novaEncomenda.Descricao = encomenda.Descricao;
            novaEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
            novaEncomenda.Questoes = encomenda.Questoes;
            novaEncomenda.EncomendaAberta = false;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }

    }
}
