CREATE PROCEDURE [dbo].[spBidEquipments_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.BidEquipments b
	WHERE b.[BidId] = @Id
END
