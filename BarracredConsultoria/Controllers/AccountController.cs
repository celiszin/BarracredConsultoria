using Microsoft.AspNetCore.Mvc;
using BarracredConsultoria.Models;
using BarracredConsultoria.Data;
using BarracredConsultoria.ViewModels; // Certifique-se de ter as ViewModels aqui
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
        public async Task<IActionResult> Login(AcessoSessaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Login.Email, 
                    model.Login.Senha, 
                    model.Login.Lembrar, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                ViewBag.Error = "Usuário ou senha inválidos.";
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(AcessoSessaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = model.Registro.Email, 
                    Email = model.Registro.Email,
                    Nome = model.Registro.Nome,
                    DataNascimento = model.Registro.DataNascimento,
                    DataAnalise = DateTime.Now
                };

                var result = await _userManager.CreateAsync(usuario, model.Registro.Senha);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, "Cooperado");
                    
                    await _signInManager.SignInAsync(usuario, isPersistent: false);

                    return RedirectToAction("Teste", "Home", new { id = usuario.Id });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View("Login", model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}