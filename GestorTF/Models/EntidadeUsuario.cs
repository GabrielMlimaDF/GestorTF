using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models
{
    public abstract class EntidadeUsuario
    {
        [Required]
        public Guid UsuarioId { get; set; }
    }
}