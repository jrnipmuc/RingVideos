using System.Configuration;
using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Monitor.Data
{
    internal class EventMonitorDbContext : DbContext
    {
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<RingEvent> RingEvents { get; set; }

        public EventMonitorDbContext() : base()
        {
        }

        public EventMonitorDbContext(DbContextOptions<EventMonitorDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = GetDatabasePath();
                
                // Ensure directory exists
                var directory = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        private static string GetDatabasePath()
        {
            // Try to read from App.config
            var configuredPath = ConfigurationManager.AppSettings["DatabasePath"];
            
            if (!string.IsNullOrWhiteSpace(configuredPath))
            {
                return configuredPath;
            }

            // Fallback to AppData if not configured
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "EventMonitor");
            
            return Path.Combine(appDataPath, "eventmonitor.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventMonitorDbContext).Assembly);
        }
    }
}
