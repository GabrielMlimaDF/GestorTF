using GestorTF.Validators;
using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models.ViewModels.ClientViewModel
{
    public class ClientRegisterDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 300 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatorio.")]
        [StringLength(14)]
        [Cnpj(ErrorMessage = "CNPJ informado não é válido.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [StringLength(20)]
        public string Telefone { get; set; }

        public bool? Ativo { get; set; } = true;
    }
}