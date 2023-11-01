using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Models.Entities;

namespace Coffee.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private NewsRepository _newsRepository;

        public AdminController(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        public IActionResult Index()
        {
            bool isAdmin = User.IsInRole("Administrator");

            return View();
        }

        public IActionResult Users()
        {
            var listUsers = new List<string>();

            return View(listUsers);
        }

        public async Task<IActionResult> News()  
        {
            var listNews = await _newsRepository.GetNewsAsync();

            return View(listNews);
        }
    }
}
