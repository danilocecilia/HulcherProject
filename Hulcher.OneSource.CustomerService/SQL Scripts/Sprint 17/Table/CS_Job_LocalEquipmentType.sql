USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_Job_LocalEquipementType]    Script Date: 11/09/2011 18:17:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_Job_LocalEquipmentType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[LocalEquipmentTypeID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_Job_LocalEquipementType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType]  WITH CHECK ADD  CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType] CHECK CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType]  WITH CHECK ADD  CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType] CHECK CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType]  WITH CHECK ADD  CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Job_LocalEquipementType] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType] CHECK CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_Job_LocalEquipementType]
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType]  WITH CHECK ADD  CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_LocalEquipmentType] FOREIGN KEY([LocalEquipmentTypeID])
REFERENCES [dbo].[CS_LocalEquipmentType] ([ID])
GO

ALTER TABLE [dbo].[CS_Job_LocalEquipementType] CHECK CONSTRAINT [FK_CS_Job_LocalEquipementType_CS_LocalEquipmentType]
GO


