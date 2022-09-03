﻿using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(int demandanteId, EncomendaViewModel encomendaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(encomendaViewModel);
            }

            List<Criterio> questoes = null;
            if (encomendaViewModel.InputCriterios is not null)
            {
                questoes = JsonConvert.DeserializeObject<List<Criterio>>(encomendaViewModel.InputCriterios);
            }

            if (questoes is not null && questoes.Any())
            {
                encomendaViewModel.Criterios = new();
                encomendaViewModel.Criterios.AddRange(questoes);
            }

            await _service.AddAsync(encomendaViewModel.Converta());
            TempData["Sucesso"] = true;
            return RedirectToAction(nameof(Cadastrar));
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

            novaEncomenda.PossuiChamada = encomenda.PossuiChamada;
            novaEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
            novaEncomenda.Titulo = encomenda.Titulo;
            novaEncomenda.Descricao = encomenda.Descricao;
            novaEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
            //TODO(Chamada)
            //novaEncomenda.Questoes = encomenda.Questoes;
            //novaEncomenda.EncomendaAberta = false;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> EmAberto()
        {
            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();
            return View(encomendas.Select(e => e.Converta()));
        }

    }
}
