using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace apoemaMatch.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ISolucionadorService _serviceSolucionador;
        private readonly IDemandanteService _serviceDemandante;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, ISolucionadorService serviceSolucionador,
            IDemandanteService serviceDemandante)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _serviceSolucionador = serviceSolucionador;
            _serviceDemandante = serviceDemandante;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Demandante");
                    }
                }
                TempData["Error"] = "Credenciais incorretas, tente novamente";
                return View(loginViewModel);
            }

            TempData["Error"] = "Credenciais incorretas, tente novamente";
            return View(loginViewModel);

        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        public IActionResult RegisterSolucionador()
        {
            var response = new RegisterSolucionadorViewModel();
            return View(response);
        }

        public IActionResult RegisterDemandante()
        {
            var response = new RegisterDemandanteViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "Esse email já está sendo usado";
                return View(registerViewModel);
            }

            var novoUsuario = new ApplicationUser()
            {
                Nome = registerViewModel.Nome,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            var novoUsuarioResponse = await _userManager.CreateAsync(novoUsuario, registerViewModel.Password);

            if (novoUsuarioResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(novoUsuario, PapeisUsuarios.User);
            }

            return View("RegisterCompleted");

        }

        [HttpPost]
        public async Task<IActionResult> RegisterSolucionador(RegisterSolucionadorViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "Esse email já está sendo usado";
                return View(registerViewModel);
            }

            var novoUsuario = new ApplicationUser()
            {
                Nome = registerViewModel.Nome,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Nome
            };
            var novoUsuarioResponse = await _userManager.CreateAsync(novoUsuario, registerViewModel.Password);

            if (novoUsuarioResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(novoUsuario, PapeisUsuarios.Solucionador);

                var usuarioSolucionador = await _userManager.FindByEmailAsync(registerViewModel.Email);

                var novoSolucionador = new Solucionador()
                {
                    IdUsuario = usuarioSolucionador.Id,
                    Disponivel = true,
                    ImagemURL = registerViewModel.ImagemURL,
                    Email = registerViewModel.Email,
                    Nome = registerViewModel.Nome,
                    Telefone = registerViewModel.Telefone,
                    Formacao = registerViewModel.Formacao,
                    AreaDePesquisa = registerViewModel.AreaDePesquisa,
                    CurriculoLattes = registerViewModel.CurriculoLattes,
                    MiniBio = registerViewModel.MiniBio,
                };

                await _serviceSolucionador.AddAsync(novoSolucionador);

            }

            return View("RegisterCompleted");

        }

        [HttpPost]
        public async Task<IActionResult> RegisterDemandante(RegisterDemandanteViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "Esse email já está sendo usado";
                return View(registerViewModel);
            }

            var novoUsuario = new ApplicationUser()
            {
                Nome = registerViewModel.Nome,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Nome
            };
            var novoUsuarioResponse = await _userManager.CreateAsync(novoUsuario, registerViewModel.Password);

            if (novoUsuarioResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(novoUsuario, PapeisUsuarios.Demandante);

                var usuarioDemandante = await _userManager.FindByEmailAsync(registerViewModel.Email);

                var novoDemandante = new Demandante()
                {
                    IdUsuario = usuarioDemandante.Id,
                    ImagemURL = registerViewModel.ImagemURL,
                    Email = registerViewModel.Email,
                    NomeDemandante = registerViewModel.Nome,
                    Telefone = registerViewModel.Telefone,
                    NomeEmpresa = registerViewModel.NomeEmpresa,
                    CargoDemandante = registerViewModel.CargoDemandante,
                    TempoDeMercado = registerViewModel.TempoDeMercado,
                    PorteDaEmpresa = registerViewModel.PorteDaEmpresa,
                    RamoDeAtuacao = registerViewModel.RamoDeAtuacao,
                    SegmentoDeMercado = registerViewModel.SegmentoDeMercado,
                    LinhaDeAtuacaoTI = registerViewModel.LinhaDeAtuacaoTI,
                    RegimeDeTributacao = registerViewModel.RegimeDeTributacao,
                    LeiDeInformatica = registerViewModel.LeiDeInformatica,
                    ObjetivoParceria = registerViewModel.ObjetivoParceria,
                    AreaSolucaoBuscada = registerViewModel.AreaSolucaoBuscada,
                    Descricao = registerViewModel.Descricao
                };

                await _serviceDemandante.AddAsync(novoDemandante);

            }

            return View("RegisterCompleted");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [Authorize(Roles = PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> MeuPerfilSolucionador()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            var solucionadorView = new RegisterSolucionadorViewModel()
            {
                Id = solucionador.Id,
                ImagemURL = solucionador.ImagemURL,
                Email = solucionador.Email,
                Nome = solucionador.Nome,
                Telefone = solucionador.Telefone,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
                MiniBio = solucionador.MiniBio
            };

            return View(solucionadorView);
        }

        [Authorize(Roles = PapeisUsuarios.Demandante)]
        public async Task<IActionResult> MeuPerfilDemandante()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userDemandante = await _userManager.FindByEmailAsync(userEmail);
            var demandante = await _serviceDemandante.GetDemandanteByIdUser(userDemandante.Id);

            var demandanteView = new RegisterDemandanteViewModel()
            {
                Id = demandante.Id,
                ImagemURL = demandante.ImagemURL,
                Email = demandante.Email,
                Nome = demandante.NomeDemandante,
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

            return View(demandanteView);
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
