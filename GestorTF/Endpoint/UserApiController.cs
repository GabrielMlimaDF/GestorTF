using GestorTF.Models;
using GestorTF.Models.ViewModels;
using GestorTF.Services;
using GestorTF.ServicesSecurity;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.EndPointsTester
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserServices _service;
        private readonly ContextApp _context;

        public UserApiController(AuthService authService, UserServices service, ContextApp context)
        {
            _authService = authService;
            _service = service;
            _context = context;
        }

        [HttpPost("/v1/user/registers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existEmail = await _context.Users.AnyAsync(x => x.Email == model.Email);
            if (existEmail)
                return StatusCode(500, new ResultViewModel<List<User>>("05x02 - Verifique o email e senha digitados. Se já tem uma conta conosco, tente recuperar a senha."));
            try
            {
                var (hashedPassword, salt) = _authService.HashPassword(model.Password);
                var user = new User
                {
                    //Id = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    Password = hashedPassword,
                    Salt = salt,
                    //IsActive = true,
                    CreateAt = DateTime.UtcNow,
                    RoleId = model.RoleId
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await _service.AddUserRoleAsync(user);
                return Created($"/v1/user/registers/{user.Id}", $"Usuario: {user.Email} criado com sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(400, new ResultViewModel<List<User>>("05x02 - Houve uma falha ao processar requisição."));
            }
        }
    }
}