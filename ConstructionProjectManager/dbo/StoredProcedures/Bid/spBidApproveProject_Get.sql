CREATE PROCEDURE [dbo].[spBidApproveProject_Get]
	@Id INT
AS
BEGIN
	
	DECLARE @TenderId INT = 0
	
    SELECT @TenderId = TenderId FROM dbo.Bids WHERE Id = @Id

	UPDATE dbo.Bids
		SET Status = 'Rejected'
	WHERE Id != @Id
		AND TenderId = @TenderId

    UPDATE dbo.Bids
		SET Status = 'Accepted'
	WHERE Id = @Id

    UPDATE dbo.Tenders
        SET Status = 'Closed'
    WHERE id=@TenderId

	INSERT INTO Projects
       ([BidId]
      ,[ContractorId]
      ,[Name]

      ,[EstimatedStartDateTime]
      ,[EstimatedEndDateTime]
      ,[StartDateTime]
      ,[EndDateTime]

      ,[IsSubmitted]
      ,[Status]
      ,[Comment]
      ,[CreatedByUsername]
      
      ,[EstimatedMaterialCostTotal]
      ,[EstimatedEquipmentCostTotal]
      ,[EstimatedLabourCostTotal]
      ,[EstimatedTax]
      ,[EstimatedCostTotal]

      ,[MaterialCostTotal]
      ,[EquipmentCostTotal]
      ,[LabourCostTotal]
      ,[Tax]
      ,[CostTotal]

      ,[MaterialsProfit]
      ,[EquipmentsProfit]
      ,[LaboursProfit]
      ,[ProfitTotal])
      SELECT 
       b.[Id]
      ,b.[ContractorId]
      ,'Temp Name'

      ,b.[StartDateTime]
      ,b.[EndDateTime]
      ,b.[StartDateTime]
      ,b.[EndDateTime]

      ,0
      ,'New'
      ,''
      ,b.[CreatedByUsername]
      
      ,b.[MaterialCostTotal]
      ,b.[EquipmentCostTotal]
      ,b.[LabourCostTotal]
      ,b.[Tax]
      ,b.[CostTotal]

      ,0
      ,0
      ,0
      ,0
      ,0

      ,b.[MaterialsProfit]
      ,b.[EquipmentsProfit]
      ,b.[LaboursProfit]
      ,b.[ProfitTotal]
  FROM [dbo].[Bids] b
  WHERE b.Id = @Id
  DECLARE @ProjectId AS INT = SCOPE_IDENTITY();

  UPDATE p
    SET p.[Name] = CONCAT('P', FORMAT(@ProjectId, '0000'))
  FROM Projects p
  WHERE p.Id = @ProjectId

  INSERT INTO ProjectTasks
      (
        [ProjectId]
       ,[TaskId]
       ,[ParentTaskId]
       ,[Task]
       ,[CreatedByUsername]
       ,[StartDateTime]
       ,[EndDateTime]
      )
    SELECT 
        @ProjectId
       ,[TaskId]
       ,[ParentTaskId]
       ,[Task]
       ,[CreatedByUsername]
       ,[StartDateTime]
       ,[EndDateTime]
    FROM [dbo].[BidTasks]
    WHERE BidId = @Id

    INSERT INTO ProjectEquipments
    (
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
    )
    SELECT
       @ProjectId
      ,[EquipmentId]
      ,[Name]
      ,[UOMId]

      ,[UnitCost]
      ,[Quantity]
      ,[TotalCost]

      ,[UnitCost]
      ,0
      ,0

      ,[Profit]
      ,[CreatedByUsername]
  FROM [dbo].[BidEquipments]
  WHERE BidId = @Id

   INSERT INTO ProjectLabours
    (
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
    )
    SELECT
       @ProjectId
      ,[LabourId]
      ,[Name]
      ,[UOMId]

      ,[UnitCost]
      ,[Quantity]
      ,[TotalCost]

      ,[UnitCost]
      ,0
      ,0

      ,[Profit]
      ,[CreatedByUsername]
  FROM [dbo].[BidLabours]
  WHERE BidId = @Id

  INSERT INTO ProjectMaterials
    (
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
    )
    SELECT
       @ProjectId
      ,[MaterialId]
      ,[Name]
      ,[UOMId]

      ,[UnitCost]
      ,[Quantity]
      ,[TotalCost]

      ,[UnitCost]
      ,0
      ,0

      ,[Profit]
      ,[CreatedByUsername]
  FROM [dbo].[BidMaterials]
  WHERE BidId = @Id

  SELECT @ProjectId AS [ProjectId]

END
