using apoemaMatch.Data;
using apoemaMatch.Data.Services;
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

        //GET: Demanda/Detalhes/1

        public async Task<IActionResult> Detalhes(int Id)
        {
            var demandaDetalhes = await _service.GetDemandaByIdAsync(Id);
            return View(demandaDetalhes);
        }

        //GET: Demanda/Cadastrar
        public IActionResult Cadastrar()
        {
            //var demandaDropDown = await _service.GetSolucionadoresDropDown();
            // ViewBag.SolucionadorId = new SelectList(demandaDropDown.Solucionadores, "Id", "Nome");

            return View();
        }

    }
}
