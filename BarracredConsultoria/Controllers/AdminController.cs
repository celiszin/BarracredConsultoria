using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarracredConsultoria.Models;

namespace BarracredConsultoria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Usuario> _userManager;

        public AdminController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        // Esta é a sua página principal de gerenciamento
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pendentes = await _userManager.Users
                .Where(u => !u.IsAprovado)
                .Select(u => new UsuarioViewModel
                {
                    UsuarioId = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    UserName = u.UserName,
                    Foto = u.Foto, 
                    Perfil = "Pendente",
                    DataNascimento = u.DataNascimento
                })
                .ToListAsync();

            return View(pendentes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Aprovar(string usuarioId) // Nome padronizado para o JS
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user != null)
            {
                user.IsAprovado = true;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Cooperado");
                TempData["MensagemSucesso"] = $"Utilizador {user.Nome} aprovado!";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rejeitar(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                TempData["MensagemSucesso"] = "Cadastro rejeitado e removido.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}