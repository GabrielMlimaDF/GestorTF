using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models.ViewModels.Called
{
    public class CalledRegisterDto
    {
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MinLength(10, ErrorMessage = "A descrição deve ter no mínimo 10 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public Guid ClienteId { get; set; }
    }
}