USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_Route]    Script Date: 11/29/2011 15:25:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_Route](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[Miles] [int] NULL,
	[Hours] [decimal](18, 2) NULL,
	[Fuel] [int] NULL,
	[Route] [varchar](300) NULL,
	[CityPermitOffice] [varchar](300) NULL,
	[CountyPermitOffice] [varchar](300) NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [varchar](255) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](255) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
 CONSTRAINT [PK_CS_Route] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_Route]  WITH CHECK ADD  CONSTRAINT [FK_CS_Route_CS_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_Route] CHECK CONSTRAINT [FK_CS_Route_CS_City]
GO

ALTER TABLE [dbo].[CS_Route]  WITH CHECK ADD  CONSTRAINT [FK_CS_Route_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])
GO

ALTER TABLE [dbo].[CS_Route] CHECK CONSTRAINT [FK_CS_Route_CS_Division]
GO


