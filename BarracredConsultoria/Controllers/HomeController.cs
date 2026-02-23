using System;
using Microsoft.AspNetCore.Mvc;
using BarracredConsultoria.Models;
using BarracredConsultoria.ViewModels;

namespace BarracredConsultoria.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new ConsultoriaViewModel
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
            Agendamento = new AgendamentoConsulta
            {
                Id = 1,
                UsuarioId = "1",
                DataHora = DateTime.Now.AddDays(2).AddHours(14), 
                Status = "Confirmado",
                Observacoes = "Dica do Consultor: Traga seus extratos bancários para analisarmos os juros das suas dívidas."
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
            }
        };

        return View(viewModel);
    }
}