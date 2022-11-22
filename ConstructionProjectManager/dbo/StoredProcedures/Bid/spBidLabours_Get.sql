CREATE PROCEDURE [dbo].[spBidLabours_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.BidLabours b
	WHERE b.[BidId] = @Id
END
