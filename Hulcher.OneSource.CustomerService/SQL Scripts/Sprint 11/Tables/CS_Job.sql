USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_Job]    Script Date: 08/02/2011 17:30:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_Job](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](50) NULL,
	[Internal_Tracking] [varchar](50) NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[EmergencyResponse] [bit] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
	[BillingStatus] [int] NULL,
	[IsBilling] [bit] NOT NULL,
 CONSTRAINT [PK_CS_Job] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_Job]  WITH CHECK ADD  CONSTRAINT [FK_CreationEmployeejob] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Job] CHECK CONSTRAINT [FK_CreationEmployeejob]
GO

ALTER TABLE [dbo].[CS_Job]  WITH CHECK ADD  CONSTRAINT [FK_ModificationEmployeejob] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_Job] CHECK CONSTRAINT [FK_ModificationEmployeejob]
GO

ALTER TABLE [dbo].[CS_Job] ADD  CONSTRAINT [DF_CS_Job_IsBilling]  DEFAULT ((0)) FOR [IsBilling]
GO


