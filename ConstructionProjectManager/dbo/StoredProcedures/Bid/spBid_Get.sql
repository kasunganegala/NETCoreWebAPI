CREATE PROCEDURE [dbo].[spBid_Get]
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
      ,T.Name AS [TenderName]
      ,T.CustomerId 
      ,T.TenderType 
      ,T.ProjectType
	FROM dbo.[Bids] b
        INNER JOIN Tenders AS T ON T.id = b.TenderId
	WHERE b.Id = @Id
END
