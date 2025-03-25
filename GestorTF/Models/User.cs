using System.Data;
using System.Security.Cryptography;

namespace GestoTF2.Models
{
    

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

             // Relacionamento com Role
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;
    }
 
}

