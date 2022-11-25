CREATE PROCEDURE [dbo].[spProjectStart_Get]
	@Id INT
AS
BEGIN
	
	UPDATE dbo.Projects
		SET Status = 'Started'
			,StartDateTime = IIF(StartDateTime IS NULL, GETUTCDATE(), StartDateTime)
	WHERE Id = @Id


  SELECT CAST(1 AS BIT) [Status]

END
