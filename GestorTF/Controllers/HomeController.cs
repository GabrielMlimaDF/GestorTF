using GestorTF.Models;
using GestoTF2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace GestorTF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContextApp _context;

        public HomeController(ILogger<HomeController> logger, ContextApp context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            // Obt�m o ID do usu�rio autenticado a partir do token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redireciona para login se n�o estiver autenticado
            }

            // Busca o nome do usu�rio no banco
            var userName = await _context.Users
                .Where(u => u.Id.ToString() == userId)
                .Select(u => u.Name)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account"); // Redireciona caso o usu�rio n�o seja encontrado
            }
            ViewData["NomeCompleto"] = userName;
            return View();
        }
    }
}