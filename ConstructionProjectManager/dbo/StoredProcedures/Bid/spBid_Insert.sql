CREATE PROCEDURE [dbo].[spBid_Insert]
	@Id INT,
	@TenderId INT,
	@ContractorId INT,
	@Name VARCHAR(MAX),
	@StartDateTime DATETIME,
	@EndDateTime DATETIME,
	@IsSubmitted BIT,
	@Status NVARCHAR(15),
	@Comment NVARCHAR(500),
	@CreatedByUsername NVARCHAR(20),
	@CreatedDateTime DATETIME,
	@LastModifiedDateTime DATETIME,
	@BidTasks udtBidTasksType READONLY	
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		INSERT INTO Bids(
			[TenderId]
			,[Name]
			,[ContractorId]
			,[StartDateTime]
			,[EndDateTime]
			,[IsSubmitted]
			,[Status]
			,[Comment]
			,[CreatedByUsername])
		VALUES(
			@TenderId
			,CONCAT('Contractor : ' , CAST(@ContractorId AS VARCHAR))
			,@ContractorId
			,@StartDateTime
			,@EndDateTime
			,@IsSubmitted
			,@Status
			,@Comment
			,@CreatedByUsername)

			DECLARE @BidId AS INT = SCOPE_IDENTITY();

			UPDATE b
			SET b.[Name] = CONCAT('B', FORMAT(@BidId, '0000'), ' ', con.Name)
			FROM Bids b
				INNER JOIN Contractors AS con ON con.Id = b.ContractorId
			WHERE b.Id = @BidId

			INSERT INTO BidTasks(
				[BidId],
				[TaskId],
				[ParentTaskkId], 
				[Task], 
				[CreatedByUsername],
				[StartDateTime],
				[EndDateTime])
			SELECT 
				@BidId
				,[TaskId]
				,[ParentTaskId]
				,IIF([Task] IS NULL, '', [Task]) 
				,IIF([CreatedByUsername] IS NULL, '', [CreatedByUsername])
				,[StartDateTime]
				,[EndDateTime]
			FROM @BidTasks

			SELECT @BidId 

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
