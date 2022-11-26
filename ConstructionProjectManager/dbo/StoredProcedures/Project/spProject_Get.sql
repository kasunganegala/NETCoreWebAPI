CREATE PROCEDURE [dbo].[spProject_Get]
	@Id INT
AS
BEGIN
    
   DECLARE @eqCost decimal(18,2)= 0
   DECLARE @maCost decimal(18,2)= 0
   DECLARE @laCost decimal(18,2)= 0

    SELECT @eqCost = SUM(UnitCost * Quantity) 
    FROM [ProjectTasksWorklogEquipments]
    WHERE ProjectId = @Id
	group by ProjectId

     SELECT @maCost = SUM(UnitCost * Quantity) 
    FROM [ProjectTasksWorklogMaterials]
    WHERE ProjectId = @Id
	group by ProjectId

     SELECT @laCost = SUM(UnitCost * Quantity) 
    FROM [ProjectTasksWorklogLabours]
    WHERE ProjectId = @Id
	group by ProjectId



	SELECT 
	   p.[Id]
      ,p.[BidId]
      ,p.[ContractorId]
      ,p.[Name]
      ,p.[StartDateTime]
      ,p.[EndDateTime]
      ,p.[IsSubmitted]
      ,p.[Status]
      ,p.[Comment]
      ,p.[CreatedByUsername]
      ,p.[CreatedDateTime]
      ,p.[LastModifiedDateTime]

      ,p.[EstimatedMaterialCostTotal]
      ,p.[EstimatedEquipmentCostTotal]
      ,p.[EstimatedLabourCostTotal]
      ,p.[EstimatedTax]
      ,p.[EstimatedCostTotal]

      ,@maCost AS [MaterialCostTotal]
      ,@eqCost AS [EquipmentCostTotal]
      ,@laCost AS [LabourCostTotal]
      ,p.[Tax]
      ,(@maCost + @eqCost + @laCost) AS [CostTotal]

      ,p.[MaterialsProfit]
      ,p.[EquipmentsProfit]
      ,p.[LaboursProfit]
      ,p.[ProfitTotal]

      ,p.[EstimatedStartDateTime]
      ,p.[EstimatedEndDateTime]
      ,p.[CustomerId] 
      ,t.TenderType
      ,t.ProjectType

	FROM dbo.[Projects] p
        INNER JOIN [dbo].[Bids] AS b ON b.Id = p.BidId
		INNER JOIN [dbo].[Tenders] AS t ON t.Id = b.TenderId
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = p.CustomerId
		INNER JOIN [dbo].Contractors AS CON ON CON.Id = p.ContractorId

	WHERE p.Id = @Id
END
