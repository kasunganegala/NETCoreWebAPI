CREATE PROCEDURE [dbo].[spTenderBids_Get]
	@Id INT
AS
BEGIN
	SELECT *
	FROM [dbo].[Bids] b
	WHERE b.[TenderId] = @Id
		AND ISNULL(b.IsSubmitted,0) = 1 
END
