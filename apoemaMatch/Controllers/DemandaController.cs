using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    public class DemandaController : Controller
    {
        private readonly IDemandaService _service;

        public DemandaController(IDemandaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var todasDemandas = await _service.GetAllAsync();
            return View(todasDemandas);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var todasDemandas = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var resultadoFiltro = todasDemandas.Where(n => n.NomeEmpresa.Contains(searchString) || n.Descricao.Contains(searchString))
                    .ToList();
                return View("Index", resultadoFiltro);
            }

            return View("Index", todasDemandas);
        }

        //GET: Demanda/Detalhes/1

        public async Task<IActionResult> Detalhes(int Id)
        {
            var demandaDetalhes = await _service.GetDemandaByIdAsync(Id);
            return View(demandaDetalhes);
        }

        //GET: Demanda/Cadastrar
        public async Task<IActionResult> Cadastrar()
        {
            //var demandaDropDown = await _service.GetSolucionadoresDropDown();
            // ViewBag.SolucionadorId = new SelectList(demandaDropDown.Solucionadores, "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(DemandaViewModel novaDemanda)
        {
            if (!ModelState.IsValid)
            {
                novaDemanda.AreaSolucaoBuscada = novaDemanda.EnumAreaSolucaoBuscada;
                novaDemanda.LeiDeInformatica = novaDemanda.EnumLeiDeInformatica;
                novaDemanda.LinhaDeAtuacaoTI = novaDemanda.EnumLinhaDeAtuacaoTI;
                novaDemanda.ObjetivoParceria = novaDemanda.EnumObjetivoParceria;
                novaDemanda.PorteDaEmpresa = novaDemanda.EnumPorteDaEmpresa;
                novaDemanda.RamoDeAtuacao = novaDemanda.EnumRamoDeAtuacao;
                novaDemanda.SegmentoDeMercado = novaDemanda.EnumSegmentoDeMercado;
                novaDemanda.RegimeDeTributacao = novaDemanda.EnumTributacao;
                return View(novaDemanda);
            }

            novaDemanda.AreaSolucaoBuscada = novaDemanda.EnumAreaSolucaoBuscada;
            novaDemanda.LeiDeInformatica = novaDemanda.EnumLeiDeInformatica;
            novaDemanda.LinhaDeAtuacaoTI = novaDemanda.EnumLinhaDeAtuacaoTI;
            novaDemanda.ObjetivoParceria = novaDemanda.EnumObjetivoParceria;
            novaDemanda.PorteDaEmpresa = novaDemanda.EnumPorteDaEmpresa;
            novaDemanda.RamoDeAtuacao = novaDemanda.EnumRamoDeAtuacao;
            novaDemanda.SegmentoDeMercado = novaDemanda.EnumSegmentoDeMercado;
            novaDemanda.RegimeDeTributacao = novaDemanda.EnumTributacao;

            await _service.AdicionarDemandaAsync(novaDemanda);


            return RedirectToAction(nameof(Index));
        }

        //GET: Demanda/Editar
        public async Task<IActionResult> Editar(int id)
        {
            //var demandaDropDown = await _service.GetSolucionadoresDropDown();
            // ViewBag.SolucionadorId = new SelectList(demandaDropDown.Solucionadores, "Id", "Nome");
            var demanda = await _service.GetDemandaByIdAsync(id);
            if(demanda == null)
            {
                return View("NotFound");
            }

            var response = new DemandaViewModel()
            {
                Id = demanda.Id,
                DemandaAberta = true,
                ImagemURL = demanda.ImagemURL,
                Email = demanda.Email,
                NomeDemandante = demanda.NomeDemandante,
                Telefone = demanda.Telefone,
                NomeEmpresa = demanda.NomeEmpresa,
                CargoDemandante = demanda.CargoDemandante,
                TempoDeMercado = demanda.TempoDeMercado,
                PorteDaEmpresa = demanda.PorteDaEmpresa,
                RamoDeAtuacao = demanda.RamoDeAtuacao,
                SegmentoDeMercado = demanda.SegmentoDeMercado,
                LinhaDeAtuacaoTI = demanda.LinhaDeAtuacaoTI,
                RegimeDeTributacao = demanda.RegimeDeTributacao,
                LeiDeInformatica = demanda.LeiDeInformatica,
                ObjetivoParceria = demanda.ObjetivoParceria,
                AreaSolucaoBuscada = demanda.AreaSolucaoBuscada,
                Descricao = demanda.Descricao
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id,DemandaViewModel novaDemanda)
        {
            if(id != novaDemanda.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(novaDemanda);
            }

            await _service.UpdateDemandaAsync(novaDemanda);


            return RedirectToAction(nameof(Index));
        }

    }
}
