using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apoemaMatch.Data.Services;
using Microsoft.AspNetCore.Mvc;

using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apoemaMatch.Data.ViewModels;

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
            var todasEncomendas = await _service.GetAllAsync();
            return View(todasEncomendas);
        }
        
        public async Task<IActionResult> Filter(string searchString)
        {
            var todasEncomendas = await _service.GetAllAsync();

            if (string.IsNullOrEmpty(searchString)) return View("Index", todasEncomendas);
           
            var resultadoFiltro = todasEncomendas.Where(n => n.Titulo.Contains(searchString) || n.Descricao.Contains(searchString))
                .ToList();
            return View("Index", resultadoFiltro);

        }
        
        [HttpPost]
        public async Task<IActionResult> Cadastrar(EncomendaViewModel novaEncomenda)
        {
            await _service.AdicionarEncomendaAsync(novaEncomenda);
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Editar(int id)
        {
            
            var encomenda = await _service.GetEncomendaByIdAsync(id);
            if(encomenda == null) return View("NotFound");
            
            var response = new EncomendaViewModel()
            {
                Id = encomenda.Id,
                Titulo = encomenda.Titulo,
                SegmentoDeMercado = encomenda.SegmentoDeMercado,
                AreaSolucaoBuscada = encomenda.AreaSolucaoBuscada,
                Descricao = encomenda.Descricao,
                RealizaProcessoSeletivo = encomenda.RealizaProcessoSeletivo,
            };

            return View(response);
        }
        
        public async Task<IActionResult> Excluir(int id)
        {
            var encomenda = await _service.GetEncomendaByIdAsync(id);

            if (encomenda == null) return View("NotFound");
            
            await _service.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
