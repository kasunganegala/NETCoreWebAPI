CREATE TABLE [dbo].[ProjectTasksWorklogs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL,
	[TaskId] INT NOT NULL,

	[MaterialCost] DECIMAL(18, 2) NOT NULL, 
	[LabourCost] DECIMAL(18, 2) NOT NULL, 
	[EquipmentCost] DECIMAL(18, 2) NOT NULL, 
	[TotalCost] DECIMAL(18, 2) NOT NULL, 

	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)
