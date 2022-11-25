CREATE TABLE [dbo].[ProjectTasksWorklogMaterials]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL,
	[TaskId] INT NOT NULL,
	[WorklogId] INT NOT NULL,
	[MaterialId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
	[UOMId] NVARCHAR(MAX) NOT NULL, 
	[UnitCost] DECIMAL(18, 2) NOT NULL, 
	[Quantity] DECIMAL(18, 2) NOT NULL, 
	[TotalCost] DECIMAL(18, 2) NOT NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)
