USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_SpecialPricing]    Script Date: 08/10/2011 13:24:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_SpecialPricing](
	[JobId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[ApprovingRVPEmployeeID] [int] NOT NULL,
	[OverallJobDiscount] [decimal](18, 4) NULL,
	[LumpsumValue] [numeric](14, 4) NULL,
	[LumpsumValuePerDay] [numeric](14, 4) NULL,
	[LumpsumDuration] [int] NULL,
	[Notes] [varchar](250) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_SpecialPricingInfo] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_SpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_SpecialPricing_CS_Employee1_ApprovingRVP] FOREIGN KEY([ApprovingRVPEmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_SpecialPricing] CHECK CONSTRAINT [FK_CS_SpecialPricing_CS_Employee1_ApprovingRVP]
GO

ALTER TABLE [dbo].[CS_SpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_SpecialPricing_CS_EmployeeCreationID] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_SpecialPricing] CHECK CONSTRAINT [FK_CS_SpecialPricing_CS_EmployeeCreationID]
GO

ALTER TABLE [dbo].[CS_SpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_CS_SpecialPricing_CS_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[CS_Job] ([ID])
GO

ALTER TABLE [dbo].[CS_SpecialPricing] CHECK CONSTRAINT [FK_CS_SpecialPricing_CS_Job]
GO

ALTER TABLE [dbo].[CS_SpecialPricing]  WITH CHECK ADD  CONSTRAINT [FK_ModificationEmployeespecialpricing] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_SpecialPricing] CHECK CONSTRAINT [FK_ModificationEmployeespecialpricing]
GO


