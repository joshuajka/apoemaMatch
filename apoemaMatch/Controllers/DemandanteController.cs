using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    [Authorize(Roles = PapeisUsuarios.Admin)]
    public class DemandanteController : Controller
    {
        private readonly IDemandanteService _service;

        public DemandanteController(IDemandanteService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var todosDemandantes = await _service.GetAllAsync();
            return View(todosDemandantes);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var todosDemandantes = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var resultadoFiltro = todosDemandantes.Where(n => n.NomeEmpresa.Contains(searchString) || n.Descricao.Contains(searchString))
                    .ToList();
                return View("Index", resultadoFiltro);
            }

            return View("Index", todosDemandantes);
        }

        //GET: Demandante/Detalhes/1
        [AllowAnonymous]
        public async Task<IActionResult> Detalhes(int Id)
        {
            var demandanteDetalhes = await _service.GetDemandanteByIdAsync(Id);
            return View(demandanteDetalhes);
        }

        //GET: Demandante/Cadastrar
        [Authorize(Roles = PapeisUsuarios.Demandante)]
        public async Task<IActionResult> Cadastrar()
        {
            //var demandanteDropDown = await _service.GetSolucionadoresDropDown();
            // ViewBag.SolucionadorId = new SelectList(demandanteDropDown.Solucionadores, "Id", "Nome");

            return View();
        }

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(DemandanteViewModel novoDemandante)
        {
            if (!ModelState.IsValid)
            {
                novoDemandante.AreaSolucaoBuscada = novoDemandante.EnumAreaSolucaoBuscada;
                novoDemandante.LeiDeInformatica = novoDemandante.EnumLeiDeInformatica;
                novoDemandante.LinhaDeAtuacaoTI = novoDemandante.EnumLinhaDeAtuacaoTI;
                novoDemandante.ObjetivoParceria = novoDemandante.EnumObjetivoParceria;
                novoDemandante.PorteDaEmpresa = novoDemandante.EnumPorteDaEmpresa;
                novoDemandante.RamoDeAtuacao = novoDemandante.EnumRamoDeAtuacao;
                novoDemandante.SegmentoDeMercado = novoDemandante.EnumSegmentoDeMercado;
                novoDemandante.RegimeDeTributacao = novoDemandante.EnumTributacao;
                return View(novoDemandante);
            }

            novoDemandante.AreaSolucaoBuscada = novoDemandante.EnumAreaSolucaoBuscada;
            novoDemandante.LeiDeInformatica = novoDemandante.EnumLeiDeInformatica;
            novoDemandante.LinhaDeAtuacaoTI = novoDemandante.EnumLinhaDeAtuacaoTI;
            novoDemandante.ObjetivoParceria = novoDemandante.EnumObjetivoParceria;
            novoDemandante.PorteDaEmpresa = novoDemandante.EnumPorteDaEmpresa;
            novoDemandante.RamoDeAtuacao = novoDemandante.EnumRamoDeAtuacao;
            novoDemandante.SegmentoDeMercado = novoDemandante.EnumSegmentoDeMercado;
            novoDemandante.RegimeDeTributacao = novoDemandante.EnumTributacao;

            await _service.AdicionarDemandanteAsync(novoDemandante);


            return RedirectToAction(nameof(Index));
        }

        //GET: Demandante/Editar/2
        [Authorize(Roles = PapeisUsuarios.Demandante)]
        public async Task<IActionResult> Editar(int id)
        {
            //var demandanteDropDown = await _service.GetSolucionadoresDropDown();
            // ViewBag.SolucionadorId = new SelectList(DemandanteDropDown.Solucionadores, "Id", "Nome");
            var demandante = await _service.GetDemandanteByIdAsync(id);
            if (demandante == null)
            {
                return View("NotFound");
            }

            var response = new DemandanteViewModel()
            {
                Id = demandante.Id,
                ImagemURL = demandante.ImagemURL,
                Email = demandante.Email,
                NomeDemandante = demandante.NomeDemandante,
                Telefone = demandante.Telefone,
                NomeEmpresa = demandante.NomeEmpresa,
                CargoDemandante = demandante.CargoDemandante,
                TempoDeMercado = demandante.TempoDeMercado,
                PorteDaEmpresa = demandante.PorteDaEmpresa,
                RamoDeAtuacao = demandante.RamoDeAtuacao,
                SegmentoDeMercado = demandante.SegmentoDeMercado,
                LinhaDeAtuacaoTI = demandante.LinhaDeAtuacaoTI,
                RegimeDeTributacao = demandante.RegimeDeTributacao,
                LeiDeInformatica = demandante.LeiDeInformatica,
                ObjetivoParceria = demandante.ObjetivoParceria,
                AreaSolucaoBuscada = demandante.AreaSolucaoBuscada,
                Descricao = demandante.Descricao
            };

            return View(response);
        }

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        [HttpPost]
        public async Task<IActionResult> Editar(int id, DemandanteViewModel novoDemandante)
        {
            if (id != novoDemandante.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(novoDemandante);
            }

            await _service.UpdateDemandanteAsync(novoDemandante);


            return RedirectToAction(nameof(Index));
        }

        //GET: Demandante/VincularSolucionador/3
        /*public async Task<IActionResult> VincularSolucionador(int id)
        {
            var demandante = await _service.GetDemandanteByIdAsync(id);
            if (demandante == null)
            {
                return View("NotFound");
            }

            var demandanteDropDown = await _service.GetSolucionadoresDropDown(demandante);

            ViewBag.Solucinadores = new SelectList(demandanteDropDown.Solucionadores, "Id", "Nome");

            return View();
        }*/


        /*[HttpPost]
        public async Task<IActionResult> VincularSolucionador(int id, DemandanteViewModel novoDemandante)
        {
            if (id != novoDemandante.Id)
            {
                return View("NotFound");
            }

            var demandante = await _service.GetDemandanteByIdAsync(id);
            novoDemandante.ImagemURL = demandante.ImagemURL;
            novoDemandante.Email = demandante.Email;
            novoDemandante.NomeDemandante = demandante.NomeDemandante;
            novoDemandante.Telefone = demandante.Telefone;
            novoDemandante.NomeEmpresa = demandante.NomeEmpresa;
            novoDemandante.CargoDemandante = demandante.CargoDemandante;
            novoDemandante.TempoDeMercado = demandante.TempoDeMercado;
            novoDemandante.PorteDaEmpresa = demandante.PorteDaEmpresa;
            novoDemandante.RamoDeAtuacao = demandante.RamoDeAtuacao;
            novoDemandante.SegmentoDeMercado = demandante.SegmentoDeMercado;
            novoDemandante.LinhaDeAtuacaoTI = demandante.LinhaDeAtuacaoTI;
            novoDemandante.RegimeDeTributacao = demandante.RegimeDeTributacao;
            novoDemandante.LeiDeInformatica = demandante.LeiDeInformatica;
            novoDemandante.ObjetivoParceria = demandante.ObjetivoParceria;
            novoDemandante.AreaSolucaoBuscada = demandante.AreaSolucaoBuscada;
            novoDemandante.Descricao = demandante.Descricao;

            //if (!ModelState.IsValid)
            //{
            //    return View(novoDemandante);
            //}


            await _service.VincularDemandanteAsync(novoDemandante);


            return RedirectToAction(nameof(Index));
        }*/

        public async Task<IActionResult> Excluir(int id)
        {
            var demandanteDetalhes = await _service.GetDemandanteByIdAsync(id);

            if (demandanteDetalhes == null)
            {
                return View("NotFound");
            }

            return View(demandanteDetalhes);
        }

        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ExcluirConfirmed(int id)
        {
            var demandanteDetalhes = await _service.GetDemandanteByIdAsync(id);
            if (!ModelState.IsValid)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
