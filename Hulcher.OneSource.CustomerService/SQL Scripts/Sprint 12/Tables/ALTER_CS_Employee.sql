USE [OneSource]
GO

ALTER TABLE CS_Employee ADD [IsDentonPersonal] [bit] NOT NULL DEFAULT 0
GO

ALTER TABLE CS_Employee ADD [HasAddressChanges] [bit] NOT NULL DEFAULT 0
GO

ALTER TABLE CS_Employee ADD [HasPhoneChanges] [bit] NOT NULL DEFAULT 0
GO

ALTER TABLE CS_Employee ADD [HireDate] [Datetime] NULL
GO

ALTER TABLE CS_Employee_Load ADD [PersonJobStartDate] [datetime] NULL
GO