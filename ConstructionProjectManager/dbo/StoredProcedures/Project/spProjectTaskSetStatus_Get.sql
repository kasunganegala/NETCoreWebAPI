CREATE PROCEDURE [dbo].[spProjectTaskSetStatus_Get]
	@ProjectId INT
	,@TaskId INT
	,@Status NVARCHAR(MAX)
AS
BEGIN
	UPDATE dbo.[ProjectTasks]
		SET Status = @Status
			,ActualStartDateTime = IIF(ActualStartDateTime IS NULL AND @Status = 'Started', GETUTCDATE(),ActualStartDateTime)
			,ActualEndDateTime = IIF(ActualEndDateTime IS NULL AND @Status = 'Completed', GETUTCDATE(),ActualEndDateTime)
	WHERE ProjectId = @ProjectId 
		AND Id = @TaskId
        AND IsDeleted = 0

		SELECT CAST(1 AS BIT) [status]
END
