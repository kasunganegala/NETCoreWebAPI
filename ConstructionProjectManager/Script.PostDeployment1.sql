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


GO
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

GO
IF NOT EXISTS(SELECT 1 FROM dbo.Customers)
BEGIN
INSERT INTO Customers([Name],[Description]) VALUES 
    ('Priyadarshana Importers','Paint importing company')
   ,('Multi Kitchen','Kitchen equipment importers and distributors')
END

GO
IF NOT EXISTS(SELECT 1 FROM dbo.Users)
BEGIN
INSERT INTO Users([UserName],[FirstName],[LastName],[Email],[Password],[UserRoleId],[IsDeactivated], [CustomerId]) VALUES 
    ('kasunganegala','Kasun','Ganegala','a@a.com','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1,0, null)
   ,('malithchanaka','Malith','Chanaka','b@b.com','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',2,0, 1)
END

GO
IF NOT EXISTS(SELECT 1 FROM dbo.ProjectType)
BEGIN
INSERT INTO ProjectType([Name],[Description]) VALUES 
    (N'Time & Meterial','Cost is done as per the spent time and resources')
   ,(N'Fixed Budget','Cost is done as per the pre agreed costing agreement')
END

GO
IF NOT EXISTS(SELECT 1 FROM dbo.TenderType)
BEGIN
INSERT INTO TenderType([Name],[Description]) VALUES 
    ('Type-A Construction Companies','Type-A Construction Companies')
   ,('All','All Construction Companies')
END
