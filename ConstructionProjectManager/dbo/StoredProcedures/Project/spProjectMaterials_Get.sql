CREATE PROCEDURE [dbo].[spProjectMaterials_Get]
	@Id INT
AS
BEGIN
	SELECT 
	   [Id]
      ,[ProjectId]
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
      ,[LastModifiedDateTime]
	FROM dbo.[ProjectMaterials] pm
	WHERE pm.ProjectId = @Id
END
