using Coffee.Models.Entities;
using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Coffee.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private NewsRepository _newsRepository;
        private DataRepository _dataRepository;

        public AdminController(NewsRepository newsRepository, DataRepository dataRepository)
        {
            _newsRepository = newsRepository;
            _dataRepository = dataRepository;
        }
        public ActionResult Index()
        {
            bool isAdmin = User.IsInRole("Administrator");

            return View();
        }

        public async Task<IActionResult> Users()
        {
            var list = await _dataRepository.GetUsersAsync();

            return View(list);
        }

        [Route("/admin/users/block/{userId}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            await _dataRepository.BlockUserAsync(userId);

            return Redirect("/Admin/Users");
        }

        [Route("/admin/users/unblock/{userId}")]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            await _dataRepository.UnblockUserAsync(userId);

            return Redirect("/Admin/Users");
        }

        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetNewsAsync();

            return View(listNews);
        }

        [Route("/admin/news/createnews")]
        [HttpGet]
        public async Task<ActionResult> CreateNews()
        {
            return View();
        }

        [Route("/admin/news/createnews")]
        [HttpPost]
        public async Task<ActionResult> CreateNewNews(News news)
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

        [Route("/admin/news/editnews/{id}")]
        [HttpGet]
        public async Task<ActionResult> EditNews(int id)
        {
            var news = await _newsRepository.GetOneNewsAsync(id);

            return View(news);
        }

        [Route("/admin/news/editnews/{id}")]
        [HttpPost]
        public async Task<ActionResult> DoneEditNews(News news)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            news.AuthorId = userId;

            news.Date = DateTime.SpecifyKind(news.Date, DateTimeKind.Utc);
            news.CreateDate = DateTime.SpecifyKind(news.CreateDate, DateTimeKind.Utc);

            var result = await _newsRepository.UpdateEditNewsAsync(news);

            return Redirect("/Admin/News");
        }

        [Route("/admin/news/deletenews/{id}")]
        [HttpGet]
        public async Task<ActionResult> DeleteNews(int id)
        {
            await _newsRepository.GetDeleteNewsAsync(id);

            return Redirect("/Admin/News");
        }
    }
}
