CREATE TABLE [dbo].[BidTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[BidId] INT NOT NULL, 
	[ParentBidTaskkId] INT NULL, 
	[Name] NVARCHAR(20) NOT NULL, 
	[Description] NVARCHAR(500) NOT NULL, 
	[CreatedByUserId] INT NULL,
	[StartDateTime] DATETIME NULL,
	[EndDateTime] DATETIME NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_BidTasks_Users] FOREIGN KEY ([CreatedByUserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_BidTasks_TenderTasks] FOREIGN KEY ([ParentBidTaskkId]) REFERENCES [BidTasks]([Id]),
	CONSTRAINT [FK_BidTasks_Bids] FOREIGN KEY ([BidId]) REFERENCES [Bids]([Id])
)
