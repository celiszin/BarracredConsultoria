using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BarracredConsultoria.Data;
using BarracredConsultoria.Models;
using BarracredConsultoria.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BarracredConsultoria.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public HomeController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var agendamento = await _context.Agendamentos
                .FirstOrDefaultAsync(a => a.UsuarioId == usuario.Id && a.DataHora >= DateTime.Now);

            var condicao = new CondicaoFinanceira
            {
                RendaMensal = usuario.RendaMensal,
                TotalDividas = usuario.TotalDividas,
                StatusAtual = usuario.TotalDividas > (usuario.RendaMensal * 3) ? "Alerta" : "Saudável"
            };

            var viewModel = new ConsultoriaViewModel
            {
                Usuario = usuario,
                CondicaoFinanceira = condicao,
                Agendamento = agendamento
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAgendamento(int agendamentoId, DateTime dataEscolhida)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return RedirectToAction("Login", "Account");

            if (agendamentoId > 0)
            {
                var agendamento = await _context.Agendamentos.FindAsync(agendamentoId);
                if (agendamento != null && agendamento.UsuarioId == usuario.Id)
                {
                    agendamento.DataHora = dataEscolhida;
                    agendamento.Status = "Confirmado";
                    _context.Update(agendamento);
                }
            }
            else
            {
                var novoAgendamento = new AgendamentoConsulta
                {
                    UsuarioId = usuario.Id,
                    DataHora = dataEscolhida,
                    Status = "Agendado",
                    Observacoes = "Agendamento via Portal do Cooperado"
                };
                _context.Agendamentos.Add(novoAgendamento);
            }

            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Seu horário foi agendado para " + dataEscolhida.ToString("dd/MM/yyyy HH:mm") + " com sucesso!";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Sonhos()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return RedirectToAction("Login", "Account");

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarOnboarding(Usuario model)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario != null)
            {
                usuario.Objetivo = model.Objetivo;
                await _userManager.UpdateAsync(usuario); 
                TempData["MensagemSucesso"] = "Sua meta foi salva com sucesso!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Simulador()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new ConsultoriaViewModel
            {
                Usuario = usuario
            };

            return View(viewModel);
        }
    }
}