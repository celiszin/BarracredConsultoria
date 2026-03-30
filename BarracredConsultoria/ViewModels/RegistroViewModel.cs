using System.ComponentModel.DataAnnotations;

namespace BarracredConsultoria.ViewModels;

public class RegistroViewModel
{
    [Display(Name = "Nome Completo", Prompt = "Informe seu Nome Completo")]
    [Required(ErrorMessage = "Por Favor, informe seu Nome")]
    [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres")]
    public string Nome { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Nascimento", Prompt = "Informe sua Data De Nascimento")]
    [Required(ErrorMessage = "Por Favor, informe sua Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    [Display(Prompt = "Informe seu EMail")]
    [Required(ErrorMessage = "Por Favor, informe um email válido")]
    [EmailAddress(ErrorMessage = "Por favor, informe um email válido")]
    [StringLength(100, ErrorMessage = "O email deve possuir no máximo 100 caracteres")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha de acesso", Prompt = "Informe uma senha para acesso")]
    [Required(ErrorMessage = "Por favor, informe a senha de acesso")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve possuir no mínimo 6 e no máximo 20 caracteres")]
    public string Senha { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar a senha de acesso", Prompt = "Confirme sua senha de Acesso")]
    [Compare("Senha", ErrorMessage = "As Senhas não conferem")]
    public string ConfirmacaoSenha { get; set; }

    public IFormFile Foto { get; set; }
}