USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_PhoneType]    Script Date: 09/13/2011 15:26:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_PhoneType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModificatedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_PhoneType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_PhoneType]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneType_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneType] CHECK CONSTRAINT [FK_CS_PhoneType_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_PhoneType]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneType_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneType] CHECK CONSTRAINT [FK_CS_PhoneType_CS_Employee_Modification]
GO


