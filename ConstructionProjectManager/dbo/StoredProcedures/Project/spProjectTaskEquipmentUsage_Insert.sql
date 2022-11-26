CREATE PROCEDURE [dbo].[spProjectTaskEquipmentUsage_Insert]
	@ProjectId INT
	,@TaskId INT
	,@Equipments udtProjectTaskEquipmentUsageType READONLY
AS
BEGIN
	INSERT INTO ProjectTasksWorklogEquipments
	(
      [ProjectId]
      ,[TaskId]
      ,[WorklogId]
      ,EquipmentId
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
        ,M.EquipmentId
        ,M.[Name]
        ,M.[UOMId]
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Equipments M
        LEFT JOIN [ProjectEquipments] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name
	
    SELECT CAST(1 AS BIT) [Status]

END
