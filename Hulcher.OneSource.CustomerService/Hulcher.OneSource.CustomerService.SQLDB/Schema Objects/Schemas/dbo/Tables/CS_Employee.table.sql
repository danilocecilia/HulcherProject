CREATE TABLE [dbo].[CS_Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[DivisionID] [int] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


