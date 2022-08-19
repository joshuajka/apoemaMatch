using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    [Authorize(Roles = PapeisUsuarios.Admin)]
    public class SolucionadorController : Controller
    {
        private readonly ISolucionadorService _service;

        public SolucionadorController(ISolucionadorService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Solucionador/Create
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Solucionador solucionador)
        {
            if (!ModelState.IsValid)
            {
                return View(solucionador);
            }
            await _service.AddAsync(solucionador);
            return RedirectToAction(nameof(Index));

        }

        //Get: Solucionador/Detalhes/1
        [AllowAnonymous]
        public async Task<IActionResult> Detalhes(int id)
        {
            var solucionadorDetalhes = await _service.GetByIdAsync(id);

            if(solucionadorDetalhes == null)
            {
                return View("NotFound");
            }

            return View(solucionadorDetalhes);
        }

        //Get: Solucionador/Edit/1
        public async Task<IActionResult> Editar(int id)
        {
            var solucionadorDetalhes = await _service.GetByIdAsync(id);

            if (solucionadorDetalhes == null)
            {
                return View("NotFound");
            }

            return View(solucionadorDetalhes);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id,[Bind("Id,ImagemURL,Email,Nome,Telefone,Formacao,AreaDePesquisa,CurriculoLattes,MiniBio")] Solucionador solucionador)
        {
            if (!ModelState.IsValid)
            {
                return View(solucionador);
            }
            await _service.UpdateAsync(id,solucionador);
            return RedirectToAction(nameof(Index));
        }

        //Get: Solucionador/Excluir/1
        public async Task<IActionResult> Excluir(int id)
        {
            var solucionadorDetalhes = await _service.GetByIdAsync(id);

            if (solucionadorDetalhes == null)
            {
                return View("NotFound");
            }

            return View(solucionadorDetalhes);
        }

        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ExcluirConfirmed(int id)
        {
            var solucionadorDetalhes = await _service.GetByIdAsync(id);
            if (!ModelState.IsValid)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
