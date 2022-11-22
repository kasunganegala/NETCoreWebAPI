CREATE TYPE [dbo].[udtBidLaboursType] AS TABLE
(
	[Id] INT  NULL, 
	[BidId] INT  NULL,
	[LabourId] INT  NULL,
	[Name] NVARCHAR(MAX)  NULL, 
	[UnitCost] NVARCHAR(MAX)  NULL, 
	[UOMId] NVARCHAR(MAX)  NULL, 
	[Quantity] NVARCHAR(MAX)  NULL, 
	[Profit] NVARCHAR(MAX)  NULL, 
	[TotalCost] NVARCHAR(MAX)  NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME  NULL, 
	[LastModifiedDateTime] DATETIME NULL
)
