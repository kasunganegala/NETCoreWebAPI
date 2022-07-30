CREATE PROCEDURE [dbo].[spTenderTasks_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[TenderTasks] u
	WHERE u.[TenderId] = @Id
END
