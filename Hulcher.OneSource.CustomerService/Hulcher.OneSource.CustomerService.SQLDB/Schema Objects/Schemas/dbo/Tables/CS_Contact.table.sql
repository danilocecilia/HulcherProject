CREATE TABLE [dbo].[CS_Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Cell] [varchar](50) NULL,
	[CreationDate] [date] NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CS_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


