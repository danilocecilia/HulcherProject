USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_CustomerAlias]    Script Date: 11/07/2011 13:22:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_CustomerAlias](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreationID] [int] NULL,
	[ModificationID] [int] NULL,
 CONSTRAINT [PK_CS_CustomerAlias] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_CustomerAlias]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerAlias_CS_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CS_Customer] ([ID])
GO

ALTER TABLE [dbo].[CS_CustomerAlias] CHECK CONSTRAINT [FK_CS_CustomerAlias_CS_Customer]
GO

ALTER TABLE [dbo].[CS_CustomerAlias]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerAlias_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_CustomerAlias] CHECK CONSTRAINT [FK_CS_CustomerAlias_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_CustomerAlias]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerAlias_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_CustomerAlias] CHECK CONSTRAINT [FK_CS_CustomerAlias_CS_Employee_Modification]
GO


