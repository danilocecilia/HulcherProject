CREATE TABLE [dbo].[CS_CallLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[CallTypeID] [int] NOT NULL,
	[CallDate] [datetime] NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionDateOffset] [datetime] NOT NULL,
	[Xml] [nchar](10) NULL,
	[Note] [varchar](500) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModificationDate] [date] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_Call_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


