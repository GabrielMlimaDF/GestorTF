using GestoTF2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorTF.Models
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int RoleId { get; set; }

        // Relacionamentos
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
