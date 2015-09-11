USE [OneSource]
GO

INSERT INTO CS_PhoneType (ID, Name, CreatedBy, CreationDate, CreationID, ModificatedBy, ModificationDate, ModificationID, Active)
VALUES (4, 'VMX', 'System', GETDATE(), null, 'System', GETDATE(), null, 1);
INSERT INTO CS_PhoneType (ID, Name, CreatedBy, CreationDate, CreationID, ModificatedBy, ModificationDate, ModificationID, Active)
VALUES (5, 'Extension', 'System', GETDATE(), null, 'System', GETDATE(), null, 1);
INSERT INTO CS_PhoneType (ID, Name, CreatedBy, CreationDate, CreationID, ModificatedBy, ModificationDate, ModificationID, Active)
VALUES (6, 'Pager', 'System', GETDATE(), null, 'System', GETDATE(), null, 1);
INSERT INTO CS_PhoneType (ID, Name, CreatedBy, CreationDate, CreationID, ModificatedBy, ModificationDate, ModificationID, Active)
VALUES (7, 'PIN Number', 'System', GETDATE(), null, 'System', GETDATE(), null, 1);