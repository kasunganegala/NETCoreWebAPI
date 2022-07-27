CREATE TABLE [dbo].[TenderTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TenderId] INT NOT NULL, 
	[ParentTenderTaskId] INT NULL, 
	[Name] NVARCHAR(20) NOT NULL, 
	[Description] NVARCHAR(500) NOT NULL, 
	[CreatedByUsername] NVARCHAR(20) NOT NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	--CONSTRAINT [FK_TenderTasks_Users] FOREIGN KEY ([CreatedByUsername]) REFERENCES [Users]([UserName]),
	--CONSTRAINT [FK_TenderTasks_TenderTasks] FOREIGN KEY ([ParentTenderTaskId]) REFERENCES [TenderTasks]([Id]),
	CONSTRAINT [FK_TenderTasks_Tenders] FOREIGN KEY ([TenderId]) REFERENCES [Tenders]([Id])
)
