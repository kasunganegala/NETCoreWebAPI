CREATE TABLE [dbo].[Tenders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(100) NOT NULL, 
	[Description] NVARCHAR(500) NOT NULL, 
	[TenderType] INT NOT NULL ,
	[StartDateTime] DATETIME NOT NULL, 
	[EndDateTime] DATETIME NOT NULL, 
	[CustomerId] INT NOT NULL,
	[Status] NVARCHAR(4) DEFAULT 'Open' NOT NULL, 
	[ProjectType] INT NOT NULL ,
	[Comment] NVARCHAR(500) NULL DEFAULT '', 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,

	CONSTRAINT [FK_Tenders_Cstomers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([Id]),
	--CONSTRAINT [FK_Tenders_Users] FOREIGN KEY ([CreatedByUsername]) REFERENCES [Users]([UserName])
)
