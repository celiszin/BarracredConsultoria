using System.ComponentModel.DataAnnotations;

namespace BarracredConsultoria.Models
{
    public class AgendamentoConsulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; } 

        [Required]
        [Display(Name = "Data e Hora da Consulta")]
        public DateTime DataHora { get; set; }

        public string Status { get; set; } 

        public string Observacoes { get; set; }
    }
}