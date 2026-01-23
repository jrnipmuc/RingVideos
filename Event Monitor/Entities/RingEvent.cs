namespace Event_Monitor.Entities
{
    public class RingEvent
    {
        public int Id { get; set; }
        
        // Ring API identifiers
        public string RingEventId { get; set; } // Unique ID from Ring API (ding_id_str or similar)
        public int DeviceId { get; set; } // FK to our Device table
        
        // Event metadata from Ring API
        public DateTime EventTimestamp { get; set; } // created_at from Ring
        public string EventKind { get; set; } // "motion", "ding", "on_demand"
        public int? DurationSeconds { get; set; } // Video length
        public bool? Answered { get; set; } // For doorbell dings - was it answered?
        public string FavoriteCategory { get; set; } // Ring's favorite/category field
        public int? TimeZone { get; set; } // Timezone offset from Ring
        
        // Video URLs from Ring (these expire)
        public string VideoUrl { get; set; } // HQ video URL from Ring
        public string ThumbnailUrl { get; set; } // Snapshot/thumbnail URL
        public DateTime? VideoUrlExpiresAt { get; set; } // When Ring URL expires
        
        // Video management (local)
        public VideoStatus VideoStatus { get; set; } // Our download status
        public string VideoPath { get; set; } // Local file path after download
        public string ThumbnailPath { get; set; } // Local thumbnail path
        public long? VideoSizeBytes { get; set; } // File size for storage tracking
        
        // Download tracking
        public DateTime FirstSeenDate { get; set; } // When we first discovered this event
        public DateTime? LastDownloadAttemptDate { get; set; } // Last time we tried to download
        public int DownloadAttemptCount { get; set; } // Number of download attempts
        public string LastDownloadError { get; set; } // Error message from last failed attempt
        public DateTime? DownloadedDate { get; set; } // When successfully downloaded
        
        // User actions
        public DateTime? MarkedUnimportantDate { get; set; } // When user marked as unimportant
        public DateTime? VideoDeletedDate { get; set; } // When video file was deleted
        public string UserNotes { get; set; } // Optional user notes about the event
        
        // Motion detection details (if available from Ring)
        public bool? HasMotion { get; set; }
        public bool? HasSound { get; set; }
        public string MotionZones { get; set; } // JSON array of triggered zones
        
        // Additional Ring metadata
        public string RingState { get; set; } // Ring's internal state
        public int? BatteryLevel { get; set; } // Battery % at time of event (if available)
        public string StreamSource { get; set; } // Source of the recording
        
        // Navigation property
        public virtual Device Device { get; set; }
    }
}