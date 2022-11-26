CREATE TYPE [dbo].[udtProjectTaskEquipmentUsageType] AS TABLE
(
	[Id] INT NULL, 
	[EquipmentId] INT NULL,
	[Name] NVARCHAR(MAX) NULL, 
	[Quantity] DECIMAL(18, 2) NULL, 
	[UOM] NVARCHAR(MAX) NULL, 
	[UOMId] NVARCHAR(MAX) NULL,
	[ProjectId] INT NULL,
	[TaskId] INT NULL

)
