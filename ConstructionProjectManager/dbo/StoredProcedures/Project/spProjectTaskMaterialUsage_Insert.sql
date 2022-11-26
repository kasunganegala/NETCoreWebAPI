CREATE PROCEDURE [dbo].[spProjectTaskMaterialUsage_Insert]
	@ProjectId INT
	,@TaskId INT
	,@Materials udtProjectTaskMaterialUsageType READONLY
AS
BEGIN
	INSERT INTO ProjectTasksWorklogMaterials 
	(
      [ProjectId]
      ,[TaskId]
      ,[WorklogId]
      ,[MaterialId]
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
        ,M.[MaterialId]
        ,M.[Name]
        ,M.[UOMId]
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Materials M
        LEFT JOIN [ProjectMaterials] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name
	
    SELECT CAST(1 AS BIT) [Status]

END
