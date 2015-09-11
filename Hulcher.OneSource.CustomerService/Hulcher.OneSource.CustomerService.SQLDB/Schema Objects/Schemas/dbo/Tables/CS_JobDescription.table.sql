CREATE TABLE [dbo].[CS_JobDescription](
	[JobId] [int] NOT NULL,
	[NumberEngines] [int] NULL,
	[NumberLoads] [int] NULL,
	[NumberEmpties] [int] NULL,
	[Landing] [varchar](50) NULL,
	[UnNumber] [varchar](50) NULL,
	[STCCInfo] [varchar](50) NULL,
	[Hazmat] [varchar](50) NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_JobDescription] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


