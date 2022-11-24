CREATE PROCEDURE [dbo].[spProjectEquipments_Get]
	@Id INT
AS
BEGIN
	SELECT
		[Id]
      ,[ProjectId]
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
      ,[LastModifiedDateTime]
	FROM dbo.[ProjectEquipments] pe
	WHERE pe.[ProjectId] = @Id
END
