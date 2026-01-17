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
                var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EventMonitor");

#if DEBUG // During development, place DB in project directory
                appDataPath = AppDomain.CurrentDomain.BaseDirectory;
#endif

                var dbPath = Path.Combine(appDataPath, "eventmonitor.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasIndex(e => e.EffectiveEndDate);
                entity.Property(e => e.EffectiveStartDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(s => s.Connection)
                    .WithMany()
                    .HasForeignKey(s => s.ConnectionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasIndex(e => e.Description);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasOne(d => d.Site)
                    .WithMany(s => s.Devices)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
