﻿CREATE TABLE [dbo].[UserRoles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[RoleName] NVARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(500) NULL, 
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL
)
