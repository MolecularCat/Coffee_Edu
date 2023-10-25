using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Controllers.Admin
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            bool isAdmin = User.IsInRole("admin");

            return View();
        }

        public IActionResult Users()
        {
            var ListUsers = new List<string>();

            return View();
        }
    }
}
