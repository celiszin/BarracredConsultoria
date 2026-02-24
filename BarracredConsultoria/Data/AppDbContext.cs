using Microsoft.EntityFrameworkCore;
using BarracredConsultoria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarracredConsultoria.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

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