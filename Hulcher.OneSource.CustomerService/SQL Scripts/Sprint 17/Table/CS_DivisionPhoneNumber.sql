USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_DivisionPhoneNumber]    Script Date: 12/02/2011 19:12:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_DivisionPhoneNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionID] [int] NOT NULL,
	[PhoneTypeID] [int] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_DivisionPhoneNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_LocalDivision] ([ID])
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber] CHECK CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Division]
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Employee] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber] CHECK CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Employee]
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Employee1] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber] CHECK CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_Employee1]
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_PhoneType] FOREIGN KEY([PhoneTypeID])
REFERENCES [dbo].[CS_PhoneType] ([ID])
GO

ALTER TABLE [dbo].[CS_DivisionPhoneNumber] CHECK CONSTRAINT [FK_CS_DivisionPhoneNumber_CS_PhoneType]
GO


