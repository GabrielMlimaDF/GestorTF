using System.ComponentModel.DataAnnotations;

namespace GestorTF.Models
{
    public class Chamado : EntidadeUsuario
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        public DateTime? DataFechamento { get; set; }

        public bool Aberto { get; set; } = true;

        public Guid ClienteId { get; set; }
    }
}