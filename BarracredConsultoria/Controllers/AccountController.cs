using Microsoft.AspNetCore.Mvc;
using BarracredConsultoria.Models;
using BarracredConsultoria.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;

namespace BarracredConsultoria.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new AcessoSessaoViewModel { 
                Login = new LoginViewModel { UrlRetorno = returnUrl } 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind(Prefix = "Login")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                string userName = user != null ? user.UserName : model.Email;

                var result = await _signInManager.PasswordSignInAsync(userName, model.Senha, model.Lembrar, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.UrlRetorno) && Url.IsLocalUrl(model.UrlRetorno))
                    {
                        return Redirect(model.UrlRetorno);
                    }
                    return RedirectToAction("Index", "Home");
                }
                
                ViewBag.Error = "Usuário ou senha inválidos.";
            }

            return View(new AcessoSessaoViewModel { Login = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind(Prefix = "Registro")] RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = model.Email, 
                    Email = model.Email,
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento,
                    DataAnalise = DateTime.Now
                };

                var result = await _userManager.CreateAsync(usuario, model.Senha);

                if (result.Succeeded)
                {
                    TempData["MensagemSucesso"] = "Conta criada com sucesso! Faça login abaixo.";
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            ViewBag.ShowRegister = true;
            return View("Login", new AcessoSessaoViewModel { Registro = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EsqueciSenha([Bind(Prefix = "EsqueciSenha")] EsqueciSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                // A lógica de envio de e-mail entra aqui no futuro
                TempData["MensagemSucesso"] = "Se o e-mail estiver cadastrado, você receberá as instruções em breve.";
                return RedirectToAction("Login");
            }

            ViewBag.ShowForgot = true;
            return View("Login", new AcessoSessaoViewModel { EsqueciSenha = model });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}