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
            var viewModel = await ObterViewModelMista();
            return View(viewModel);
        }

        public async Task<IActionResult> Simulador()
        {
            var viewModel = await ObterViewModelMista();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAgendamento(int agendamentoId, DateTime dataEscolhida)
        {
            var agendamento = await _context.Agendamentos.FindAsync(agendamentoId) 
                              ?? await _context.Agendamentos.FirstOrDefaultAsync(a => a.UsuarioId == "1");

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

        private async Task<ConsultoriaViewModel> ObterViewModelMista()
        {
            var agendamentoDoBanco = await _context.Agendamentos.FirstOrDefaultAsync(a => a.UsuarioId == "1");

            if (agendamentoDoBanco == null)
            {
                agendamentoDoBanco = new AgendamentoConsulta
                {
                    Id = 0,
                    UsuarioId = "1",
                    DataHora = DateTime.Now.AddDays(1).Date.AddHours(14), 
                    Status = "Pendente",
                    Observacoes = "Escolha um horário para sua primeira consultoria."
                };
            }

            return new ConsultoriaViewModel
            {
                Usuario = new Usuario
                {
                    UsuarioId = 1,
                    RendaMensal = 4500.00m,
                    TotalDividas = 1200.00m,
                    Objetivo = "Criar Reserva de Emergência",
                    DataAnalise = DateTime.Now
                },
                CondicaoFinanceira = new CondicaoFinanceira
                {
                    Id = 1,
                    StatusAtual = "Atenção: Endividamento Leve",
                    RendaMensal = 4500.00m,
                    TotalDividas = 1200.00m
                },
                Trilha = new Trilha
                {
                    Id = 1,
                    Titulo = "Saindo do Vermelho",
                    Descricao = "Passo a passo para organizar as contas e fazer sobrar dinheiro."
                },
                Aula = new Aula
                {
                    Id = 1,
                    Titulo = "Mapeando seus gastos invisíveis",
                    TrilhaId = 1,
                    ConteudoTexto = "Assista a este vídeo antes da nossa consultoria.",
                    Ordem = 1
                },
                Agendamento = agendamentoDoBanco
            };
        }
    }
}