CREATE PROCEDURE [dbo].[spGet_User_Login]
	@Email nvarchar(50)
	,@Password nvarchar(100)
AS
BEGIN
	SELECT *
	FROM dbo.[Users] u
	WHERE u.Email = @Email
		AND u.Password = @Password
END
