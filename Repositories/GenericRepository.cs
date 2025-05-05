using Microsoft.EntityFrameworkCore;
using ProtelAppT.Data;

namespace ProtelAppT.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ProtelDbContext _context;

        public GenericRepository(ProtelDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
