using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BarracredConsultoria.Models
{
    public class Trilha
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public virtual ICollection<Aula> Aulas { get; set; } = new List<Aula>();
    }
}