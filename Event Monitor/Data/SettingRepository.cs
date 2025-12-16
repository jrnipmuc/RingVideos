using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Monitor.Data
{
    internal class SettingRepository : IDisposable
    {
        private readonly EventMonitorDbContext _context;

        public SettingRepository()
        {
            _context = new EventMonitorDbContext();
        }

        public SettingRepository(EventMonitorDbContext context)
        {
            _context = context;
        }

        public async Task<Setting> GetActiveSettingAsync()
        {
            return await _context.Settings
                .Include(s => s.Connection)
                .FirstOrDefaultAsync(s => s.EffectiveEndDate == null);
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings
                .Include(s => s.Connection)
                .OrderByDescending(s => s.EffectiveStartDate)
                .ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings
                .Include(s => s.Connection)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Setting> CreateNewActiveSettingAsync(string rootDirectory, int connectionId)
        {
            // End-date the current active setting
            var currentActive = await GetActiveSettingAsync();
            if (currentActive != null)
            {
                currentActive.EffectiveEndDate = DateTime.Now;
                _context.Entry(currentActive).State = EntityState.Modified;
            }

            // Create new active setting
            var newSetting = new Setting
            {
                RootDirectory = rootDirectory,
                ConnectionId = connectionId,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = null
            };

            _context.Settings.Add(newSetting);
            await _context.SaveChangesAsync();

            // Reload with Connection included
            return await GetByIdAsync(newSetting.Id);
        }

        public async Task<Setting> UpdateActiveSettingAsync(string rootDirectory, int connectionId)
        {
            var activeSetting = await GetActiveSettingAsync();
            
            if (activeSetting == null)
            {
                // No active setting exists, create one
                return await CreateNewActiveSettingAsync(rootDirectory, connectionId);
            }

            // Check if anything actually changed
            if (activeSetting.RootDirectory == rootDirectory && activeSetting.ConnectionId == connectionId)
            {
                return activeSetting;
            }

            // End-date current and create new
            return await CreateNewActiveSettingAsync(rootDirectory, connectionId);
        }

        public async Task<List<Setting>> GetHistoryAsync()
        {
            return await _context.Settings
                .Include(s => s.Connection)
                .Where(s => s.EffectiveEndDate != null)
                .OrderByDescending(s => s.EffectiveEndDate)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
