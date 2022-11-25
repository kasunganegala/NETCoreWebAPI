CREATE PROCEDURE [dbo].[spProjectLabours_Update]
	@ProjectId INT,
	@ProjectLabours udtProjectLaboursType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		UPDATE PL
		SET
			 [LabourId] = IIF(IncomingPL.[LabourId] IS NOT NULL, IncomingPL.[LabourId], PL.[LabourId])
			,[Name] = IIF(IncomingPL.[Name] IS NOT NULL, IncomingPL.[Name], PL.[Name])
			,[UOMId] = IIF(IncomingPL.[UOMId] IS NOT NULL, IncomingPL.[UOMId], PL.[UOMId])
			,[UnitCost] = IIF(IncomingPL.[UnitCost] IS NOT NULL, IncomingPL.[UnitCost], PL.[UnitCost])
			
			,[LastModifiedDateTime] = IIF(IncomingPL.[LastModifiedDateTime] IS NOT NULL, IncomingPL.[LastModifiedDateTime], PL.[LastModifiedDateTime])
			,[IsDeleted] = IIF(IncomingPL.[Name] IS NOT NULL, PL.[IsDeleted], 1)

		FROM ProjectLabours PL
			LEFT JOIN @ProjectLabours AS IncomingPL ON IncomingPL.Id = PL.Id AND IncomingPL.Id != 0
		WHERE PL.ProjectId = @ProjectId

		INSERT INTO ProjectLabours(
			[ProjectId]
			,[LabourId]
			,[Name]
			,[UOMId]
			,[EstimatedUnitCost]
			,[EstimatedQuantity]
			,[EstimatedTotalCost]
			,[UnitCost]
			,[Quantity]
			,[TotalCost]
			,[Profit]
			,[CreatedByUsername]
			,[CreatedDateTime]
			)
		SELECT 
			 @ProjectId
			,[LabourId]
			,[Name]
			,[UOMId]
			,[EstimatedUnitCost]
			,[EstimatedQuantity]
			,[EstimatedTotalCost]
			,[UnitCost]
			,0
			,0
			,[Profit]
			,[CreatedByUsername]
			,[CreatedDateTime]
		FROM @ProjectLabours IncomingPL
		WHERE IncomingPL.Id = 0

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
