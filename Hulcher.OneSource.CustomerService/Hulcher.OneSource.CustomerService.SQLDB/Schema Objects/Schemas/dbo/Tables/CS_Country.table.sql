﻿CREATE TABLE [dbo].[CS_Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


