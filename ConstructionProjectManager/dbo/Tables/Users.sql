CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(20) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(100) NOT NULL,
    [UserRoleId] INT NOT NULL,
    [IsDeactivated] BIT NOT NULL DEFAULT 0, 
    [CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL, 

    CONSTRAINT [FK_User_UserRoles] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRoles]([Id])
)
