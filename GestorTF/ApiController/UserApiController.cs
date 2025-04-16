using GestorTF.Models.ViewModels;
using GestorTF.Models.ViewModels.UserViewModel;
using GestorTF.Repository;
using GestorTF.Services;
using GestorTF.ServicesSecurity;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.ApiController
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IUserRepository _userRepository;

        public UserApiController(AuthService authService, UserServices service, ContextApp context, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("/v1/user/registers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existEmail = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existEmail)
                return StatusCode(500, new ResultViewModel<string>("05x02 - Verifique o email e senha digitados. Se já tem uma conta conosco, tente recuperar a senha."));
            try
            {
                var (hashedPassword, salt) = _authService.HashPassword(model.Password);
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = hashedPassword,
                    Salt = salt,
                    CreateAt = DateTime.UtcNow,
                    RoleId = model.RoleId
                };
                await _userRepository.CreateUserAsync(user);
                return Created($"/v1/user/registers/{user.Id}", $"Usuario: {user.Email} criado com sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(400, new ResultViewModel<string>("05x02 - Houve uma falha ao processar requisição."));
            }
        }
    }
}