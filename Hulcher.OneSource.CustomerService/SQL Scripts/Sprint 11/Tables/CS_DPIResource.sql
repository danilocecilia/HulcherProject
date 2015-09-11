USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DPIResource]    Script Date: 08/02/2011 14:25:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DPIResource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DPIID] [int] NOT NULL,
	[EquipmentID] [int] NULL,
	[EmployeeID] [int] NULL,
	[CalculationStatus] [smallint] NOT NULL,
	[Hours] [numeric](18, 4) NOT NULL,
	[ModifiedHours] [numeric](18, 4) NULL,
	[IsContinuing] [bit] NOT NULL,
	[Rate] [numeric](18, 4) NOT NULL,
	[ModifiedRate] [numeric](18, 4) NULL,
	[PermitQuantity] [int] NULL,
	[PermitRate] [numeric](18, 4) NULL,
	[MealQuantity] [int] NULL,
	[MealRate] [numeric](18, 4) NULL,
	[HasHotel] [bit] NOT NULL,
	[HotelRate] [numeric](18, 4) NULL,
	[ModifiedHotelRate] [numeric](18, 4) NULL,
	[Discount] [int] NULL,
	[Total] [numeric](18, 4) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_DPIResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DPIResource]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIResource_CS_DPI] FOREIGN KEY([DPIID])
REFERENCES [dbo].[CS_DPI] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIResource] CHECK CONSTRAINT [FK_CS_DPIResource_CS_DPI]
GO

ALTER TABLE [dbo].[CS_DPIResource]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIResource_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIResource] CHECK CONSTRAINT [FK_CS_DPIResource_CS_Employee]
GO

ALTER TABLE [dbo].[CS_DPIResource]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIResource_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIResource] CHECK CONSTRAINT [FK_CS_DPIResource_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_DPIResource]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIResource_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIResource] CHECK CONSTRAINT [FK_CS_DPIResource_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_DPIResource]  WITH CHECK ADD  CONSTRAINT [FK_CS_DPIResource_CS_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[CS_Equipment] ([ID])
GO

ALTER TABLE [dbo].[CS_DPIResource] CHECK CONSTRAINT [FK_CS_DPIResource_CS_Equipment]
GO


