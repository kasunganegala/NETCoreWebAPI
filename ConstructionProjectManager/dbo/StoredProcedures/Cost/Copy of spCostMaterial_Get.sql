CREATE PROCEDURE [dbo].[spCostMaterial_Get]
AS
BEGIN

	SELECT 
	   M.[Id]
      ,M.[Name]
      ,M.[Description]

	  ,UOM.[Id] AS [UOMId]
      ,UOM.[Name] AS [UOMName]
      ,UOM.[UOM] AS [UOM]
      ,UOM.[Type] AS [UOMType]
      ,UOM.[Description] AS [UOMDescription]
      ,UOM.[IsCost] AS [UOMIsCost]
      --,UOM.[MeasurementId] AS [UOM]
      ,UOM.[UsedInMaterials] AS [UOMUsedInMaterials]
      ,UOM.[UsedInEquipment] AS [UOMUsedInEquipment]
      ,UOM.[UsedInLabour] AS [UOMUsedInLabour]

      ,UnitMeasurement.[UOM] AS [UnitMeasurementUOM]
      ,UnitMeasurement.[Type] AS [UnitMeasurementType]

	FROM [dbo].[Meterials] M
		LEFT JOIN dbo.UOM AS UOM ON UOM.Id = M.UOM
        LEFT JOIN dbo.UOM AS UnitMeasurement ON UnitMeasurement.Id = UOM.[MeasurementId]

    ORDER BY M.Name 

END
