﻿CREATE TABLE [dbo].[CS_ScopeOfWork](
	[ID] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[ScopeOfWork] [varchar](255) NOT NULL,
	[ScopeChange] [bit] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_ScopeOfWork] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


