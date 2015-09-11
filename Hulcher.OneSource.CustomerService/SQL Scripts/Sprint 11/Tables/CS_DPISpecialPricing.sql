USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DPISpecialPricing]    Script Date: 08/10/2011 13:27:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DPISpecialPricing](
	[ID] [int] NOT NULL,
	[DPIID] [int] NOT NULL,
	[ApprovingRVPEmployee] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[OverallJobDiscount] [decimal](18, 4) NULL,
	[LumpsumValue] [numeric](14, 4) NULL,
	[LumpsumValuePerDay] [numeric](14, 4) NULL,
	[LumpsumDuration] [int] NULL,
	[Notes] [varchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_DPISpecialPricing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPISpecialPricing_CS_DPI] FOREIGN KEY([DPIID])
REFERENCES [dbo].[CS_DPI] ([ID])
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing] CHECK CONSTRAINT [FK_CS_DPISpecialPricing_CS_DPI]
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeApprovingRVP] FOREIGN KEY([ApprovingRVPEmployee])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing] CHECK CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeApprovingRVP]
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeCreationID] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing] CHECK CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeCreationID]
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeModificationID] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPISpecialPricing] CHECK CONSTRAINT [FK_CS_DPISpecialPricing_CS_EmployeeModificationID]
GO


