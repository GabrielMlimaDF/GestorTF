using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models
{
    public class Cliente : EntidadeUsuario
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        public bool Ativo { get; set; } = true;
    }
}