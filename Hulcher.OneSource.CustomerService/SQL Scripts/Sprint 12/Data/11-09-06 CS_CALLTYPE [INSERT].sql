SET IDENTITY_INSERT CS_CALLTYPE ON

GO

INSERT INTO CS_CallType (ID, Description, Xml, CallCriteria, IsAutomaticProcess, DpiStatus, CreatedBy, CreationDate, CreationID, ModifiedBy, ModificationDate, ModificationID, Active)
SELECT 43, 'Coverage', null, 0, 1, null, 'System', GETDATE(), 325237, 'System', GETDATE(), 325237, 1

GO

SET IDENTITY_INSERT CS_CALLTYPE OFF

GO

INSERT INTO CS_PrimaryCallType_CallType
SELECT 43, 8
--COVERAGE AND NON-JOB UPDATE