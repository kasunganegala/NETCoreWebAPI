CREATE PROCEDURE [dbo].[spProjectTaskEquipments_Get]
	@ProjectId INT
	,@TaskId INT
AS
BEGIN
	
	SELECT PTWlogE.Name, PTWlogE.UOMId, PTWlogE.UnitCost, SUM(PTWlogE.TotalCost) AS [CostTotal], SUM(PTWlogE.Quantity) AS [Quantity]
	FROM [dbo].[ProjectTasksWorklogs] PTWlog
		INNER JOIN [dbo].[ProjectTasksWorklogEquipments] PTWlogE ON PTWlogE.WorklogId = PTWlog.Id
	WHERE PTWlog.TaskId = @TaskId AND PTWlog.ProjectId = @ProjectId
	GROUP BY PTWlogE.Name , PTWlogE.UOMId, PTWlogE.UnitCost



END
