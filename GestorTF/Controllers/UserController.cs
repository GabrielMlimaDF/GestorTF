using GestoTF2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace GestorTF.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
    }
}