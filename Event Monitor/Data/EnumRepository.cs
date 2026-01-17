using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Monitor.Data
{



    internal class EnumRepository<T> : IDisposable where T : class, IEnumEntity
    {
        private readonly EventMonitorDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EnumRepository()
        {
            _context = new EventMonitorDbContext();
            _dbSet = _context.Set<T>();
        }

        public EnumRepository(EventMonitorDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.OrderBy(e => e.Description).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string description)
        {
            return await _dbSet.AnyAsync(e => e.Description == description);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}