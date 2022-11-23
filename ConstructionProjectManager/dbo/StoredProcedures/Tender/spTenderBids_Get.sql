CREATE PROCEDURE [dbo].[spTenderBids_Get]
	@Id INT
AS
BEGIN
	SELECT
	   b.[Id]
      ,b.[TenderId]
      ,b.[ContractorId]
      ,b.[Name]
      ,b.[StartDateTime]
      ,b.[EndDateTime]
      ,b.[IsSubmitted]
      ,b.[Status]
      ,b.[Comment]
      ,b.[CreatedByUsername]
      ,b.[CreatedDateTime]
      ,b.[LastModifiedDateTime]
      ,b.[MaterialCostTotal]
      ,b.[EquipmentCostTotal]
      ,b.[LabourCostTotal]
      ,b.[Tax]
      ,b.[CostTotal]
      ,b.[MaterialsProfit]
      ,b.[EquipmentsProfit]
      ,b.[LaboursProfit]
      ,b.[ProfitTotal]
      ,C.Name AS [ContractorName]
	FROM [dbo].[Bids] b
        INNER JOIN Contractors AS C ON C.Id = b.ContractorId
	WHERE b.[TenderId] = @Id
		AND ISNULL(b.IsSubmitted,0) = 1 
END
