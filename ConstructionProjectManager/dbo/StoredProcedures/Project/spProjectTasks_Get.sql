CREATE PROCEDURE [dbo].[spBidTasks_Get]
	@Id INT
AS
BEGIN
	SELECT 
		[Id]
      ,[BidId]
      ,[TaskId]
      ,[ParentTaskId]
      ,[Task]
      ,[CreatedByUsername]
      ,[StartDateTime] AS [StartDate]
      ,[EndDateTime] AS [EndDate]
      ,[CreatedDateTime]
      ,[LastModifiedDateTime]
	FROM dbo.[BidTasks] b
	WHERE b.[BidId] = @Id
END
