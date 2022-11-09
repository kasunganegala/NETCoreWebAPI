CREATE TABLE [dbo].[TenderTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TenderId] INT NOT NULL, 
	[TaskId] INT NULL, 
	[ParentTaskId] INT NULL, 
	[Task] NVARCHAR(MAX) NOT NULL, 
	[StartDateTime] DATETIME NULL, 
	[EndDateTime] DATETIME NULL, 
	[CreatedByUsername] NVARCHAR(100) NOT NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	--CONSTRAINT [FK_TenderTasks_Users] FOREIGN KEY ([CreatedByUsername]) REFERENCES [Users]([UserName]),
	--CONSTRAINT [FK_TenderTasks_TenderTasks] FOREIGN KEY ([ParentTenderTaskId]) REFERENCES [TenderTasks]([Id]),
	CONSTRAINT [FK_TenderTasks_Tenders] FOREIGN KEY ([TenderId]) REFERENCES [Tenders]([Id])
)
