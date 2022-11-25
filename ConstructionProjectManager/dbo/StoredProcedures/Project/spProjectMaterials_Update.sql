CREATE PROCEDURE [dbo].[spProjectMaterials_Update]
	@ProjectId INT,
	@ProjectMaterials udtProjectMaterialsType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		UPDATE PM
		SET
			 [MaterialId] = IIF(IncomingPM.[MaterialId] IS NOT NULL, IncomingPM.[MaterialId], PM.[MaterialId])
			,[Name] = IIF(IncomingPM.[Name] IS NOT NULL, IncomingPM.[Name], PM.[Name])
			,[UOMId] = IIF(IncomingPM.[UOMId] IS NOT NULL, IncomingPM.[UOMId], PM.[UOMId])
			,[UnitCost] = IIF(IncomingPM.[UnitCost] IS NOT NULL, IncomingPM.[UnitCost], PM.[UnitCost])
			
			,[LastModifiedDateTime] = IIF(IncomingPM.[LastModifiedDateTime] IS NOT NULL, IncomingPM.[LastModifiedDateTime], PM.[LastModifiedDateTime])
			,[IsDeleted] = IIF(IncomingPM.[Name] IS NOT NULL, PM.[IsDeleted], 1)

		FROM ProjectMaterials PM
			LEFT JOIN @ProjectMaterials AS IncomingPM ON IncomingPM.Id = PM.Id AND IncomingPM.Id != 0
		WHERE PM.ProjectId = @ProjectId

		INSERT INTO ProjectMaterials(
			[ProjectId]
			,[MaterialId]
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
			,[MaterialId]
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
		FROM @ProjectMaterials IncomingPM
		WHERE IncomingPM.Id = 0

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
