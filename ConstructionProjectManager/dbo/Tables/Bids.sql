CREATE TABLE [dbo].[Bids]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TenderId] INT NOT NULL, 
	[ContractorId] INT NOT NULL, 
	[Name] VARCHAR(MAX) NOT NULL, 
	[StartDateTime] DATETIME NOT NULL, 
	[EndDateTime] DATETIME NOT NULL, 
	[IsSubmitted] BIT DEFAULT 0,
	[Status] NVARCHAR(15) DEFAULT 'Submitted', 
	[Comment] NVARCHAR(500), 
	[CreatedByUsername] NVARCHAR(20) NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastModifiedDateTime] DATETIME NULL,
	[MaterialCostTotal] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[EquipmentCostTotal] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[LabourCostTotal] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[Tax] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[CostTotal] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[MaterialsProfit] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[EquipmentsProfit] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[LaboursProfit] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[ProfitTotal] DECIMAL(18, 2) NOT NULL DEFAULT 0,


	--CONSTRAINT [FK_Bidss_Users] FOREIGN KEY ([CreatedByUsername]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Bids_Tenders] FOREIGN KEY ([TenderId]) REFERENCES [Tenders]([Id])
)
