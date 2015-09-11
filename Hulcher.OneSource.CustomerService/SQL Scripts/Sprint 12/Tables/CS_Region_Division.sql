CREATE TABLE [dbo].[CS_Region_Division](
	[ID] [int] NOT NULL,
	[RegionID] [int] NOT NULL,
	[DivisionID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [varchar](255) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](255) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
 CONSTRAINT [PK_CS_Region_Division] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
)

GO

ALTER TABLE [dbo].[CS_Region_Division]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_Division_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])
GO

ALTER TABLE [dbo].[CS_Region_Division] CHECK CONSTRAINT [FK_CS_Region_Division_CS_Division]
GO

ALTER TABLE [dbo].[CS_Region_Division]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_Division_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Region_Division] CHECK CONSTRAINT [FK_CS_Region_Division_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_Region_Division]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_Division_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Region_Division] CHECK CONSTRAINT [FK_CS_Region_Division_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_Region_Division]  WITH CHECK ADD  CONSTRAINT [FK_CS_Region_Division_CS_Region] FOREIGN KEY([RegionID])
REFERENCES [dbo].[CS_Region] ([ID])
GO

ALTER TABLE [dbo].[CS_Region_Division] CHECK CONSTRAINT [FK_CS_Region_Division_CS_Region]
GO


