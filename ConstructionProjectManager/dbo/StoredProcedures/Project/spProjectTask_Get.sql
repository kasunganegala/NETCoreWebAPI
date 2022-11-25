CREATE PROCEDURE [dbo].[spProjectTask_Get]
	@ProjectId INT
	,@TaskId INT
AS
BEGIN
	SELECT 
       PT.[Id]
      ,PT.[ProjectId]
      ,PT.[TaskId]
      ,PT.[ParentTaskId]
      ,PT.[Task]
      ,PT.[CreatedByUsername]
      ,PT.[ActualStartDateTime] AS [StartDateTime]
      ,PT.[ActualEndDateTime] AS [EndDateTime]
      ,PT.[CreatedDateTime]
      ,PT.[LastModifiedDateTime]
      ,PT.[Status]
      ,PT.[MaterialCost]
      ,PT.[LabourCost]
      ,PT.[EquipmentCost]
      ,PT.[TotalCost]
      ,P.Name AS [ProjectName]

    FROM [dbo].[ProjectTasks] PT
        LEFT JOIN [dbo].Projects AS P ON P.Id = PT.ProjectId
    WHERE PT.ProjectId = @ProjectId
        AND PT.Id = @TaskId



END
