CREATE PROCEDURE [dbo].[spBidsExport_Get]
(
	@NoOfRecords			INT OUT
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
	FROM [dbo].[Bids] AS b
		INNER JOIN [dbo].[Tenders] AS t ON t.Id = b.TenderId
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = t.CustomerId
		INNER JOIN [dbo].Contractors AS CON ON CON.Id = b.ContractorId

	WHERE ((t.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((t.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((t.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((t.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((t.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
		AND ((b.CreatedDateTime = @SubmittedDate AND @SubmittedDate != NULL) OR @SubmittedDate IS NULL)
		AND ((b.Status = @Status AND @Status != 'All') OR @Status = 'All')
		AND ((b.ContractorId = @Contractor AND @Contractor != 0) OR @Contractor = 0)
		)

	SELECT 
	   b.[Id]
      ,b.[TenderId]
      ,b.[ContractorId]
      ,b.[Name]
      ,b.[StartDateTime]
      ,b.[EndDateTime]
      ,b.[IsSubmitted]
      ,b.[Status]
      ,b.[Comment]
      ,b.[CreatedByUsername]
      ,b.[CreatedDateTime]
      ,b.[LastModifiedDateTime]
	  ,CUS.Name AS [CustomerName]
	  ,CON.Name AS [ContractorName]
	FROM [dbo].[Bids] AS b
		INNER JOIN [dbo].[Tenders] AS t ON t.Id = b.TenderId
		INNER JOIN [dbo].Customers AS CUS ON CUS.Id = t.CustomerId
		INNER JOIN [dbo].Contractors AS CON ON CON.Id = b.ContractorId
	WHERE ((t.CustomerId = @Customer AND @Customer != 0) OR @Customer = 0)
		AND ((t.TenderType = @TenderType AND @TenderType != 0) OR @TenderType = 0)
		AND ((t.ProjectType = @ProjectType AND @ProjectType != 0) OR @ProjectType = 0)
		AND ((t.StartDateTime = @StartDate AND @StartDate != NULL) OR @StartDate IS NULL)
		AND ((t.EndDateTime = @EndDate AND @EndDate != NULL) OR @EndDate IS NULL)
		AND ((b.CreatedDateTime = @SubmittedDate AND @SubmittedDate != NULL) OR @SubmittedDate IS NULL)
		AND ((b.Status = @Status AND @Status != 'All') OR @Status = 'All')
		AND ((b.ContractorId = @Contractor AND @Contractor != 0) OR @Contractor = 0)

END
