using Coffee.Data;
using Coffee.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Repositories
{
    public class NewsRepository
    {
        private ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<List<News>> GetActiveNewsAsync()
        {
            return await _context.News.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<News> CreateNewNewsAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> GetOneNewsAsync(int id)
        {
            return await _context.News.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<News> UpdateEditNewsAsync(News news)
        {
            var item = await _context.News.Where(x => x.Id == news.Id).FirstOrDefaultAsync();
            item.Title = news.Title;
            item.Text = news.Text;
            item.Date = news.Date;
            item.IsActive = news.IsActive;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task GetDeleteNewsAsync(int id)
        {
            var item = await _context.News.Where(x => x.Id == id).FirstOrDefaultAsync();

            
            _context.News.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
