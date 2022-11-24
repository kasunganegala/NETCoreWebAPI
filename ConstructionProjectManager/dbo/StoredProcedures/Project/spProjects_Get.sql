CREATE PROCEDURE [dbo].[spProjects_Get]
(
	@NoOfRecords			INT OUT
	,@Offset				INT = 0
	,@Limit					INT = 6
	,@Customer				INT = 0
	,@TenderType			INT = 0
	,@ProjectType			INT = 0
	,@StartDate				DATETIME = NULL
	,@EndDate				DATETIME = NULL
	,@UserRole				VARCHAR(MAX)
	,@Contractor			INT = 0
	,@Status				VARCHAR(MAX)
	,@SubmittedDate			DATETIME = NULL

)
AS
BEGIN

	SET @NoOfRecords = (SELECT COUNT(b.Id) 
	FROM [dbo].[Projects] AS p
		INNER JOIN [dbo].[Bids] AS b ON b.Id = p.BidId
		INNER JOIN [dbo].[Tenders] AS t ON t.Id = b.TenderId
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = p.CustomerId
		INNER JOIN [dbo].Contractors AS CON ON CON.Id = p.ContractorId

	WHERE ((t.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((t.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((t.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((t.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((t.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
		AND ((p.CreatedDateTime = @SubmittedDate AND @SubmittedDate != NULL) OR @SubmittedDate IS NULL)
		AND ((p.Status = @Status AND @Status != 'All') OR @Status = 'All')
		AND ((p.ContractorId = @Contractor AND @Contractor != 0) OR @Contractor = 0)
		)

	SELECT 
	   p.[Id]
	  ,t.Id AS [TenderId]
      ,p.[BidId] 
      ,p.[ContractorId]
	  ,p.[CustomerId]
      ,p.[Name]
      ,p.[StartDateTime]
      ,p.[EndDateTime]
      ,p.[IsSubmitted]
      ,p.[Status]
      ,p.[Comment]
      ,p.[CreatedByUsername]
      ,p.[CreatedDateTime]
      ,p.[LastModifiedDateTime]
	  ,CUS.Name AS [CustomerName]
	  ,CON.Name AS [ContractorName]
	FROM [dbo].[Projects] AS p
		INNER JOIN [dbo].[Bids] AS b ON b.Id = p.BidId
		INNER JOIN [dbo].[Tenders] AS t ON t.Id = b.TenderId
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = p.CustomerId
		INNER JOIN [dbo].Contractors AS CON ON CON.Id = p.ContractorId
	WHERE ((t.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((t.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((t.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((t.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((t.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
		AND ((p.CreatedDateTime = @SubmittedDate AND @SubmittedDate != NULL) OR @SubmittedDate IS NULL)
		AND ((p.Status = @Status AND @Status != 'All') OR @Status = 'All')
		AND ((p.ContractorId = @Contractor AND @Contractor != 0) OR @Contractor = 0)

	ORDER BY p.Id ASC
	OFFSET (@Offset) ROWS FETCH NEXT @Limit ROWS ONLY;	
END
