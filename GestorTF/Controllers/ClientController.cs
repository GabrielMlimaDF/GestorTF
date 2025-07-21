using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorTF.Controllers
{
    public class ClientController : Controller 
    {
        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateClient()
        {
            return PartialView("_CreateClient");
        }
        public IActionResult ListClient()
        {
            return PartialView("_ListClient");
        }

    }
}
