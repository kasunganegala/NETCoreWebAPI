CREATE PROCEDURE [dbo].[spTenderHold_Get]
	@Id INT
AS
BEGIN
	UPDATE dbo.[Tenders]
	SET Status = 'On-Hold'
	WHERE Id = @Id

END
