CREATE PROCEDURE [dbo].[spBidTasks_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[BidTasks] b
	WHERE b.[BidId] = @Id
END
