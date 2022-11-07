CREATE PROCEDURE [dbo].[spTenders_Get]
(
	@NoOfRecords			INT OUT
	,@Offset				INT
	,@Limit					INT
)
AS
BEGIN

	SET @NoOfRecords = (SELECT COUNT(Id) FROM dbo.[Tenders] u)

	SELECT *
	FROM dbo.[Tenders] u
	ORDER BY u.Id ASC
	OFFSET (@Offset) ROWS FETCH NEXT @Limit ROWS ONLY;	
END
