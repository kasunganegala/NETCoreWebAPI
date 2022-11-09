CREATE TYPE [dbo].[udtTenderTasksType] AS TABLE
(
	[TenderId] INT NULL, 
	[TaskId] INT NULL, 
	[ParentTaskId] INT NULL, 
	[Task] NVARCHAR(MAX) NULL, 
	[StartDate] DATETIME NULL,
	[EndDate] DATETIME NULL,
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NULL,
    [LastModifiedDateTime] DATETIME NULL
)
