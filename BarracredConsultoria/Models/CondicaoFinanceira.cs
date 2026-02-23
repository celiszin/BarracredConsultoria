using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarracredConsultoria.Models
{
    public class CondicaoFinanceira
    {
        [Key]
        public int Id { get; set; }
        public string StatusAtual { get; set; } 
        public decimal RendaMensal { get; set; }
        public decimal TotalDividas { get; set; }
    }
}