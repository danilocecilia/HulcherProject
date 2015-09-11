USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_FirstAlert]    Script Date: 07/11/2011 14:56:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_FirstAlert](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[CustomerID] [int] NULL,
	[InChargeEmployeeID] [int] NULL,
	[CountryID] [int] NULL,
	[StateID] [int] NULL,
	[CityID] [int] NULL,
	[Date] [datetime] NOT NULL,
	[ReportedBy] [varchar](50) NULL,
	[CompletedByEmployeeID] [int] NULL,
	[Details] [varchar](1000) NOT NULL,
	[HasPoliceReport] [bit] NOT NULL,
	[PoliceAgency] [varchar](50) NULL,
	[PoliceReportNumber] [varchar](50) NULL,
	[CreationID] [int] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_FirstAlert] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_City]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[CS_Country] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Country]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CS_Customer] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Customer]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Employee_CompletedBy] FOREIGN KEY([CompletedByEmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Employee_CompletedBy]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Employee_InCharge] FOREIGN KEY([InChargeEmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Employee_InCharge]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_Job]
GO

ALTER TABLE [dbo].[CS_FirstAlert]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlert_CS_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlert] CHECK CONSTRAINT [FK_CS_FirstAlert_CS_State]
GO


