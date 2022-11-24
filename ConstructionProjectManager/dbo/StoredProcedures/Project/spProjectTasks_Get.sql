CREATE PROCEDURE [dbo].[spProjectTasks_Get]
	@Id INT
AS
BEGIN
	SELECT 
	   [Id]
      ,[ProjectId]
      ,[TaskId]
      ,[ParentTaskId]
      ,[Task]
      ,[CreatedByUsername]
      ,[StartDateTime]  AS [StartDate]
      ,[EndDateTime] AS [EndDate]
      ,[CreatedDateTime]
      ,[LastModifiedDateTime]
      ,[Status]
      ,[IsDeleted]
	FROM dbo.[ProjectTasks] pt
	WHERE pt.ProjectId = @Id
        AND IsDeleted = 0
END
