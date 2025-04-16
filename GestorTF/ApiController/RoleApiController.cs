using GestorTF.Models;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestorTF.ApiController
{
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        private readonly ContextApp _context;

        public RoleApiController(ContextApp context)
        {
            _context = context;
        }
       
        [HttpGet("/v1/role/roles")]
        [Authorize] 
        public async Task<ActionResult> GetRole()
        {
            var roleList = await _context.Roles.ToArrayAsync();
            return Ok(roleList);
        }

        [HttpPost("/v1/role/creates")]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto model)
        {
            if (await _context.Roles.AnyAsync(r => r.Name == model.Name))
                return BadRequest("Perfil já existe.");

            var role = new Role
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok("Perfil criado com sucesso!");
        }
    }
}

