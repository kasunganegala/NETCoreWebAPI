/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



IF NOT EXISTS(SELECT 1 FROM UserRoles)
BEGIN
    INSERT INTO UserRoles(RoleName) VALUES 
    ('ProjectManager')
    ,('Client')
    ,('Contractor')
    ,('Worker')
    ,('Supervisor')
    ,('SubContractor')
END

IF NOT EXISTS(SELECT 1 FROM dbo.Users)
BEGIN
INSERT INTO Users([UserName],[FirstName],[LastName],[Email],[Password],[UserRoleId],[IsDeactivated]) VALUES 
    ('kasunganegala','Kasun','Ganegala','a@a.com','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1,0)
   ,('malithchanaka','Malith','Chanaka','b@b.com','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',2,0)
END