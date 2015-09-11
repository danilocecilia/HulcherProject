USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_LocalDivision]    Script Date: 12/05/2011 16:44:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_LocalDivision](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](300) NULL,
	[StateID] [int] NULL,
	[CityID] [int] NULL,
	[ZipCodeID] [int] NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
 CONSTRAINT [PK_CS_LocalDivision] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_LocalDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocalDivision_CS_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_LocalDivision] CHECK CONSTRAINT [FK_CS_LocalDivision_CS_City]
GO

ALTER TABLE [dbo].[CS_LocalDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocalDivision_CS_Employee] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_LocalDivision] CHECK CONSTRAINT [FK_CS_LocalDivision_CS_Employee]
GO

ALTER TABLE [dbo].[CS_LocalDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocalDivision_CS_Employee1] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_LocalDivision] CHECK CONSTRAINT [FK_CS_LocalDivision_CS_Employee1]
GO

ALTER TABLE [dbo].[CS_LocalDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocalDivision_CS_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_LocalDivision] CHECK CONSTRAINT [FK_CS_LocalDivision_CS_State]
GO

ALTER TABLE [dbo].[CS_LocalDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocalDivision_CS_ZipCode] FOREIGN KEY([ZipCodeID])
REFERENCES [dbo].[CS_ZipCode] ([ID])
GO

ALTER TABLE [dbo].[CS_LocalDivision] CHECK CONSTRAINT [FK_CS_LocalDivision_CS_ZipCode]
GO


