USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_FirstAlertDivision]    Script Date: 07/12/2011 14:57:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_FirstAlertDivision](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstAlertID] [int] NOT NULL,
	[DivisionID] [int] NOT NULL,
	[CreationID] [int] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_FirstAlertDivision] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertDivision_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision] CHECK CONSTRAINT [FK_CS_FirstAlertDivision_CS_Division]
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertDivision_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision] CHECK CONSTRAINT [FK_CS_FirstAlertDivision_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertDivision_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision] CHECK CONSTRAINT [FK_CS_FirstAlertDivision_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertDivision_CS_FirstAlert] FOREIGN KEY([FirstAlertID])
REFERENCES [dbo].[CS_FirstAlert] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertDivision] CHECK CONSTRAINT [FK_CS_FirstAlertDivision_CS_FirstAlert]
GO


