USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_FirstAlertContactPersonal]    Script Date: 12/21/2011 17:47:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_FirstAlertContactPersonal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstAlertID] [int] NOT NULL,
	[EmployeeID] [int] NULL,
	[ContactID] [int] NULL,
	[EmailAdviseDate] [datetime] NULL,
	[EmailAdviseUser] [varchar](50) NULL,
	[VMXAdviseDate] [datetime] NULL,
	[VMXAdviseUser] [varchar](50) NULL,
	[InPersonAdviseDate] [datetime] NULL,
	[InPersonAdviseUser] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_FirstAlertContactPersonal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[CS_Contact] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal] CHECK CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Contact]
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal] CHECK CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee]
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal] CHECK CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal] CHECK CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_FirstAlert] FOREIGN KEY([FirstAlertID])
REFERENCES [dbo].[CS_FirstAlert] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertContactPersonal] CHECK CONSTRAINT [FK_CS_FirstAlertContactPersonal_CS_FirstAlert]
GO


