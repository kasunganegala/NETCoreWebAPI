CREATE PROCEDURE [dbo].[spBidEquipments]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.BidEquipments b
	WHERE b.[BidId] = @Id
END
