using GestorTF.Models;
using GestorTF.Services;
using GestorTF.ServicesSecurity;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestorTF.EndPointsTester
{
    [ApiController]
    public class UserApiController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly ContextApp _context;
        private readonly UserService _userService;

        public UserApiController(AuthService authService, ContextApp context, UserService userService)
        {
            _authService = authService;
            _context = context;
            _userService = userService;
        }
        [HttpGet("/v1/user/users")]
        [Authorize]
        public async Task<ActionResult> GetUser()
        {
            var userList = await _context.Users.ToArrayAsync();
            return Ok(userList);
        }
        [HttpPost("/v1/user/registers")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email já está em uso.");

            var (hashedPassword, salt) = _authService.HashPassword(model.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword,
                Salt = salt,
                IsActive = true,
                CreateAt = DateTime.UtcNow,
                RoleId = model.RoleId // Definir a role inicial
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _userService.AddUserRoleAsync(user, "User");

            return Ok("Usuário registrado com sucesso!");
        }
    }


}

