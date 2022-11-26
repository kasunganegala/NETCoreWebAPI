CREATE TYPE [dbo].[udtProjectTaskMaterialUsageType] AS TABLE
(
	[Id] INT NULL, 
	[MaterialId] INT NULL,
	[Name] NVARCHAR(MAX) NULL, 
	[Quantity] DECIMAL(18, 2) NULL, 
	[UOM] NVARCHAR(MAX) NULL, 
	[UOMId] NVARCHAR(MAX) NULL,
	[ProjectId] INT NULL,
	[TaskId] INT NULL

)
