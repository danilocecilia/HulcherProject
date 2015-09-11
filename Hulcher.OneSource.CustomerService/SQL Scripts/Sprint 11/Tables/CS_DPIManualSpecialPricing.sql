USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DPIManualSpecialPricing]    Script Date: 08/23/2011 16:07:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DPIManualSpecialPricing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DPIId] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[QtdHrs] [numeric](14, 4) NOT NULL,
	[Rate] [numeric](14, 4) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_ManualSpecialPricing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_ManualSpecialPricing_CS_DPI] FOREIGN KEY([DPIId])
REFERENCES [dbo].[CS_DPI] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing] CHECK CONSTRAINT [FK_CS_ManualSpecialPricing_CS_DPI]
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_ManualSpecialPricing_CS_EmployeeCreationID] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing] CHECK CONSTRAINT [FK_CS_ManualSpecialPricing_CS_EmployeeCreationID]
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_ManualSpecialPricing_CS_EmployeeModificationID] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIManualSpecialPricing] CHECK CONSTRAINT [FK_CS_ManualSpecialPricing_CS_EmployeeModificationID]
GO


