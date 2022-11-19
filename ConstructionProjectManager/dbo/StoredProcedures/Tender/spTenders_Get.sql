﻿CREATE PROCEDURE [dbo].[spTenders_Get]
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
)
AS
BEGIN

	SET @NoOfRecords = (SELECT COUNT(u.Id) 
	FROM dbo.[Tenders] u
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = u.CustomerId
	WHERE ((u.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((u.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((u.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((u.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((u.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
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
		AND ((u.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((u.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
	ORDER BY u.Id ASC
	OFFSET (@Offset) ROWS FETCH NEXT @Limit ROWS ONLY;	
END
