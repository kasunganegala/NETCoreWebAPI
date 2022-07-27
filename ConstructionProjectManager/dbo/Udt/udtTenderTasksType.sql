CREATE TYPE [dbo].[udtTenderTasksType] AS TABLE
(
	[Id] INT NULL, 
	[TenderId] INT NULL, 
	[ParentTenderTaskId] INT NULL, 
	[Name] NVARCHAR(20) NULL, 
	[Description] NVARCHAR(500) NULL,
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NULL,
    [LastModifiedDateTime] DATETIME NULL
)
