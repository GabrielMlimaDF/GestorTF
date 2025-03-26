using GestorTF.Models;
using GestorTF.Services;
using GestorTF.ServicesSecurity;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestorTF.Controllers
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
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<User>>> GetUserAll()
        {
            var userList = await _userService.GetUserAsync();

            if (userList == null || userList.Count == 0)
                return NotFound("Nenhum usuário encontrado.");

            return Ok(userList);
        }

        [HttpPost("/v1/user/creates")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}