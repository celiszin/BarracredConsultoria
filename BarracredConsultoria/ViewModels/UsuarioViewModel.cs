using System.ComponentModel.DataAnnotations;
using BarracredConsultoria.Models;

public class UsuarioViewModel
{
    public string UsuarioId { get; set; }
    public string UserName { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Foto { get; set; }
    public string Perfil { get; set; }
    public bool IsAdmin { get; set; } = false;
}