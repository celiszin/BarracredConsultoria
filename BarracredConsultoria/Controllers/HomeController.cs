using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarracredConsultoria.Models;
using BarracredConsultoria.ViewModels;
using BarracredConsultoria.Data;

namespace BarracredConsultoria.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var agendamentoDoBanco = await _context.Agendamentos.FirstOrDefaultAsync(a => a.UsuarioId == "1");

            var viewModel = new ConsultoriaViewModel
            {
                Usuario = new Usuario { UsuarioId = 1, Objetivo = "Criar Reserva de Emergência", DataAnalise = DateTime.Now },
                CondicaoFinanceira = new CondicaoFinanceira { StatusAtual = "Atenção: Endividamento Leve", RendaMensal = 4500.00m, TotalDividas = 1200.00m },
                Trilha = new Trilha { Titulo = "Saindo do Vermelho", Descricao = "Passo a passo para organizar as contas." },
                Aula = new Aula { Titulo = "Mapeando seus gastos invisíveis", ConteudoTexto = "Assista a este vídeo.", Ordem = 1 },

                Agendamento = agendamentoDoBanco
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAgendamento(int agendamentoId, DateTime dataEscolhida)
        {
            var agendamento = await _context.Agendamentos.FindAsync(agendamentoId);

            if (agendamento == null)
            {
                agendamento = new AgendamentoConsulta
                {
                    UsuarioId = "1",
                    DataHora = dataEscolhida,
                    Status = "Confirmado",
                    Observacoes = "Agendamento criado pelo portal."
                };

                _context.Agendamentos.Add(agendamento);
            }
            else
            {
                agendamento.DataHora = dataEscolhida;
                agendamento.Status = "Confirmado";

                _context.Update(agendamento);
            }

            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = $"Seu agendamento foi salvo para {dataEscolhida:dd/MM/yyyy HH:mm}!";

            return RedirectToAction("Index");
        }
    }
}