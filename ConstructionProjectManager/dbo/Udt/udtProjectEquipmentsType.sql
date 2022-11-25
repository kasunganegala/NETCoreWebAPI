CREATE TYPE [dbo].[udtProjectEquipmentsType] AS TABLE
(
	[Id] INT NULL, 
	[ProjectId] INT NULL,
	[EquipmentId] INT NULL,
	[Name] NVARCHAR(MAX) NULL, 
	[UOMId] NVARCHAR(MAX) NULL, 

	[EstimatedUnitCost] DECIMAL(18, 2) NULL, 
	[EstimatedQuantity] DECIMAL(18, 2) NULL, 
	[EstimatedTotalCost] DECIMAL(18, 2) NULL,

	[UnitCost] DECIMAL(18, 2) NULL, 
	[Quantity] DECIMAL(18, 2) NULL, 
	[TotalCost] DECIMAL(18, 2) NULL,

	[Profit] DECIMAL(18, 2) NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NULL, 
	[LastModifiedDateTime] DATETIME NULL,
	[IsDeleted] BIT NULL
)
