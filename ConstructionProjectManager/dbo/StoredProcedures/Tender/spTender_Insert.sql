﻿CREATE PROCEDURE [dbo].[spTender_Insert]
	@Id INT,
	@Name NVARCHAR(20),
	@Description NVARCHAR(500),
	@TenderType INT,
	@StartDateTime DATETIME,
	@EndDateTime DATETIME,
	@CustomerId INT,
	@Status NVARCHAR(4),
	@ProjectType INT,
	@ProjectBudget INT,
	@Comment NVARCHAR(500),
	@CreatedByUsername NVARCHAR(20),
	@CreatedDateTime DATETIME,
	@LastModifiedDateTime DATETIME,
	@TenderTasks udtTenderTasksType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		INSERT INTO Tenders	(
			[Name] 
			,[Description] 
			,[TenderType]
			,[StartDateTime]
			,[EndDateTime]
			,[CustomerId]
			,[Status] 
			,[ProjectType]
			,[ProjectBudget]
			,[Comment]
			,[CreatedByUsername])
		VALUES(
			IIF(@Name IS NULL, '', @Name)
			,IIF(@Description IS NULL, '', @Description)
			,@TenderType
			,@StartDateTime 
			,@EndDateTime
			,@CustomerId
			,@Status 
			,@ProjectType 
			,@ProjectBudget
			,@Comment 
			,@CreatedByUsername)

			DECLARE @TenderId AS INT = SCOPE_IDENTITY();

			UPDATE t
			SET t.[Name] = CONCAT('T', FORMAT(@TenderId, '0000'))
			FROM Tenders t
			INNER JOIN Customers AS c ON c.Id = t.CustomerId
			WHERE t.Id = @TenderId

			INSERT INTO TenderTasks(
				 [TenderId] 
				,[TaskId]
				,[ParentTaskId]
				,[Task]
				,[CreatedByUsername]
				,[StartDateTime]
				,[EndDateTime])
			SELECT 
				 @TenderId
				,[TaskId]
				,[ParentTaskId]
				,IIF([Task] IS NULL, '', [Task]) 
				,IIF([CreatedByUsername] IS NULL, '', [CreatedByUsername])
				,[StartDate]
				,[EndDate]
			FROM @TenderTasks

			SELECT @TenderId 

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
