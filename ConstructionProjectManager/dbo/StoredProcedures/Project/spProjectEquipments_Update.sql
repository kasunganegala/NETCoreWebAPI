CREATE PROCEDURE [dbo].[spProjectEquipments_Update]
	@ProjectId INT,
	@ProjectEquipments udtProjectEquipmentsType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		UPDATE PE
		SET
			 [EquipmentId] = IIF(IncomingPE.[EquipmentId] IS NOT NULL, IncomingPE.[EquipmentId], PE.[EquipmentId])
			,[Name] = IIF(IncomingPE.[Name] IS NOT NULL, IncomingPE.[Name], PE.[Name])
			,[UOMId] = IIF(IncomingPE.[UOMId] IS NOT NULL, IncomingPE.[UOMId], PE.[UOMId])
			,[UnitCost] = IIF(IncomingPE.[UnitCost] IS NOT NULL, IncomingPE.[UnitCost], PE.[UnitCost])
			
			,[LastModifiedDateTime] = IIF(IncomingPE.[LastModifiedDateTime] IS NOT NULL, IncomingPE.[LastModifiedDateTime], PE.[LastModifiedDateTime])
			,[IsDeleted] = IIF(IncomingPE.[Name] IS NOT NULL, PE.[IsDeleted], 1)

		FROM ProjectEquipments PE
			LEFT JOIN @ProjectEquipments AS IncomingPE ON IncomingPE.Id = PE.Id AND IncomingPE.Id != 0
		WHERE PE.ProjectId = @ProjectId

		INSERT INTO ProjectEquipments(
			[ProjectId]
			,[EquipmentId]
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
			,[EquipmentId]
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
		FROM @ProjectEquipments IncomingPE
		WHERE IncomingPE.Id = 0

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
