using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorTF.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }
    }
}