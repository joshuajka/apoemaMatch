using apoemaMatch.Data.Services;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace apoemaMatch.Controllers
{
    public class EncomendaController : Controller
    {
        private readonly IEncomendaService _service;

        public EncomendaController(IEncomendaService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(EncomendaViewModel encomendaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(encomendaViewModel);
            }

            Encomenda encomenda = new()
            {
                Id = encomendaViewModel.Id,
                Titulo = encomendaViewModel.Titulo,
                SegmentoDeMercado = encomendaViewModel.SegmentoDeMercado,
                AreaSolucaoBuscada = encomendaViewModel.AreaSolucaoBuscada,
                Descricao = encomendaViewModel.Descricao,
                RealizaProcessoSeletivo = encomendaViewModel.RealizaProcessoSeletivo,
                Questoes = encomendaViewModel.Questoes?.ConvertAll(q => new Questao()
                {
                    Id = q.Id,
                    Pergunta = q.Pergunta,
                    TipoResposta = q.TipoResposta
                })
            };

            _service.AddAsync(encomenda);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult FormularioAvaliacao()
        {
            return View();
        }
    }
}
