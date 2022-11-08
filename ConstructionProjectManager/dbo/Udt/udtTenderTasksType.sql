CREATE TYPE [dbo].[udtTenderTasksType] AS TABLE
(
	[Id] INT NULL, 
	[TenderId] INT NULL, 
	[ParentTenderTaskId] INT NULL, 
	[Task] NVARCHAR(20) NULL, 
	[StartDate] DATETIME NULL,
	[EndDate] DATETIME NULL,
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NULL,
    [LastModifiedDateTime] DATETIME NULL
)
