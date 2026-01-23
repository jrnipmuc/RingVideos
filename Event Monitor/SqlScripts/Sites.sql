DROP TABLE [Sites];
CREATE TABLE [Sites] (
  [Id] bigint NOT NULL
, [Description] text NOT NULL
, CONSTRAINT [sqlite_master_PK_Sites] PRIMARY KEY ([Id])
);

INSERT INTO [Sites] ([Description]) VALUES
	('Sasha'),
	('Riverglade'),
	('Moneymaker')
;