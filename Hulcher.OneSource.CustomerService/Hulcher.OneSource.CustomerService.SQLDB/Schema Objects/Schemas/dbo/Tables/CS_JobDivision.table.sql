CREATE TABLE [dbo].[CS_JobDivision](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[DivisionID] [int] NOT NULL,
	[PrimaryDivision] [bit] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_JobDivision] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


