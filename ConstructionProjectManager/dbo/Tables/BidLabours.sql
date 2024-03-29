﻿CREATE TABLE [dbo].[BidLabours]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[BidId] INT NOT NULL,
	[LabourId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
	[UnitCost] DECIMAL(18, 2) NOT NULL, 
	[UOMId] NVARCHAR(MAX) NOT NULL, 
	[Quantity] DECIMAL(18, 2) NOT NULL, 
	[Profit] DECIMAL(18, 2) NOT NULL, 
	[TotalCost] DECIMAL(18, 2) NOT NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_BidLabours_Bids] FOREIGN KEY ([BidId]) REFERENCES [Bids]([Id])
)
