CREATE PROCEDURE [dbo].[spProjectEquipments_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.BidEquipments b
	WHERE b.[BidId] = @Id
END
