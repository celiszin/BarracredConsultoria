using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarracredConsultoria.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "Renda Mensal")]
        [DataType(DataType.Currency)]
        public decimal RendaMensal { get; set; }

        [Display(Name = "Total de Dívidas")]
        [DataType(DataType.Currency)]
        public decimal TotalDividas { get; set; }

        [Display(Name = "Objetivo")]
        public string Objetivo { get; set; } 

        public DateTime DataAnalise { get; set; } = DateTime.Now;
    }
}