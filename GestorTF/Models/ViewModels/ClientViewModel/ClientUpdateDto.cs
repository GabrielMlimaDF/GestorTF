using GestorTF.Validators;
using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models.ViewModels.ClientViewModel
{
    public class ClientUpdateDto
    {
        [StringLength(300, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 300 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

    }
}
