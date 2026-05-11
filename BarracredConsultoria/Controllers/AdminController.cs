using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BarracredConsultoria.Models;
using System.Linq;
using System.Threading.Tasks;

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

        // Lista todos os usuários aguardando aprovação
        [HttpGet]
        public IActionResult Index()
        {
            var usuariosPendentes = _userManager.Users.Where(u => !u.IsAprovado).ToList();
            return View(usuariosPendentes);
        }

        [HttpPost]
        public async Task<IActionResult> Aprovar(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsAprovado = true;
                await _userManager.UpdateAsync(user);
                
                // Adiciona o usuário ao cargo de Cooperado ao aprovar
                await _userManager.AddToRoleAsync(user, "Cooperado");
                
                TempData["MensagemSucesso"] = $"Usuário {user.Nome} aprovado com sucesso!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Rejeitar(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                TempData["MensagemSucesso"] = "Solicitação de cadastro rejeitada e excluída.";
            }
            return RedirectToAction("Index");
        }
    }
}