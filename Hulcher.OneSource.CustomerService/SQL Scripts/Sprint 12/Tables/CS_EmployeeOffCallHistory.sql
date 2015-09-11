USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_EmployeeOffCallHistory]    Script Date: 08/26/2011 14:37:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_EmployeeOffCallHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[ProxyEmployeeID] [int] NOT NULL,
	[OffCallStartDate] [date] NOT NULL,
	[OffCallEndDate] [date] NOT NULL,
	[OffCallReturnTime] [time] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationID] [int] NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_EmployeeOffCallHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory] CHECK CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory] CHECK CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory] CHECK CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee]
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Proxy] FOREIGN KEY([ProxyEmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_EmployeeOffCallHistory] CHECK CONSTRAINT [FK_CS_EmployeeOffCallHistory_CS_Employee_Proxy]
GO


