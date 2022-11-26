CREATE PROCEDURE [dbo].[spProjectTaskLabourUsage_Insert]
	@ProjectId INT
	,@TaskId INT
	,@Labours udtProjectTaskLabourUsageType READONLY
AS
BEGIN
	INSERT INTO ProjectTasksWorklogLabours
	(
      [ProjectId]
      ,[TaskId]
      ,[WorklogId]
      ,LabourId
      ,[Name]
      ,[UOMId]
      ,[UnitCost]
      ,[Quantity]
      ,[CreatedDateTime]
      ,[TotalCost]
	)
    SELECT 
        @ProjectId
        ,@TaskId
        ,0
        ,M.LabourId
        ,M.[Name]
        ,M.[UOMId]
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Labours M
        LEFT JOIN [ProjectLabours] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name
	
    SELECT CAST(1 AS BIT) [Status]

END
