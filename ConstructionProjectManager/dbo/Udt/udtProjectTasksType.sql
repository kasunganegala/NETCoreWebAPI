CREATE TYPE [dbo].[udtProjectTasksType] AS TABLE
(
	[Id] INT NULL, 
	[ProjectId] INT NULL, 
	[TaskId] INT NULL, 
	[ParentTaskId] INT NULL, 
	[Task] NVARCHAR(MAX) NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[StartDateTime] DATETIME NULL,
	[EndDateTime] DATETIME NULL,
	[CreatedDateTime] DATETIME NULL,
    [LastModifiedDateTime] DATETIME NULL,
	[Status] NVARCHAR(MAX) NULL
)
