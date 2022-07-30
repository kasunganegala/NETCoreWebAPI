CREATE PROCEDURE [dbo].[spTender_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[Tenders] u
	WHERE u.Id = @Id
END
