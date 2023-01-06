﻿CREATE TABLE [dbo].[ProjectTasksWorklogs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL,
	[TaskId] INT NOT NULL,

	[LogDate] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
	[Comment] NVARCHAR(MAX) NOT NULL DEFAULT '',
	
	[MaterialCost] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
	[LabourCost] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
	[EquipmentCost] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
	[TotalCost] DECIMAL(18, 2) NOT NULL DEFAULT 0, 

	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)
