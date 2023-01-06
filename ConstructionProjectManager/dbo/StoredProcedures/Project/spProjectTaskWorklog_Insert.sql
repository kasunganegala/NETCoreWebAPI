CREATE PROCEDURE [dbo].[spProjectTaskWorklog_Insert]
	@ProjectId INT
	,@TaskId INT
	,@LogDate DATETIME
	,@Comment NVARCHAR(MAX)
	,@Equipments udtProjectTaskEquipmentUsageType READONLY
	,@Materials udtProjectTaskMaterialUsageType READONLY
	,@Labours udtProjectTaskLabourUsageType READONLY
AS
BEGIN

     DECLARE @EquipmentCost AS INT = 0;
     DECLARE @MaterialCost AS INT = 0;
     DECLARE @LabourCost AS INT = 0;

 
	INSERT INTO ProjectTasksWorklogs
	(
      [ProjectId]
      ,[TaskId]
	  ,[LogDate] 
	  ,[Comment] 
    ) VALUES (@ProjectId, @TaskId, @LogDate, @Comment)
    DECLARE @ProjectTasksWorklogsId AS INT = SCOPE_IDENTITY();

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
        ,@ProjectTasksWorklogsId
        ,M.EquipmentId
        ,M.[Name]
        ,ISNULL(PM.[UOMId],0)
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Equipments M
        LEFT JOIN [ProjectEquipments] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name

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
        ,@ProjectTasksWorklogsId
        ,M.[MaterialId]
        ,M.[Name]
        ,ISNULL(PM.[UOMId],0)
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Materials M
        LEFT JOIN [ProjectMaterials] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name

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
        ,@ProjectTasksWorklogsId
        ,M.LabourId
        ,M.[Name]
        ,ISNULL(PM.[UOMId],0)
        ,PM.UnitCost
        ,M.Quantity
        ,CAST(GETUTCDATE() AS DATE)
        ,PM.UnitCost * M.Quantity
    FROM @Labours M
        LEFT JOIN [ProjectLabours] PM ON PM.ProjectId = M.ProjectId AND PM.Name = M.Name
	
    SELECT @EquipmentCost = SUM(TotalCost)
    FROM ProjectTasksWorklogEquipments
    WHERE WorklogId = @ProjectTasksWorklogsId
    GROUP BY WorklogId

    SELECT @MaterialCost = SUM(TotalCost)
    FROM ProjectTasksWorklogMaterials
    WHERE WorklogId = @ProjectTasksWorklogsId
    GROUP BY WorklogId

    SELECT @LabourCost = SUM(TotalCost)
    FROM ProjectTasksWorklogLabours
    WHERE WorklogId = @ProjectTasksWorklogsId
    GROUP BY WorklogId

    UPDATE ProjectTasksWorklogs
    SET EquipmentCost = @EquipmentCost,
        MaterialCost = @MaterialCost,
        LabourCost = @LabourCost,
        TotalCost = @EquipmentCost + @MaterialCost + @LabourCost
    WHERE @ProjectTasksWorklogsId = @ProjectTasksWorklogsId

    SELECT CAST(1 AS BIT) [Status]

END
