CREATE PROCEDURE [dbo].[spBid_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[Bids] b
	WHERE b.Id = @Id
END
