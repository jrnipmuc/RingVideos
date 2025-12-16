using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Monitor.Data
{
    internal class ConnectionRepository : IDisposable
    {
        private readonly EventMonitorDbContext _context;

        public ConnectionRepository()
        {
            _context = new EventMonitorDbContext();
        }

        public ConnectionRepository(EventMonitorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Connection>> GetAllAsync()
        {
            var connections = await _context.Connections.Where(l => l.IsActive).ToListAsync();
            
            // Decrypt passwords for use
            foreach (var connection in connections)
            {
                connection.Decrypt();
            }
            
            return connections;
        }

        public async Task<Connection> GetByIdAsync(int id)
        {
            var connection = await _context.Connections.FindAsync(id);
            connection?.Decrypt();
            return connection;
        }

        public async Task<Connection> GetByUsernameAsync(string username)
        {
            var connection = await _context.Connections.FirstOrDefaultAsync(l => l.Username == username && l.IsActive);
            connection?.Decrypt();
            return connection;
        }

        public async Task<Connection> AddAsync(Connection connection)
        {
            // Encrypt before adding to database
            connection.Encrypt();
            
            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();
            
            // Decrypt after saving for continued use
            connection.Decrypt();
            return connection;
        }

        public async Task<Connection> UpdateAsync(Connection connection)
        {
            // Encrypt before updating
            connection.Encrypt();
            
            _context.Entry(connection).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            // Decrypt after saving for continued use
            connection.Decrypt();
            return connection;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var connection = await _context.Connections.FindAsync(id);
            if (connection == null)
                return false;

            connection.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string username)
        {
            return await _context.Connections.AnyAsync(l => l.Username == username && l.IsActive);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
