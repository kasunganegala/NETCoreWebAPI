CREATE TYPE [dbo].[udtBidTasksType] AS TABLE
(
	[Id] INT NULL, 
	[BidId] INT NULL, 
	[TaskId] INT NULL, 
	[ParentTaskId] INT NULL, 
	[Task] NVARCHAR(MAX) NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[StartDateTime] DATETIME NULL,
	[EndDateTime] DATETIME NULL,
	[CreatedDateTime] DATETIME NULL,
    [LastModifiedDateTime] DATETIME NULL
)
