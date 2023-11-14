using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Models.Entities;
using System.Security.Claims;

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

        [Route("/admin/news/createnews")]
        [HttpGet]
        public async Task<IActionResult> CreateNews()
        {
            return View();
        }

        [Route("/admin/news/createnews")]
        [HttpPost]
        public async Task<IActionResult> CreateNewNews(News news)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                news.AuthorId = userId;

                news.Date = DateTime.SpecifyKind(news.Date, DateTimeKind.Utc);

                var result = await _newsRepository.CreateNewNewsAsync(news);
            }

            return Redirect("/Admin/News");
        }
    }
}
