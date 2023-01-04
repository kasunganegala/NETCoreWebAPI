CREATE PROCEDURE [dbo].[spTenders_Get]
(
	@NoOfRecords			INT OUT
	,@Offset				INT = 0
	,@Limit					INT = 3
	,@Customer				INT = 0
	,@TenderType			INT = 0
	,@ProjectType			INT = 0
	,@StartDate				DATETIME = NULL
	,@EndDate				DATETIME = NULL
	,@UserRole				VARCHAR(MAX)
	,@Status				VARCHAR(MAX)
)
AS
BEGIN

	IF @Status = ''
	BEGIN
		SET @Status = 'All'
	END

	SET @NoOfRecords = (SELECT COUNT(u.Id) 
						FROM dbo.[Tenders] u
							INNER JOIN [dbo].Customers AS CUS ON CUS.Id = u.CustomerId
						WHERE ((u.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
							AND ((u.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
							AND ((u.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
							AND ((CAST(u.StartDateTime AS DATE) = CAST(@StartDate AS DATE) AND @StartDate IS NOT NULL) OR @StartDate IS NULL)
							AND ((CAST(u.EndDateTime AS DATE) = CAST(@EndDate AS DATE) AND @EndDate IS NOT NULL) OR @EndDate IS NULL)
							AND ((@Status = 'All') 
								OR (u.Status = @Status AND @Status != 'Not Started') 
								OR (CAST(GETUTCDATE() AS DATE) < CAST(u.StartDateTime AS DATE) AND @Status = 'Not Started'))
							)

	SELECT 
		u.[Id]
      ,u.[Name]
      ,u.[Description]
      ,u.[TenderType]
      ,u.[StartDateTime]
      ,u.[EndDateTime]
      ,u.[CustomerId]
      ,u.[Status]
      ,u.[ProjectType]
      ,u.[Comment]
      ,u.[CreatedByUsername]
      ,u.[CreatedDateTime]
      ,u.[LastModifiedDateTime]
	  ,CUS.Name AS [CustomerName]
	FROM dbo.[Tenders] u
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = u.CustomerId
	WHERE ((u.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((u.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((u.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((CAST(u.StartDateTime AS DATE) = CAST(@StartDate AS DATE) AND @StartDate IS NOT NULL) OR @StartDate IS NULL)
		AND ((CAST(u.EndDateTime AS DATE) = CAST(@EndDate AS DATE) AND @EndDate IS NOT NULL) OR @EndDate IS NULL)
		AND ((@Status = 'All') 
				OR (u.Status = @Status AND @Status != 'Not Started') 
				OR (CAST(GETUTCDATE() AS DATE) < CAST(u.StartDateTime AS DATE) AND @Status = 'Not Started'))
	ORDER BY u.Id ASC
	OFFSET (@Offset) ROWS FETCH NEXT @Limit ROWS ONLY;	
END
