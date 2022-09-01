using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    public class EncomendaController : Controller
    {
        private readonly IEncomendaService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISolucionadorService _serviceSolucionador;
        private readonly IDemandanteService _serviceDemandante;

        public EncomendaController(IEncomendaService service, UserManager<ApplicationUser> userManager,ISolucionadorService serviceSolucionador,
            IDemandanteService serviceDemandante)
        {
            _service = service;
            _userManager = userManager;
            _serviceSolucionador = serviceSolucionador;
            _serviceDemandante = serviceDemandante;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();
            return View(encomendas.Select(e => e.Converta()));
        }

        public async Task<IActionResult> MinhasEncomendasSolucionador()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();

            var encomendasSolucionador = encomendas.Where(n => n.IdSolucionador == solucionador.Id);

            return View(encomendasSolucionador.Select(e => e.Converta()));
        }

        public async Task<IActionResult> MinhasEncomendasDemandante()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userDemandante = await _userManager.FindByEmailAsync(userEmail);
            var demamandante = await _serviceDemandante.GetDemandanteByIdUser(userDemandante.Id);

            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();

            var encomendasDemandante = encomendas.Where(n => n.IdDemandante == demamandante.Id);

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
            
            if (User.IsInRole("Admin"))
            {
                encomendaViewModel.IdDemandante = 1;
            }
            else
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);
                var userDemandante= await _userManager.FindByEmailAsync(userEmail);
                var demandante = await _serviceSolucionador.GetSolucionadorByIdUser(userDemandante.Id);
                encomendaViewModel.IdDemandante = demandante.Id;
            }

            encomendaViewModel.EncomendaAberta = true;
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

            if(encomenda.IdSolucionador != null)
            {
                var solucionador = await _serviceSolucionador.GetByIdAsync((int)encomenda.IdSolucionador);
                ViewData["NomeSolucionador"] = solucionador.Nome;
            }
            else
            {
                ViewData["NomeSolucionador"] = "Não possui";
            }
            var demandante = await _serviceDemandante.GetByIdAsync((int)encomenda.IdDemandante);
            ViewData["EmpresaDemandante"] = demandante.NomeEmpresa;



            return View(encomenda.Converta());
        }

        public async Task<IActionResult> VincularSolucionador(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);
            
            if (encomenda == null)
            {
                return View("NotFound");
            }

            if (encomenda.IdSolucionador != null)
            {
                var solucionador = await _serviceSolucionador.GetByIdAsync((int)encomenda.IdSolucionador);
                ViewData["NomeSolucionador"] = solucionador.Nome;
                ViewData["IdEncomenda"] = Id;

                return View("SolucionadorVinculado");
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
            novaEncomenda.IdDemandante = encomenda.IdDemandante;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AlterarSolucionador(int Id)
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
        public async Task<IActionResult> AlterarSolucionador(int id, EncomendaViewModel novaEncomenda)
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
            novaEncomenda.IdDemandante = encomenda.IdDemandante;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }

    }
}
