CREATE TYPE [dbo].[udtProjectTaskLabourUsageType] AS TABLE
(
	[Id] INT NULL, 
	[LabourId] INT NULL,
	[Name] NVARCHAR(MAX) NULL, 
	[Quantity] DECIMAL(18, 2) NULL, 
	[UOM] NVARCHAR(MAX) NULL, 
	[UOMId] NVARCHAR(MAX) NULL,
	[ProjectId] INT NULL,
	[TaskId] INT NULL

)
