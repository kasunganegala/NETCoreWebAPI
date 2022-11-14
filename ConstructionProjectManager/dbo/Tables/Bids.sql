﻿CREATE TABLE [dbo].[Bids]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TenderId] INT NOT NULL, 
	[StartDateTime] DATETIME NOT NULL, 
	[EndDateTime] DATETIME NOT NULL, 
	[IsSubmitted] BIT DEFAULT 0,
	[Status] NVARCHAR(15) DEFAULT 'Submitted', 
	[Comment] NVARCHAR(500), 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	--CONSTRAINT [FK_Bidss_Users] FOREIGN KEY ([CreatedByUsername]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Bids_Tenders] FOREIGN KEY ([TenderId]) REFERENCES [Tenders]([Id])
)
