﻿CREATE TABLE [dbo].[CS_JobAction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_JobAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


