using apoemaMatch.Data;
using apoemaMatch.Data.Services;
using apoemaMatch.Data.Static;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using apoemaMatch.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, ISolucionadorService serviceSolucionador,
            IDemandanteService serviceDemandante, IAccountRepository accountRepository, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _serviceSolucionador = serviceSolucionador;
            _serviceDemandante = serviceDemandante;
            _accountRepository = accountRepository;
            _emailService = emailService;
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
                        return RedirectToAction("Index", "Home");
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
                Nome = textoSemAcentos(registerViewModel.Nome),
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
                Nome = textoSemAcentos(registerViewModel.NomeCompleto),
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName
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
                    Cpf = registerViewModel.Cpf,
                    ImagemURL = registerViewModel.ImagemURL,
                    Email = registerViewModel.Email,
                    Nome = registerViewModel.NomeCompleto,
                    Telefone = registerViewModel.Telefone,
                    Formacao = registerViewModel.Formacao,
                    AreaDePesquisa = registerViewModel.AreaDePesquisa,
                    CurriculoLattes = registerViewModel.CurriculoLattes,
                    MiniBio = registerViewModel.MiniBio,
                };

                UserEmailOptions dados = DadosEmail(novoSolucionador.Email, novoSolucionador.Nome, "Solucionador");
                await _emailService.SendForConfirmationRegistrationEmail(dados);
                await _serviceSolucionador.AddAsync(novoSolucionador);
                return View("RegisterCompleted");
            }

            TempData["Error"] = "Nome de usuário já sendo utilizado";
            return View(registerViewModel);

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
                Nome = textoSemAcentos(registerViewModel.NomeCompleto),
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName
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
                    Cnpj = registerViewModel.Cnpj,
                    Email = registerViewModel.Email,
                    NomeDemandante = registerViewModel.NomeCompleto,
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

                UserEmailOptions dados = DadosEmail(novoDemandante.Email, novoDemandante.NomeDemandante, "Demandante");
                await _emailService.SendForConfirmationRegistrationEmail(dados);
                await _serviceDemandante.AddAsync(novoDemandante);
                return View("RegisterCompleted");
            }


            TempData["Error"] = "Nome de usuário já sendo utilizado";
            return View(registerViewModel);
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

        [Authorize(Roles = PapeisUsuarios.Admin + "," + PapeisUsuarios.Solucionador)]
        public async Task<IActionResult> MeuPerfilSolucionador()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userSolucionador = await _userManager.FindByEmailAsync(userEmail);
            var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(userSolucionador.Id);

            var solucionadorView = new RegisterSolucionadorViewModel()
            {
                UserName = userSolucionador.UserName,
                Disponivel = solucionador.Disponivel,
                Cpf = solucionador.Cpf,
                Id = solucionador.Id,
                ImagemURL = solucionador.ImagemURL,
                Email = userSolucionador.Email,
                NomeCompleto = solucionador.Nome,
                Telefone = solucionador.Telefone,
                Formacao = solucionador.Formacao,
                AreaDePesquisa = solucionador.AreaDePesquisa,
                CurriculoLattes = solucionador.CurriculoLattes,
                MiniBio = solucionador.MiniBio
            };

            return View(solucionadorView);
        }

        [Authorize(Roles = PapeisUsuarios.Admin + "," + PapeisUsuarios.Demandante)]
        public async Task<IActionResult> MeuPerfilDemandante()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userDemandante = await _userManager.FindByEmailAsync(userEmail);
            var demandante = await _serviceDemandante.GetDemandanteByIdUser(userDemandante.Id);

            var demandanteView = new RegisterDemandanteViewModel()
            {
                UserName = userDemandante.UserName,
                Cnpj = demandante.Cnpj,
                Id = demandante.Id,
                ImagemURL = demandante.ImagemURL,
                Email = userDemandante.Email,
                NomeCompleto = demandante.NomeDemandante,
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

        [AllowAnonymous]
        [HttpGet("fotgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("fotgot-password")]
        public async Task<IActionResult> ForgotPassword(EsqueciSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // code here
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                Token = token,
                UserId = uid
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        public IActionResult ChangeEmail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(MudarEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.CurrentEmail);

                if (user == null)
                {
                    TempData["Error"] = "Esse email não está cadastrado";
                    return View(model);
                }

                var userVerificador = await _userManager.FindByEmailAsync(model.NewEmail);

                if (userVerificador != null)
                {
                    TempData["Error"] = "Esse email já está cadastrado";
                    return View(model);
                }

                user.Email = model.NewEmail;
                user.EmailConfirmed = true;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (User.IsInRole("Demandante"))
                    {
                        var demandante = await _serviceDemandante.GetDemandanteByIdUser(user.Id);
                        demandante.Email = model.NewEmail;
                        _serviceDemandante.UpdateAsync(demandante.Id, demandante);
                    }
                    else if (User.IsInRole("Solucionador"))
                    {
                        var solucionador = await _serviceSolucionador.GetSolucionadorByIdUser(user.Id);
                        solucionador.Email = model.NewEmail;
                        _serviceSolucionador.UpdateAsync(solucionador.Id, solucionador);
                    }

                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(MudarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }

                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmacaoCadastro(ConfirmacaoCadastroViewModel model)
        {
            model.EmailSent = true;
            return View(model);
        }

        public UserEmailOptions DadosEmail(string Email, string Nome, string TipoUsuario)
        {
            UserEmailOptions dados = new UserEmailOptions
            {
                ToEmails = new List<string>() { Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", Nome),
                        new KeyValuePair<string, string>("{{UserType}}", TipoUsuario)
                    }
            };
            return dados;
        }

        public static string textoSemAcentos(string text)
        {
            string withDiacritics = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string withoutDiacritics = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < withDiacritics.Length; i++)
            {
                text = text.Replace(withDiacritics[i].ToString(), withoutDiacritics[i].ToString());
            }
            return text;
        }
    }
}
