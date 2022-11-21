CREATE PROCEDURE [dbo].[spCostLabour_Get]
AS
BEGIN

	SELECT 
	   L.[Id]
      ,L.[Name]
      ,L.[Description]

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

	FROM [dbo].[Labours] L
		LEFT JOIN dbo.UOM AS UOM ON UOM.Id = L.UOM
        LEFT JOIN dbo.UOM AS UnitMeasurement ON UnitMeasurement.Id = UOM.[MeasurementId]

    ORDER BY L.Name 

END
