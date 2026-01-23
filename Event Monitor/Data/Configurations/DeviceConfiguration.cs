using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event_Monitor.Data.Configurations
{
    internal class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasOne(d => d.Site)
                .WithMany(s => s.Devices)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}