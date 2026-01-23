using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event_Monitor.Data.Configurations
{
    internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasIndex(e => e.EffectiveEndDate);
            builder.Property(e => e.EffectiveStartDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(s => s.Connection)
                .WithMany()
                .HasForeignKey(s => s.ConnectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}