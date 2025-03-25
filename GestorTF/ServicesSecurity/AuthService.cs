using GestorTF.Models.ViewModels.AuthUserViewModel;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestorTF.ServicesSecurity
{
    public class AuthService
    {
        private readonly ContextApp _context;
        private readonly IConfiguration _configuration;

        public AuthService(ContextApp contextApp, IConfiguration configuration)
        {
            _context = contextApp;
            _configuration = configuration;
        }
        public (string Hash, string Salt) HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            string salt = Convert.ToBase64String(hmac.Key);
            string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return (hash, salt);
        }
        public async Task<string?> Authenticate(AuthLoginViewModel model)
        {
            var user = await _context.Users
       .Where(u => u.Email == model.Email)
       .FirstOrDefaultAsync();


            if (user == null || !VerifyPassword(model.Password, user.Password, user.Salt))
                return null;

            var roles = await _context.UserRoles
        .Where(ur => ur.UserId == user.Id)
        .Select(ur => ur.Role.Name)
        .ToListAsync();
            return GenerateJwtToken(user, roles);
        }
    
        private bool VerifyPassword(string password, string storedHash, string salt)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(salt));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return storedHash == computedHash;
        }

        private string GenerateJwtToken(User user, List<string> roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
