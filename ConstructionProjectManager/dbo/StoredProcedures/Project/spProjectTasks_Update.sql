CREATE PROCEDURE [dbo].[spProjectTasks_Update]
	@ProjectId INT,
	@ProjectTasks udtProjectTasksType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		UPDATE PT
		SET
			 [TaskId] = IIF(IncomingPT.[TaskId] IS NOT NULL, IncomingPT.[TaskId], PT.[TaskId])
			,[ParentTaskId] = IIF(IncomingPT.[ParentTaskId] IS NOT NULL, IncomingPT.[ParentTaskId], 0)
			,[Task] = IIF(IncomingPT.[Task] IS NOT NULL, IncomingPT.[Task], PT.[Task])
			
			,[StartDateTime] = IIF(IncomingPT.[StartDateTime] IS NOT NULL, IncomingPT.[StartDateTime], PT.[StartDateTime])
			,[EndDateTime] = IIF(IncomingPT.[EndDateTime] IS NOT NULL, IncomingPT.[EndDateTime], PT.[EndDateTime])
			
			,[LastModifiedDateTime] = IIF(IncomingPT.[LastModifiedDateTime] IS NOT NULL, IncomingPT.[LastModifiedDateTime], PT.[LastModifiedDateTime])
			,[IsDeleted] = IIF(IncomingPT.[TaskId] IS NOT NULL, PT.[IsDeleted], 1)

		FROM ProjectTasks PT
			LEFT JOIN @ProjectTasks AS IncomingPT ON IncomingPT.Id = PT.Id AND IncomingPT.Id != 0
		WHERE PT.ProjectId = @ProjectId

		INSERT INTO ProjectTasks([ProjectId], [TaskId], [ParentTaskId], [Task], [CreatedByUsername], [StartDateTime], [EndDateTime], [CreatedDateTime])
		SELECT 
			@ProjectId,
			[TaskId], 
			[ParentTaskId], 
			[Task], 
			[CreatedByUsername], 
			[StartDateTime], 
			[EndDateTime], 
			[CreatedDateTime]
		FROM @ProjectTasks IncomingPT
		WHERE IncomingPT.Id = 0

		SELECT CAST(1 AS BIT) AS [IsProcessed]

	COMMIT TRAN -- Transaction Success!
END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
				ROLLBACK TRAN --RollBack in case of Error

			-- <EDIT>: From SQL2008 on, you must raise error messages as follows:
			DECLARE @ErrorMessage NVARCHAR(4000);  
			DECLARE @ErrorSeverity INT;  
			DECLARE @ErrorState INT;  

			SELECT   
			   @ErrorMessage = ERROR_MESSAGE(),  
			   @ErrorSeverity = ERROR_SEVERITY(),  
			   @ErrorState = ERROR_STATE();  

			RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);  
			-- </EDIT>
	END CATCH
END
