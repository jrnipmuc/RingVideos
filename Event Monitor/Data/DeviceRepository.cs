using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Monitor.Data
{
    internal class DeviceRepository : IDisposable
    {
        private readonly EventMonitorDbContext _context;

        public DeviceRepository()
        {
            _context = new EventMonitorDbContext();
        }

        public DeviceRepository(EventMonitorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.Site)
                .OrderBy(d => d.Description)
                .ToListAsync();
        }

        public async Task<List<Device>> GetUnassociatedAsync()
        {
            return await _context.Devices
                .Where(d => d.SiteId == 0)
                .OrderBy(d => d.Description)
                .ToListAsync();
        }

        public async Task<List<Device>> GetBySiteAsync(long siteId)
        {
            return await _context.Devices
                .Include(d => d.Site)
                .Where(d => d.SiteId == siteId)
                .OrderBy(d => d.Description)
                .ToListAsync();
        }

        public async Task<Device> GetByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.Site)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Device> GetByRingIdAsync(long ringId)
        {
            return await _context.Devices
                .Include(d => d.Site)
                .FirstOrDefaultAsync(d => d.RingId == ringId);
        }

        public async Task<Device> AddAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<Device> UpdateAsync(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                return false;

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Device> UpsertFromRingAsync(long ringId, string deviceId, string kind, string description, long deviceTypeId)
        {
            var existing = await GetByRingIdAsync(ringId);
            
            if (existing != null)
            {
                // Update existing device
                existing.DeviceId = deviceId;
                existing.Kind = kind;
                existing.Description = description;
                existing.DeviceTypeId = deviceTypeId;
                
                await UpdateAsync(existing);
                return existing;
            }
            else
            {
                // Add new device (unassociated by default)
                var newDevice = new Device
                {
                    RingId = ringId,
                    DeviceId = deviceId,
                    Kind = kind,
                    Description = description,
                    DeviceTypeId = deviceTypeId,
                    SiteId = 0 // Unassociated
                };
                
                return await AddAsync(newDevice);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
