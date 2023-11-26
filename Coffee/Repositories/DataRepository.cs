using Coffee.Data;
using Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Repositories
{
    public class DataRepository
    {
        private ApplicationDbContext _context;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var result = await _context.Users.ToListAsync();

            return result;
        }
    }
}
