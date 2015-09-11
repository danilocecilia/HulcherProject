USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_EmployeeCoverage]    Script Date: 08/26/2011 14:22:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_EmployeeCoverage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[DivisionID] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[CoverageStartDate] [datetime] NOT NULL,
	[CoverageEndDate] [datetime] NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_EmployeeCoverage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeCoverage_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage] CHECK CONSTRAINT [FK_CS_EmployeeCoverage_CS_Division]
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage] CHECK CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage] CHECK CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeCoverage] CHECK CONSTRAINT [FK_CS_EmployeeCoverage_CS_Employee]
GO


