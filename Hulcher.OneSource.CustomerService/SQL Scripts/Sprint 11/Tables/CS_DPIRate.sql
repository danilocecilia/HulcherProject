USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DPIRate]    Script Date: 08/02/2011 14:42:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DPIRate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Value] [numeric](18, 4) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_DPIRate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DPIRate]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIRate_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIRate] CHECK CONSTRAINT [FK_CS_DPIRate_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_DPIRate]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIRate_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIRate] CHECK CONSTRAINT [FK_CS_DPIRate_CS_Employee_Modification]
GO


