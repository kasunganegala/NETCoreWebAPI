CREATE PROCEDURE [dbo].[spProject_Get]
	@Id INT
AS
BEGIN
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

      ,p.[MaterialCostTotal]
      ,p.[EquipmentCostTotal]
      ,p.[LabourCostTotal]
      ,p.[Tax]
      ,p.[CostTotal]

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
