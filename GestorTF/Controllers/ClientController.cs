using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorTF.Controllers
{
    public class ClientController : Controller 
    {
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

    }
}
