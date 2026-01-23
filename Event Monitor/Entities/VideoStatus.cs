namespace Event_Monitor.Entities
{
    public enum VideoStatus
    {
        NotDownloaded = 0,      // Event exists in DB, video not yet downloaded
        Downloading = 1,        // Currently being downloaded
        Downloaded = 2,         // Video successfully downloaded and exists on disk
        DownloadFailed = 3,     // Download attempts failed
        MarkedUnimportant = 4,  // User marked as unimportant (may or may not have video)
        Expired = 5,            // Ring URL expired before we could download
        Deleted = 6             // Video was downloaded but user deleted it
    }
}