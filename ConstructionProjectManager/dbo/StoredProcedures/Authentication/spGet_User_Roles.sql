CREATE PROCEDURE [dbo].[spGet_User_Roles]
	@Id nvarchar(50)
AS
BEGIN
	SELECT ur.*
	FROM dbo.Users u
        INNER JOIN dbo.UserRoles AS ur ON ur.Id = u.UserRoleId
	WHERE u.Id = @Id
END
