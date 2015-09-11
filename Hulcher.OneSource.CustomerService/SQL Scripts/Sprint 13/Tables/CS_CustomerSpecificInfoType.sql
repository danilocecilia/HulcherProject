USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_CustomerSpecificInfoType]    Script Date: 09/21/2011 18:17:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_CustomerSpecificInfoType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModificationID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_CustomerSpecificInfoType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_CustomerSpecificInfoType]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerSpecificInfoType_CS_Employee_Create] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_CustomerSpecificInfoType] CHECK CONSTRAINT [FK_CS_CustomerSpecificInfoType_CS_Employee_Create]
GO

ALTER TABLE [dbo].[CS_CustomerSpecificInfoType]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerSpecificInfoType_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_CustomerSpecificInfoType] CHECK CONSTRAINT [FK_CS_CustomerSpecificInfoType_CS_Employee_Modification]
GO


