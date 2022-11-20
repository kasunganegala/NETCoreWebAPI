CREATE PROCEDURE [dbo].[spCostUOM_Get]
AS
BEGIN

	SELECT 
       [Id]
      ,[Name]
      ,[UOM]
      ,[Type]
      ,[Description]
      ,[IsCost]
      ,[MeasurementId]
      ,[UsedInMaterials]
      ,[UsedInEquipment]
      ,[UsedInLabour]
    FROM [dbo].[UOM]
    ORDER BY CASE WHEN uom = 'Rs/day' THEN 1 
				  WHEN uom = 'Rs/hour' THEN 2 
				  WHEN uom = 'Rs/Item' THEN 3 
				  ELSE 4 END, uom DESC

END
