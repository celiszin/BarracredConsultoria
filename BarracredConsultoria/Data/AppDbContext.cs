using Microsoft.EntityFrameworkCore;
using BarracredConsultoria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarracredConsultoria.Data;

public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AgendamentoConsulta> Agendamentos { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
    public DbSet<Aula> Aulas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<Usuario>(entity => {
            entity.Property(e => e.RendaMensal).HasPrecision(18, 2);
            entity.Property(e => e.TotalDividas).HasPrecision(18, 2);
        });
    }
}