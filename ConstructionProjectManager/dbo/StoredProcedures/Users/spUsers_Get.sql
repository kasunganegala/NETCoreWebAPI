CREATE PROCEDURE [dbo].[spUsers_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[Users] u
	WHERE u.Id = @Id
END
