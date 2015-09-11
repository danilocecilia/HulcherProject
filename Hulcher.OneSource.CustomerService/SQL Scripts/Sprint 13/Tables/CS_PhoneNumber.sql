USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_PhoneNumber]    Script Date: 09/13/2011 15:57:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_PhoneNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContactID] [int] NULL,
	[EmployeeID] [int] NULL,
	[PhoneTypeID] [int] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_ContactPhone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneNumber_CS_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[CS_Contact] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneNumber] CHECK CONSTRAINT [FK_CS_PhoneNumber_CS_Contact]
GO

ALTER TABLE [dbo].[CS_PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneNumber] CHECK CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneNumber] CHECK CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Phone] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneNumber] CHECK CONSTRAINT [FK_CS_PhoneNumber_CS_Employee_Phone]
GO

ALTER TABLE [dbo].[CS_PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_CS_PhoneNumber_CS_PhoneType] FOREIGN KEY([PhoneTypeID])
REFERENCES [dbo].[CS_PhoneType] ([ID])
GO

ALTER TABLE [dbo].[CS_PhoneNumber] CHECK CONSTRAINT [FK_CS_PhoneNumber_CS_PhoneType]
GO


