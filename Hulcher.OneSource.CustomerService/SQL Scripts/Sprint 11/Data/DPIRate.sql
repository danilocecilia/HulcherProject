USE [OneSource]
GO

SET IDENTITY_INSERT [dbo].[CS_DPIRate] ON
GO

INSERT INTO [dbo].[CS_DPIRate]
 (ID, Description, Value, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active)
VALUES
 (1, 'Permit Rate', 100, 'System', 1, GETDATE(), 'System', 1, GETDATE(), 1);
 
INSERT INTO [dbo].[CS_DPIRate]
 (ID, Description, Value, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active)
VALUES
 (2, 'Hotel Rate', 70, 'System', 1, GETDATE(), 'System', 1, GETDATE(), 1);
 
INSERT INTO [dbo].[CS_DPIRate]
 (ID, Description, Value, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active)
VALUES
 (3, 'Meal Rate - Each 6h', 10, 'System', 1, GETDATE(), 'System', 1, GETDATE(), 1);
 
SET IDENTITY_INSERT [dbo].[CS_DPIRate] OFF
GO

