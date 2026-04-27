using System.ComponentModel.DataAnnotations;

namespace BarracredConsultoria.ViewModels
{
    public class AcessoSessaoViewModel
    {
        public LoginViewModel Login { get; set; } = new LoginViewModel();
        public RegistroViewModel Registro { get; set; } = new RegistroViewModel();
        public EsqueciSenhaViewModel EsqueciSenha { get; set; } = new EsqueciSenhaViewModel();
    }

    public class EsqueciSenhaViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        public string Email { get; set; }
    }
}