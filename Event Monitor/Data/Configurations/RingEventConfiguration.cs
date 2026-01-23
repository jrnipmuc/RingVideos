using Event_Monitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event_Monitor.Data.Configurations
{
    internal class RingEventConfiguration : IEntityTypeConfiguration<RingEvent>
    {
        public void Configure(EntityTypeBuilder<RingEvent> builder)
        {
            builder.HasKey(e => e.Id);
            
            // Unique constraint on RingEventId to prevent duplicates
            builder.HasIndex(e => e.RingEventId).IsUnique();
            
            // Composite index for efficient queries by device and timestamp
            builder.HasIndex(e => new { e.DeviceId, e.EventTimestamp });
            
            // Index for finding events needing download
            builder.HasIndex(e => e.VideoStatus);
            
            // Index for timestamp-based queries
            builder.HasIndex(e => e.EventTimestamp);
            
            // String field constraints
            builder.Property(e => e.RingEventId).IsRequired().HasMaxLength(100);
            builder.Property(e => e.EventKind).HasMaxLength(50);
            builder.Property(e => e.FavoriteCategory).HasMaxLength(100);
            builder.Property(e => e.VideoUrl).HasMaxLength(1000);
            builder.Property(e => e.ThumbnailUrl).HasMaxLength(1000);
            builder.Property(e => e.VideoPath).HasMaxLength(500);
            builder.Property(e => e.ThumbnailPath).HasMaxLength(500);
            builder.Property(e => e.LastDownloadError).HasMaxLength(1000);
            builder.Property(e => e.UserNotes).HasMaxLength(2000);
            builder.Property(e => e.MotionZones).HasMaxLength(500);
            builder.Property(e => e.RingState).HasMaxLength(100);
            builder.Property(e => e.StreamSource).HasMaxLength(100);
            
            // Default values
            builder.Property(e => e.FirstSeenDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.DownloadAttemptCount).HasDefaultValue(0);
            builder.Property(e => e.VideoStatus).HasDefaultValue(VideoStatus.NotDownloaded);
            
            // FK to Device
            builder.HasOne(e => e.Device)
                .WithMany()
                .HasForeignKey(e => e.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}