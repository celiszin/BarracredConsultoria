using BarracredConsultoria.Models;

namespace BarracredConsultoria.ViewModels;

public class ConsultoriaViewModel
{
    public Usuario Usuario { get; set; } 
    public CondicaoFinanceira CondicaoFinanceira { get; set; } 
    public AgendamentoConsulta Agendamento { get; set; } 
    public Trilha Trilha { get; set; } 
    public Aula Aula { get; set; } 
}