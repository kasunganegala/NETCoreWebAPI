CREATE PROCEDURE [dbo].[spProjectComplete_Get]
	@Id INT
AS
BEGIN
	
	UPDATE dbo.Projects
		SET Status = 'Completed'
			,StartDateTime = IIF(StartDateTime IS NULL, GETUTCDATE(), StartDateTime)
	WHERE Id = @Id


  SELECT CAST(1 AS BIT) [Status]

END
