CREATE TABLE [dbo].[BidsResourcesCost]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[BidId] INT NOT NULL, 
	[BidTaskId] INT NULL, 
	[Name] NVARCHAR(20) NOT NULL, 
	[Description] NVARCHAR(500) NOT NULL, 
	[CreatedByUserId] INT NULL,
	[Quantity] INT NOT NULL,
	[UnitCost] INT NOT NULL,
	[TotalCost] INT NOT NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_BidsResourcesCost_Users] FOREIGN KEY ([CreatedByUserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_BidsResourcesCost_Bids] FOREIGN KEY ([BidId]) REFERENCES [Bids]([Id])
)
