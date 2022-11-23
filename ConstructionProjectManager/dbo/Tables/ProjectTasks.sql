CREATE TABLE [dbo].[ProjectTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL,
	[TaskId] INT NOT NULL,
	[ParentTaskId] INT NULL, 
	[Task] NVARCHAR(MAX) NOT NULL, 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[StartDateTime] DATETIME NULL,
	[EndDateTime] DATETIME NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_ProjectTasks_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
)
