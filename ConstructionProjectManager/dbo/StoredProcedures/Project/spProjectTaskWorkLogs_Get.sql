CREATE PROCEDURE [dbo].[spProjectTaskWorkLogs_Get]
	@ProjectId INT
	,@TaskId INT
AS
BEGIN
	
	DECLARE @CostEquipment TABLE 
    (
        Cost INT,
        WorkLog INT

    )

    INSERT INTO @CostEquipment (Cost,WorkLog)
    SELECT SUM(we.TotalCost) AS [CostTotal], we.WorklogId
	FROM [dbo].[ProjectTasksWorklogEquipments] we
	WHERE we.ProjectId = @ProjectId
		AND we.TaskId = @TaskId
	GROUP BY we.WorklogId

	DECLARE @CostLabor TABLE 
    (
        Cost INT,
        WorkLog INT
    )

	INSERT INTO @CostLabor (Cost,WorkLog)
	SELECT SUM(wl.TotalCost) AS [CostTotal], wl.WorklogId
	FROM [dbo].[ProjectTasksWorklogLabours] wl
	WHERE wl.ProjectId = @ProjectId
		AND wl.TaskId = @TaskId
	GROUP BY wl.WorklogId

	DECLARE @CostMaterial TABLE 
    (
        Cost INT,
        WorkLog INT
    )

	INSERT INTO @CostMaterial (Cost,WorkLog)
	SELECT SUM(wm.TotalCost) AS [CostTotal], wm.WorklogId
	FROM [dbo].[ProjectTasksWorklogMaterials] wm
	WHERE wm.ProjectId = @ProjectId
		AND wm.TaskId = @TaskId
	GROUP BY wm.WorklogId



	SELECT 
		Comment,
		LogDate,
		Effort,
		CreatedDateTime,
		ISNULL(CM.Cost,0) AS [CostMaterial],
		ISNULL(CE.Cost,0) AS [CostEquipment],
		ISNULL(CL.Cost,0) AS [CostLabour]
	FROM [dbo].[ProjectTasksWorklogs] worklog
		LEFT JOIN @CostMaterial AS CM ON CM.WorkLog = worklog.Id
		LEFT JOIN @CostEquipment AS CE ON CE.WorkLog = worklog.Id
		LEFT JOIN @CostLabor AS CL ON CL.WorkLog = worklog.Id
	WHERE worklog.ProjectId = @ProjectId
		AND worklog.TaskId = @TaskId
	


END
