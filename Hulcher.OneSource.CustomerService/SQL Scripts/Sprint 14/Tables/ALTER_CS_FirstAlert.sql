USE [OneSource]
GO

ALTER TABLE [CS_FirstAlert] ADD [CopyToGeneralLog] BIT NOT NULL DEFAULT 0
GO