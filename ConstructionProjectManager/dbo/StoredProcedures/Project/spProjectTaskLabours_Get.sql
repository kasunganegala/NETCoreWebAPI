CREATE PROCEDURE [dbo].[spProjectTaskLabours_Get]
	@ProjectId INT
	,@TaskId INT
AS
BEGIN
	--SELECT PTWlogE.Name, PTWlogE.UOMId, PTWlogE.UnitCost, SUM(PTWlogE.TotalCost) AS [CostTotal], SUM(PTWlogE.Quantity) AS [Quantity]
	--FROM [dbo].[ProjectTasksWorklogs] PTWlog
	--	INNER JOIN [dbo].[ProjectTasksWorklogLabours] PTWlogE ON PTWlogE.WorklogId = PTWlog.Id
	--WHERE PTWlog.TaskId = @TaskId AND PTWlog.ProjectId = @ProjectId
	--GROUP BY PTWlogE.Name , PTWlogE.UOMId, PTWlogE.UnitCost

	SELECT PTWlogM.Name, PTWlogM.UOMId, PTWlogM.UnitCost, SUM(PTWlogM.TotalCost) AS [CostTotal], SUM(PTWlogM.Quantity) AS [Quantity]
	FROM [dbo].[ProjectTasksWorklogLabours] PTWlogM
	WHERE PTWlogM.ProjectId = @ProjectId
		AND PTWlogM.TaskId = @TaskId
	GROUP BY PTWlogM.Name , PTWlogM.UOMId, PTWlogM.UnitCost



END
