CREATE TABLE [dbo].[CS_Region](
	[ID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[RegionalVpID] [int] NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [varchar](255) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](255) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
 CONSTRAINT [PK_CS_Region] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
)
GO
ALTER TABLE [dbo].[CS_Region]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Region] CHECK CONSTRAINT [FK_CS_Region_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_Region]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Region] CHECK CONSTRAINT [FK_CS_Region_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_Region]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_CS_Employee_RegionalVP] FOREIGN KEY([RegionalVpID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Region] CHECK CONSTRAINT [FK_CS_Region_CS_Employee_RegionalVP]
GO


