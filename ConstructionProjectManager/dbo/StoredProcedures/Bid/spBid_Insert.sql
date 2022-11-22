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
	@MaterialCostTotal NVARCHAR(MAX),
	@EquipmentCostTotal NVARCHAR(MAX),
	@LabourCostTotal NVARCHAR(MAX),
	@Tax NVARCHAR(MAX),
	@CostTotal NVARCHAR(MAX),
	@MaterialsProfit NVARCHAR(MAX),
	@EquipmentsProfit NVARCHAR(MAX),
	@LaboursProfit NVARCHAR(MAX),
	@ProfitTotal NVARCHAR(MAX),

	@BidTasks udtBidTasksType READONLY,	
	@Materials udtBidMaterialsType READONLY,	
	@Equipments udtBidEquipmentsType READONLY,	
	@Labours udtBidLaboursType READONLY	
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION

		INSERT INTO Bids([TenderId] ,[Name] ,[ContractorId] ,[StartDateTime] ,[EndDateTime] ,[IsSubmitted] ,[Status] ,[Comment] ,[CreatedByUsername] ,MaterialCostTotal ,EquipmentCostTotal ,LabourCostTotal ,Tax ,CostTotal ,MaterialsProfit ,EquipmentsProfit ,LaboursProfit ,ProfitTotal)
		VALUES(
			@TenderId
			,CONCAT('Contractor : ' , CAST(@ContractorId AS VARCHAR))
			,@ContractorId
			,@StartDateTime
			,@EndDateTime
			,@IsSubmitted
			,@Status
			,@Comment
			,@CreatedByUsername
			,@MaterialCostTotal
			,@EquipmentCostTotal
			,@LabourCostTotal
			,@Tax
			,@CostTotal
			,@MaterialsProfit
			,@EquipmentsProfit
			,@LaboursProfit
			,@ProfitTotal)

			DECLARE @BidId AS INT = SCOPE_IDENTITY();

			UPDATE b
			SET b.[Name] = CONCAT('B', FORMAT(@BidId, '0000'), ' ', con.Name)
			FROM Bids b
				INNER JOIN Contractors AS con ON con.Id = b.ContractorId
			WHERE b.Id = @BidId

			INSERT INTO BidTasks( [BidId], [TaskId], [ParentTaskId], [Task], [CreatedByUsername], [StartDateTime], [EndDateTime])
			SELECT 
				@BidId
				,[TaskId]
				,[ParentTaskId]
				,IIF([Task] IS NULL, '', [Task])
				,IIF([CreatedByUsername] IS NULL, '', [CreatedByUsername])
				,[StartDateTime]
				,[EndDateTime]
			FROM @BidTasks

			INSERT INTO BidEquipments ([BidId], [EquipmentId], [Name], [UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime])
			SELECT @BidId ,
				IIF([EquipmentId] IS NULL, 0, [EquipmentId]), 
				IIF([Name] IS NULL, '', [Name]), 
				[UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime]
			FROM @Equipments

			INSERT INTO BidMaterials([BidId], [MaterialId], [Name], [UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime])
			SELECT @BidId ,
				IIF([MaterialId] IS NULL, 0, [MaterialId]), 
				IIF([Name] IS NULL, '', [Name]), 
				[UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime]
			FROM @Materials

			INSERT INTO BidLabours([BidId], [LabourId], [Name], [UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime])
			SELECT @BidId ,
				IIF([LabourId] IS NULL, 0, [LabourId]), 
				IIF([Name] IS NULL, '', [Name]), 
				[UnitCost], [UOMId], [Quantity], [Profit], [TotalCost], [CreatedByUsername], [CreatedDateTime]
			FROM @Labours

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
