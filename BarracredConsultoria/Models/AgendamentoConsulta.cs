using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BarracredConsultoria.Models
{
    [Table("Agendamentos")]
    public class AgendamentoConsulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [Display(Name = "Data e Hora da Consulta")]
        public DateTime DataHora { get; set; }

        public string Status { get; set; } = "Pendente";

        public string Observacoes { get; set; }
    }
}