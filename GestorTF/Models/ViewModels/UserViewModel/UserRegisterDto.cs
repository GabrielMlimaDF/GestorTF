using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models.ViewModels.UserViewModel
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nível de acesso é obrigatório.")]
        [Range(1, 2, ErrorMessage = "O ID da Role deve ser um número válido.")]
        public int RoleId { get; set; } // Role associada ao usuário
    }
}