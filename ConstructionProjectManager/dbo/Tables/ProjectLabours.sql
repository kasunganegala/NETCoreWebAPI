CREATE TABLE [dbo].[ProjectLabours]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL,
	[LabourId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
	[UOMId] NVARCHAR(MAX) NOT NULL, 

	[EstimatedUnitCost] DECIMAL(18, 2) NOT NULL, 
	[EstimatedQuantity] DECIMAL(18, 2) NOT NULL, 
	[EstimatedTotalCost] DECIMAL(18, 2) NOT NULL,

	[UnitCost] DECIMAL(18, 2) NOT NULL, 
	[Quantity] DECIMAL(18, 2) NOT NULL, 
	[TotalCost] DECIMAL(18, 2) NOT NULL,

	[Profit] DECIMAL(18, 2) NOT NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	[IsDeleted] BIT NOT NULL DEFAULT 0,

	CONSTRAINT [FK_ProjectLabours_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
)
