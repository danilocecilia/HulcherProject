USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DPI]    Script Date: 08/11/2011 14:19:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DPI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[JobID] [int] NOT NULL,
	[ProcessStatus] [smallint] NOT NULL,
	[ProcessStatusDate] [datetime] NOT NULL,
	[CalculationStatus] [smallint] NOT NULL,
	[IsContinuing] [bit] NOT NULL,
	[Calculate] [bit] NOT NULL,
	[Total] [numeric](18, 4) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_DPI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DPI]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPI_CS_DPI] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])
GO

ALTER TABLE [dbo].[CS_DPI] CHECK CONSTRAINT [FK_CS_DPI_CS_DPI]
GO

ALTER TABLE [dbo].[CS_DPI]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPI_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPI] CHECK CONSTRAINT [FK_CS_DPI_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_DPI]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPI_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPI] CHECK CONSTRAINT [FK_CS_DPI_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_DPI] ADD  CONSTRAINT [DF_CS_DPI_IsContinuing]  DEFAULT ((0)) FOR [IsContinuing]
GO

ALTER TABLE [dbo].[CS_DPI] ADD  CONSTRAINT [DF_CS_DPI_IsContinuing1]  DEFAULT ((0)) FOR [Calculate]
GO


