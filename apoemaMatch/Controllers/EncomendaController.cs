using apoemaMatch.Data.MetodosExtensao;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using apoemaMatch.Data.Enums;

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

        [Authorize(Roles = PapeisUsuarios.Admin)]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();
            return View(encomendas.Select(e => e.Converta()));
        }

        [Authorize(Roles = PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> MinhasEncomendasSolucionador()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();

            var encomendasSolucionador = encomendas.Where(n => n.IdSolucionador == solucionador.Id);

            return View(encomendasSolucionador.Select(e => e.Converta()));
        }

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        public async Task<IActionResult> MinhasEncomendasDemandante()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userDemandante = await _userManager.FindByEmailAsync(userEmail);
            var demamandante = await _serviceDemandante.GetDemandanteByIdUser(userDemandante.Id);

            IEnumerable<Encomenda> encomendas = await _service.GetAllAsync();

            var encomendasDemandante = encomendas.Where(n => n.IdDemandante == demamandante.Id);

            return View(encomendasDemandante.Select(e => e.Converta()));
        }

        [HttpGet]
        [Authorize(Roles = PapeisUsuarios.Demandante)]
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
            
            if (User.IsInRole("Admin"))
            {
                encomendaViewModel.IdDemandante = 1;
            }
            else
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);
                var userDemandante= await _userManager.FindByEmailAsync(userEmail);
                var demandante = await _serviceDemandante.GetDemandanteByIdUser(userDemandante.Id);
                encomendaViewModel.IdDemandante = demandante.Id;
            }

            List<Criterio> criterios = null;
            if (encomendaViewModel.InputCriterios is not null)
            {
                criterios = JsonConvert.DeserializeObject<List<Criterio>>(encomendaViewModel.InputCriterios);
            }

            if (criterios is not null && criterios.Any())
            {
                encomendaViewModel.Criterios = new();
                encomendaViewModel.Criterios.AddRange(criterios);
            }

            encomendaViewModel.EncomendaAberta = true;

            Encomenda encomenda = encomendaViewModel.Converta();
            encomenda.DataCadastro = DateTime.Now;

            await _service.AddAsync(encomenda);
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
            //Encomenda encomenda = await _service.GetByIdAsync(Id);

            var encomenda = await _service.GetEncomendaAsync(new Encomenda { Id = Id });

            if (encomenda.IdSolucionador != null)
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

            novaEncomenda.PossuiChamada = encomenda.PossuiChamada;
            novaEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
            novaEncomenda.Titulo = encomenda.Titulo;
            novaEncomenda.Descricao = encomenda.Descricao;
            novaEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
            //TODO(Chamada)
            //novaEncomenda.Questoes = encomenda.Questoes;
            //novaEncomenda.EncomendaAberta = false;
            //novaEncomenda.ChamadaId = encomenda.Chamada.Id;
            novaEncomenda.EncomendaAberta = true;
            novaEncomenda.IdDemandante = encomenda.IdDemandante;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> EmAberto()
        {
            List<Encomenda> encomendas = await _service.GetAllEncomendasAsync();
            List<EncomendaViewModel> encomendasViewModel = encomendas.ConvertAll(e => e.Converta());

            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            foreach (var encomendaViewModel in encomendasViewModel)
            {
                encomendaViewModel.SolucionadorLogadoPossuiPropostaNaEncomenda =
                    encomendaViewModel.Propostas is not null && encomendaViewModel.Propostas.Any(p => p.SolucionadorId == solucionador.Id); 
            }

            return View(encomendasViewModel);
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

            novaEncomenda.PossuiChamada = encomenda.PossuiChamada;
            novaEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
            novaEncomenda.Titulo = encomenda.Titulo;
            novaEncomenda.Descricao = encomenda.Descricao;
            novaEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
            //novaEncomenda.ChamadaId = encomenda.Chamada.Id;
            //novaEncomenda.EncomendaAberta = false;
            novaEncomenda.IdDemandante = encomenda.IdDemandante;


            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}


            await _service.VincularEncomendaAsync(novaEncomenda);


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Recusar(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            encomenda.IdSolucionador = null;
            encomenda.EncomendaAberta = true;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Recusada;

            await _service.AceitarRecusarEncomendaAsync(encomenda);

            //return RedirectToAction(nameof(MinhasEncomendasSolucionador));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Aceitar(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            encomenda.EncomendaAberta = false;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Aberta;

            await _service.AceitarRecusarEncomendaAsync(encomenda);

            //return RedirectToAction(nameof(MinhasEncomendasSolucionador));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CadastrarProposta(int Id)
        {
            Encomenda encomenda = await _service.GetEncomendaAsync(new() { Id = Id });

            return View(encomenda.Converta());
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProposta(EncomendaViewModel encomendaViewModel)
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            encomendaViewModel.Proposta.SolucionadorId = solucionador.Id;
            encomendaViewModel.Proposta.ChamadaId = encomendaViewModel.ChamadaId;
            await _service.InsereProposta(encomendaViewModel.Proposta);

            return RedirectToAction(nameof(EmAberto));
        }

        [HttpGet]
        public async Task<IActionResult> VisualizarPropostas(int Id)
        {
            Encomenda encomenda = await _service.GetEncomendaAsync(new Encomenda { Id = Id });
            var encomendaViewModel = encomenda.Converta();
            encomendaViewModel.Proposta = encomenda.Chamada.Propostas.FirstOrDefault();

            return View(encomendaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AnalisarProposta(int Id)
        {
            Encomenda encomenda = await _service.GetEncomendaByProposta(new Proposta { Id = Id });
            var encomendaViewModel = encomenda.Converta();
            encomendaViewModel.Proposta = encomenda.Chamada.Propostas.FirstOrDefault();

            return View(encomendaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarNotasProposta(EncomendaViewModel encomendaViewModel)
        {
            await _service.UpdateNotasRespostasCriteriosProposta(encomendaViewModel.Proposta);
            var encomenda = await _service.GetEncomendaByChamada(encomendaViewModel.ChamadaId);
            return RedirectToAction(nameof(VisualizarPropostas), new { id = encomenda.Id });
        }

        //TODO
        [HttpPost]
        public async Task<IActionResult> FinalizeProcessoSeletivo(int Id)
        {
            Encomenda encomenda = await _service.GetEncomendaAsync(new Encomenda { Id = Id });
            List<Proposta> propostas_ordenadas = encomenda.Chamada.Propostas.OrderByDescending(o=>o.Pontuacao).ToList();
            encomenda.IdSolucionador = propostas_ordenadas[0].Solucionador.Id;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Finalizada;
            await _service.AtualizaEncomendaAsync(encomenda);
            return RedirectToAction(nameof(Detalhes), new { id = encomenda.Id });
        }
    }
}
