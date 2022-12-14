using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        public async Task<IActionResult> Filter(string searchString)
        {
            var todosSolucionadores = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var resultadoFiltro = todosSolucionadores.Where(n => n.Nome.Contains(searchString))
                    .ToList();
                return View("Index", resultadoFiltro);
            }

            return View("Index", todosSolucionadores);
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
                Cpf = solucionador.Cpf,
                Nome = solucionador.Nome,
                Email = solucionador.Email,
                Telefone = solucionador.Telefone,
                MiniBio = solucionador.MiniBio,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
                Disponivel = solucionador.Disponivel
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

            var solucionadorBanco = await _service.GetByIdAsync(id);

            var solucionadorAlterado = new SolucionadorViewModel()
            {
                Id = solucionador.Id,
                ImagemURL = solucionador.ImagemURL,
                Cpf = solucionador.Cpf,
                Nome = solucionador.Nome,
                Email = solucionador.Email,
                Telefone = solucionador.Telefone,
                MiniBio = solucionador.MiniBio,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
                Disponivel = solucionadorBanco.Disponivel
        };

            await _service.UpdateSolucionadorAsync(solucionadorAlterado);
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Solucionador");
            }
            return RedirectToAction("MeuPerfilSolucionador", "Account");
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

        public async Task<IActionResult> MudarStatus(int id)
        {
            var solucionador = await _service.GetByIdAsync(id);

            if (solucionador == null)
            {
                return View("NotFound");
            }

            var response = new SolucionadorViewModel()
            {
                Id = solucionador.Id,
                ImagemURL = solucionador.ImagemURL,
                Disponivel = solucionador.Disponivel,
                Cpf = solucionador.Cpf,
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

        public async Task<IActionResult> AtivarDesativar(int id)
        {
            var solucionador = await _service.GetByIdAsync(id);

            if (id != solucionador.Id)
            {
                return View("NotFound");
            }

            if (solucionador.Disponivel)
            {
                var solucionadorAlterado = new SolucionadorViewModel()
                {
                    Id = solucionador.Id,
                    Disponivel = false,
                    ImagemURL = solucionador.ImagemURL,
                    Cpf = solucionador.Cpf,
                    Nome = solucionador.Nome,
                    Email = solucionador.Email,
                    Telefone = solucionador.Telefone,
                    MiniBio = solucionador.MiniBio,
                    Formacao = solucionador.Formacao,
                    AreaDePesquisa = solucionador.AreaDePesquisa,
                    CurriculoLattes = solucionador.CurriculoLattes,
                };
                await _service.UpdateSolucionadorAsync(solucionadorAlterado);
            }
            else
            {
                var solucionadorAlterado = new SolucionadorViewModel()
                {
                    Id = solucionador.Id,
                    Disponivel = true,
                    ImagemURL = solucionador.ImagemURL,
                    Cpf = solucionador.Cpf,
                    Nome = solucionador.Nome,
                    Email = solucionador.Email,
                    Telefone = solucionador.Telefone,
                    MiniBio = solucionador.MiniBio,
                    Formacao = solucionador.Formacao,
                    AreaDePesquisa = solucionador.AreaDePesquisa,
                    CurriculoLattes = solucionador.CurriculoLattes,
                };
                await _service.UpdateSolucionadorAsync(solucionadorAlterado);
            }
            
            return RedirectToAction("MeuPerfilSolucionador", "Account");
        }

        public async Task<IActionResult> AtivarDesativarAdmin(int id)
        {
            var solucionador = await _service.GetByIdAsync(id);

            if (id != solucionador.Id)
            {
                return View("NotFound");
            }

            if (solucionador.Disponivel)
            {
                var solucionadorAlterado = new SolucionadorViewModel()
                {
                    Id = solucionador.Id,
                    Disponivel = false,
                    ImagemURL = solucionador.ImagemURL,
                    Cpf = solucionador.Cpf,
                    Nome = solucionador.Nome,
                    Email = solucionador.Email,
                    Telefone = solucionador.Telefone,
                    MiniBio = solucionador.MiniBio,
                    Formacao = solucionador.Formacao,
                    AreaDePesquisa = solucionador.AreaDePesquisa,
                    CurriculoLattes = solucionador.CurriculoLattes,
                };
                await _service.UpdateSolucionadorAsync(solucionadorAlterado);
            }
            else
            {
                var solucionadorAlterado = new SolucionadorViewModel()
                {
                    Id = solucionador.Id,
                    Disponivel = true,
                    ImagemURL = solucionador.ImagemURL,
                    Cpf = solucionador.Cpf,
                    Nome = solucionador.Nome,
                    Email = solucionador.Email,
                    Telefone = solucionador.Telefone,
                    MiniBio = solucionador.MiniBio,
                    Formacao = solucionador.Formacao,
                    AreaDePesquisa = solucionador.AreaDePesquisa,
                    CurriculoLattes = solucionador.CurriculoLattes,
                };
                await _service.UpdateSolucionadorAsync(solucionadorAlterado);
            }

            return RedirectToAction("Index", "Solucionador");
        }

    }
}
