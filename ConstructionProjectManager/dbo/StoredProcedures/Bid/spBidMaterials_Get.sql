CREATE PROCEDURE [dbo].[spBidMaterials_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.BidMaterials b
	WHERE b.[BidId] = @Id
END
