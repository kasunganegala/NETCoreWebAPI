CREATE PROCEDURE [dbo].[spProjectLabours_Get]
	@Id INT
AS
BEGIN
	SELECT 
		[Id]
      ,[ProjectId]
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
      ,[CreatedDateTime]
      ,[LastModifiedDateTime]
	FROM dbo.[ProjectLabours] pl
	WHERE pl.ProjectId = @Id
END
