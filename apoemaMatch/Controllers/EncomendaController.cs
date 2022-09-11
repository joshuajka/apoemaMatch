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

        public EncomendaController(IEncomendaService service, UserManager<ApplicationUser> userManager, ISolucionadorService serviceSolucionador,
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
            
            foreach (var item in encomendas)
            {
                if (item.PossuiChamada == true && item.StatusEncomenda == EnumStatusEncomenda.Aberta && await _service.CheckDateExpiration(item.Id))
                {
                    item.StatusEncomenda = EnumStatusEncomenda.AguardandoAnaliseChamada;
                    await _service.AtualizaEncomendaAsync(item);
                }
            }
            
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

            foreach (var item in encomendasDemandante)
            {
                if (item.PossuiChamada == true)
                {
                    var propostas = await _service.GetPropostasByEncomenda(item.Id);
                    ViewData[item.Id.ToString()] = propostas.Count();
                    
                    if (item.StatusEncomenda == EnumStatusEncomenda.Aberta && await _service.CheckDateExpiration(item.Id))
                    {
                        item.StatusEncomenda = EnumStatusEncomenda.AguardandoAnaliseChamada;
                        await _service.AtualizaEncomendaAsync(item);
                    }
                }
            }
            
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
                var userDemandante = await _userManager.FindByEmailAsync(userEmail);
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
        
        [Authorize(Roles = PapeisUsuarios.Demandante + "," + PapeisUsuarios.Admin)]
        [HttpGet]
        public async Task<IActionResult> Excluir(int Id)
        {
            await _service.DeleteAsync(Id);

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Encomenda");
            }
            return RedirectToAction("MeuPerfilDemandante", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int Id)
        {
            //Encomenda encomenda = await _service.GetByIdAsync(Id);
            
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var usersolucionador = await _serviceSolucionador.GetSolucionadorByIdUser(user.Id);

            var encomenda = await _service.GetEncomendaAsync(new Encomenda { Id = Id });

            if (encomenda.IdSolucionador != null)
            {
                var solucionador = await _serviceSolucionador.GetByIdAsync((int)encomenda.IdSolucionador);
                ViewData["NomeSolucionador"] = solucionador.Nome;
                ViewData["SolucionadorEmail"] = solucionador.Email;
                ViewData["SolucionadorTelefone"] = solucionador.Telefone;
                ViewData["SolucionadorLattes"] = solucionador.CurriculoLattes;
            }
            else
            {
                ViewData["NomeSolucionador"] = "Não possui";
            }
            var demandante = await _serviceDemandante.GetByIdAsync((int)encomenda.IdDemandante);
            ViewData["EmpresaDemandante"] = demandante.NomeEmpresa;

            if (encomenda.PossuiChamada == true)
            {
                ViewData["DescricaoChamada"] = encomenda.Chamada.DescricaoChamada;
                ViewData["DataCriacao"] = encomenda.DataCadastro.ToString("dd/MM/yyyy");
                ViewData["DataValidade"] = encomenda.Chamada.DataValidade.ToString("dd/MM/yyyy");
                ViewData["Anexo"] = encomenda.Chamada.ArquivoAnexo;
            }

            if (User.Identity.IsAuthenticated && User.IsInRole("Demandante") && encomenda.StatusEncomenda == EnumStatusEncomenda.Finalizada)
            {
                ViewData["AlertEncomendaFinalizada"] = 1;
            }else if (User.Identity.IsAuthenticated && User.IsInRole("Solucionador") && encomenda.StatusEncomenda == EnumStatusEncomenda.Finalizada && encomenda.IdSolucionador != null && encomenda.IdSolucionador == usersolucionador.Id)
            {
                ViewData["AlertEncomendaFinalizada"] = 2;
            }

            if (encomenda.StatusEncomenda == EnumStatusEncomenda.Recusada)
            {
                ViewData["Justificativa"] = encomenda.JustificativaRecusa;
            }

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
            
            foreach (var item in encomendas)
            {
                if (item.PossuiChamada == true && item.StatusEncomenda == EnumStatusEncomenda.Aberta && await _service.CheckDateExpiration(item.Id))
                {
                    item.StatusEncomenda = EnumStatusEncomenda.AguardandoAnaliseChamada;
                    await _service.AtualizaEncomendaAsync(item);
                }
            }
            
            List<EncomendaViewModel> encomendasViewModel = encomendas.ConvertAll(e => e.Converta());

            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            foreach (var encomendaViewModel in encomendasViewModel)
            {
                encomendaViewModel.SolucionadorLogadoPossuiPropostaNaEncomenda =
                    encomendaViewModel.Propostas is not null && encomendaViewModel.Propostas.Any(p => p.SolucionadorId == solucionador.Id);

                if (encomendaViewModel.SolucionadorLogadoPossuiPropostaNaEncomenda == true)
                {
                    encomendaViewModel.Proposta =
                        encomendaViewModel.Propostas.First(p => p.SolucionadorId == solucionador.Id);
                }
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
            encomenda.StatusEncomenda = EnumStatusEncomenda.Inicial;

            await _service.AceitarRecusarEncomendaAsync(encomenda);

            return RedirectToAction(nameof(MinhasEncomendasSolucionador));
        }

        public async Task<IActionResult> Aceitar(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            encomenda.EncomendaAberta = false;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Finalizada;

            await _service.AceitarRecusarEncomendaAsync(encomenda);

            return RedirectToAction(nameof(MinhasEncomendasSolucionador));
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
            List<Proposta> propostas_ordenadas = encomenda.Chamada.Propostas.OrderByDescending(o => o.Pontuacao).ToList();
            encomenda.IdSolucionador = propostas_ordenadas[0].Solucionador.Id;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Finalizada;
            await _service.AtualizaEncomendaAsync(encomenda);
            return RedirectToAction(nameof(Detalhes), new { id = encomenda.Id });
        }

        [Authorize(Roles = PapeisUsuarios.Admin)]
        [HttpPost]
        public async Task<IActionResult> RecusarEncomenda(int Id, EncomendaViewModel encomendaViewModel)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            //encomenda.IdSolucionador = null;
            //encomenda.EncomendaAberta = true;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Recusada;
            encomenda.JustificativaRecusa = encomendaViewModel.JustificativaRecusa;

            await _service.AtualizaEncomendaAsync(encomenda);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AceitarEncomenda(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            //encomenda.EncomendaAberta = false;
            encomenda.StatusEncomenda = EnumStatusEncomenda.Aberta;

            await _service.AtualizaEncomendaAsync(encomenda);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HabilitarStatusAguardandoAnaliseChamada(int Id)
        {
            var encomenda = await _service.GetByIdAsync(Id);

            if (encomenda == null)
            {
                return View("NotFound");
            }

            //encomenda.EncomendaAberta = false;
            encomenda.StatusEncomenda = EnumStatusEncomenda.AguardandoAnaliseChamada;

            await _service.AtualizaEncomendaAsync(encomenda);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExcluirProposta(int Id)
        {
            await _service.ExcluirProposta(Id);

            return RedirectToAction(nameof(EmAberto));
        }
        
        public async Task<IActionResult> ExcluirPropostaTelaMinhasPropostas(int Id)
        {
            await _service.ExcluirProposta(Id);

            return RedirectToAction(nameof(ChamadasComMinhasPropostas));
        }
        
        [Authorize(Roles = PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> ChamadasComMinhasPropostas()
        {
            List<Encomenda> encomendas = await _service.GetAllEncomendasAsync();
            
            foreach (var item in encomendas)
            {
                if (item.PossuiChamada == true && item.StatusEncomenda == EnumStatusEncomenda.Aberta && await _service.CheckDateExpiration(item.Id))
                {
                    item.StatusEncomenda = EnumStatusEncomenda.AguardandoAnaliseChamada;
                    await _service.AtualizaEncomendaAsync(item);
                }
            }
            
            List<EncomendaViewModel> encomendasViewModel = encomendas.ConvertAll(e => e.Converta());

            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            encomendasViewModel = encomendasViewModel.Where(e =>
                e.Propostas is not null && e.Propostas.Any(p => p.SolucionadorId == solucionador.Id)).ToList();
            
            foreach (var encomendaViewModel in encomendasViewModel)
            {
                encomendaViewModel.Proposta =
                        encomendaViewModel.Propostas.First(p => p.SolucionadorId == solucionador.Id);
            }

            return View(encomendasViewModel);
        }
    }
}
