using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{

    public class SolucionadorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ISolucionadorService _service;

        public SolucionadorController(ISolucionadorService service,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Solucionador/Create
        //public IActionResult Cadastrar()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Cadastrar(Solucionador solucionador)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(solucionador);
        //    }
        //    await _service.AddAsync(solucionador);
        //    return RedirectToAction(nameof(Index));

        //}

        //Get: Solucionador/Detalhes/1
        [AllowAnonymous]
        public async Task<IActionResult> Detalhes(int id)
        {
            var solucionadorDetalhes = await _service.GetByIdAsync(id);

            if (solucionadorDetalhes == null)
            {
                return View("NotFound");
            }

            return View(solucionadorDetalhes);
        }

        //Get: Solucionador/Edit/1
        [Authorize(Roles = PapeisUsuarios.Admin + "," + PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> Editar(int id)
        {
            var solucionador = await _service.GetByIdAsync(id);

            if (solucionador == null)
            {
                return View("NotFound");
            }

            var response = new SolucionadorViewModel()
            {
                ImagemURL = solucionador.ImagemURL,
                Nome = solucionador.Nome,
                Email = solucionador.Email,
                Telefone = solucionador.Telefone,
                MiniBio = solucionador.MiniBio,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
            };

            return View(response);
        }

        [Authorize(Roles = PapeisUsuarios.Admin + "," + PapeisUsuarios.Solucionador)]
        [HttpPost]
        public async Task<IActionResult> Editar(int id, SolucionadorViewModel solucionador)
        {
            if (id != solucionador.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(solucionador);
            }

            var solucionadorAlterado = new SolucionadorViewModel()
            {
                ImagemURL = solucionador.ImagemURL,
                Nome = solucionador.Nome,
                Email = solucionador.Email,
                Telefone = solucionador.Telefone,
                MiniBio = solucionador.MiniBio,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
        };

            await _service.UpdateSolucionadorAsync(solucionadorAlterado);
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
            var user = await _userManager.FindByEmailAsync(solucionadorDetalhes.Email);

            if (!ModelState.IsValid)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

    }
}
