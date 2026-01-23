-- Script Date: 1/19/2026 1:18 PM  - ErikEJ.SqlCeScripting version 3.5.2.103
DROP TABLE [RingEvents];
CREATE TABLE [RingEvents] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [RingEventId] text NOT NULL
, [DeviceId] bigint NOT NULL
, [EventTimestamp] text NOT NULL
, [EventKind] text NULL
, [DurationSeconds] bigint NULL
, [Answered] bigint NULL
, [FavoriteCategory] text NULL
, [TimeZone] bigint NULL
, [VideoUrl] text NULL
, [ThumbnailUrl] text NULL
, [VideoUrlExpiresAt] text NULL
, [VideoStatus] bigint DEFAULT (0) NOT NULL
, [VideoPath] text NULL
, [ThumbnailPath] text NULL
, [VideoSizeBytes] bigint NULL
, [FirstSeenDate] text DEFAULT (datetime('now')) NOT NULL
, [LastDownloadAttemptDate] text NULL
, [DownloadAttemptCount] bigint DEFAULT (0) NOT NULL
, [LastDownloadError] text NULL
, [DownloadedDate] text NULL
, [MarkedUnimportantDate] text NULL
, [VideoDeletedDate] text NULL
, [UserNotes] text NULL
, [HasMotion] bigint NULL
, [HasSound] bigint NULL
, [MotionZones] text NULL
, [RingState] text NULL
, [BatteryLevel] bigint NULL
, [StreamSource] text NULL
, CONSTRAINT [FK_RingEvents_0_0] FOREIGN KEY ([DeviceId]) REFERENCES [Devices] ([Id]) ON DELETE RESTRICT ON UPDATE NO ACTION
);
CREATE INDEX [IX_RingEvents_EventTimestamp] ON [RingEvents] ([EventTimestamp] ASC);
CREATE INDEX [IX_RingEvents_VideoStatus] ON [RingEvents] ([VideoStatus] ASC);
CREATE INDEX [IX_RingEvents_DeviceId_EventTimestamp] ON [RingEvents] ([DeviceId] ASC,[EventTimestamp] ASC);
CREATE UNIQUE INDEX [IX_RingEvents_RingEventId] ON [RingEvents] ([RingEventId] ASC);
