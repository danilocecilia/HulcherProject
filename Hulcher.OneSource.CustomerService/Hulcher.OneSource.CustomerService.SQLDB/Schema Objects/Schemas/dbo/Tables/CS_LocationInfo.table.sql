CREATE TABLE [dbo].[CS_LocationInfo](
	[JobID] [int] NOT NULL,
	[CountryID] [int] NOT NULL,
	[StateID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[ZipCodeId] [int] NOT NULL,
	[SiteName] [varchar](100) NULL,
	[AlternateName] [varchar](100) NULL,
	[Directions] [varchar](500) NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_LocationInfo_1] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


