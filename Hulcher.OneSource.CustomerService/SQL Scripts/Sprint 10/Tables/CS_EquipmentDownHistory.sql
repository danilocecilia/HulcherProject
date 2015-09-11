USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_EquipmentDownHistory]    Script Date: 07/21/2011 17:12:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_EquipmentDownHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[DownHistoryStartDate] [datetime] NOT NULL,
	[DownHistoryEndDate] [datetime] NULL,
	[Duration] [int] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_EquipmentDownHistory_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory] CHECK CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory] CHECK CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[CS_Equipment] ([ID])
GO

ALTER TABLE [dbo].[CS_EquipmentDownHistory] CHECK CONSTRAINT [FK_CS_EquipmentDownHistory_CS_Equipment]
GO


