using Microsoft.EntityFrameworkCore;
using BarracredConsultoria.Models;

namespace BarracredConsultoria.Data;

public class BarracredContext : DbContext
{
    public BarracredContext(DbContextOptions<BarracredContext> options) : base(options)
    {
    }

    // Cada DbSet representa uma tabela no Banco de Dados
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CondicaoFinanceira> CondicoesFinanceiras { get; set; }
    public DbSet<AgendamentoConsulta> Agendamentos { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
    public DbSet<Aula> Aulas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}