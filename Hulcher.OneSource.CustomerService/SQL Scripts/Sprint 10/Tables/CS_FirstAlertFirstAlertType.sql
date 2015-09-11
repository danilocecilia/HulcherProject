USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_FirstAlertFirstAlertType]    Script Date: 07/11/2011 15:00:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_FirstAlertFirstAlertType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstAlertID] [int] NOT NULL,
	[FirstAlertTypeID] [int] NOT NULL,
	[CreationID] [int] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_FirstAlertFirstAlertType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType] CHECK CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType] CHECK CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_FirstAlert] FOREIGN KEY([FirstAlertID])
REFERENCES [dbo].[CS_FirstAlert] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType] CHECK CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_FirstAlert]
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_FirstAlertType] FOREIGN KEY([FirstAlertTypeID])
REFERENCES [dbo].[CS_FirstAlertType] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertFirstAlertType] CHECK CONSTRAINT [FK_CS_FirstAlertFirstAlertType_CS_FirstAlertType]
GO

