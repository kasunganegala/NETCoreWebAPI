CREATE PROCEDURE [dbo].[spTenderClose_Get]
	@Id INT
AS
BEGIN
	UPDATE dbo.[Tenders]
	SET Status = 'Close'
	WHERE Id = @Id
END
