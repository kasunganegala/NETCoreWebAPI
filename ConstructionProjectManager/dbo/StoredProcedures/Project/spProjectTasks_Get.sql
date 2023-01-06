CREATE PROCEDURE [dbo].[spProjectTasks_Get]
	@Id INT
AS
BEGIN

    DECLARE @EffortTable TABLE 
    (
        Effort INT,
        TaskId INT
    )


    INSERT INTO @EffortTable (Effort,TaskId)
    SELECT SUM(ISNULL(Effort,0)) AS [Effort], TaskId
    FROM [ProjectTasksWorklogs] ptl 
    WHERE ptl.ProjectId = @Id
    GROUP BY [TaskId]





	SELECT 
	   [Id]
      ,[ProjectId]
      ,pt.[TaskId]
      ,[ParentTaskId]
      ,[Task]
      ,[CreatedByUsername]
      ,[StartDateTime]  AS [StartDate]
      ,[EndDateTime] AS [EndDate]
      ,[CreatedDateTime]
      ,[LastModifiedDateTime]
      ,[Status]
      ,[IsDeleted]
      ,ActualEndDateTime
      ,ActualStartDateTime
      ,DATEDIFF(day, [StartDateTime], [EndDateTime]) AS [EstimatedTaskDuration]
      ,IIF(ActualEndDateTime IS NOT NULL AND ActualStartDateTime IS NOT NULL, DATEDIFF(day, ActualEndDateTime, ActualStartDateTime),0) AS [ActualTaskDuration]
      ,IIF(Status != 'Not Started', ISNULL(ET.Effort,0), 0) AS [TaskProgress]
      ,IIF(Status != 'Not Started', (CAST(DATEDIFF(hour, [StartDateTime], [EndDateTime]) AS INT) - ET.Effort), 0) AS [RemaingTaskProgress]
      ,IIF(GETUTCDATE() > [EndDateTime], DATEDIFF(day, [EndDateTime], GETUTCDATE()),0) AS [TaskOverDueBy]
	FROM dbo.[ProjectTasks] pt
        LEFT JOIN @EffortTable AS ET ON  ET.TaskId = pt.Id
	WHERE pt.ProjectId = @Id
        AND IsDeleted = 0
END
