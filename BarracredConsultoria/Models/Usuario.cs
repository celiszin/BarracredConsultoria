using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarracredConsultoria.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal RendaMensal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDividas { get; set; }

        public string Objetivo { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Foto { get; set; }

        public DateTime DataAnalise { get; set; } = DateTime.Now;
    }
}