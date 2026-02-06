using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarracredConsultoria.Models
{
    public class Aula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }  

        public string ConteudoTexto { get; set; } 

        public string VideoUrl { get; set; }

        public int Ordem { get; set; } 

        public int TrilhaId { get; set; }
        [ForeignKey("TrilhaId")]
        public virtual Trilha Trilha { get; set; }
    }
}