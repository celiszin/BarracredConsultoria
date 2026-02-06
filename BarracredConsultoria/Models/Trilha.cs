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

        [Required(ErrorMessage = "O título da trilha é obrigatório")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "URL da Imagem de Capa")]
        public string ImagemUrl { get; set; }

        public virtual ICollection<Aula> Aulas { get; set; } = new List<Aula>();
    }
}