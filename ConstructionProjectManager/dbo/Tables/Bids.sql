CREATE TABLE [dbo].[Bids]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TenderId] INT NOT NULL, 
	[StartDateTime] DATETIME NOT NULL, 
	[EndDateTime] DATETIME NOT NULL, 
	[Status] NVARCHAR(4) DEFAULT 'Submitted', 
	[Comment] NVARCHAR(500), 
	[CreatedByUserId] INT NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_Bidss_Users] FOREIGN KEY ([CreatedByUserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Bids_Tenders] FOREIGN KEY ([TenderId]) REFERENCES [Tenders]([Id])
)
