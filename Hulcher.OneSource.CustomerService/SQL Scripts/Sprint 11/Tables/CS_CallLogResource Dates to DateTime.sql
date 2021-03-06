/*
   Friday, August 05, 20116:26:27 PM
   User: OneSourceUser
   Server: sqls02
   Database: OneSource
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLog_Resource_CS_Job
GO
ALTER TABLE dbo.CS_Job SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CreationEmployeeCallLogResource
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_ModificationEmployeeCallLogResource
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLog_Resource_CS_Employee
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLog_Resource_CS_Contact
GO
ALTER TABLE dbo.CS_Contact SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Contact', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Contact', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Contact', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLogResource_CS_CallLogResourceType
GO
ALTER TABLE dbo.CS_CallLogResourceType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_CallLogResourceType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_CallLogResourceType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_CallLogResourceType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLog_Resource_CS_Equipment
GO
ALTER TABLE dbo.CS_Equipment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLogResource
	DROP CONSTRAINT FK_CS_CallLog_Resource_CS_CallLog
GO
ALTER TABLE dbo.CS_CallLog SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_CallLog', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_CallLog', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_CallLog', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CS_CallLogResource
	(
	ID int NOT NULL IDENTITY (1, 1),
	CallLogID int NOT NULL,
	EmployeeID int NULL,
	EquipmentID int NULL,
	ContactID int NULL,
	JobID int NOT NULL,
	Type int NOT NULL,
	CreatedBy varchar(100) NOT NULL,
	CreationDate datetime NOT NULL,
	ModifiedBy varchar(100) NOT NULL,
	ModificationDate datetime NOT NULL,
	Active bit NOT NULL,
	InPerson bit NULL,
	Voicemail bit NULL,
	CreationID int NULL,
	ModificationID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_CallLogResource SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CS_CallLogResource ON
GO
IF EXISTS(SELECT * FROM dbo.CS_CallLogResource)
	 EXEC('INSERT INTO dbo.Tmp_CS_CallLogResource (ID, CallLogID, EmployeeID, EquipmentID, ContactID, JobID, Type, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, InPerson, Voicemail, CreationID, ModificationID)
		SELECT ID, CallLogID, EmployeeID, EquipmentID, ContactID, JobID, Type, CreatedBy, CONVERT(datetime, CreationDate), ModifiedBy, CONVERT(datetime, ModificationDate), Active, InPerson, Voicemail, CreationID, ModificationID FROM dbo.CS_CallLogResource WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_CallLogResource OFF
GO
DROP TABLE dbo.CS_CallLogResource
GO
EXECUTE sp_rename N'dbo.Tmp_CS_CallLogResource', N'CS_CallLogResource', 'OBJECT' 
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	PK_CS_CallLog_Resource PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLog_Resource_CS_CallLog FOREIGN KEY
	(
	CallLogID
	) REFERENCES dbo.CS_CallLog
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLog_Resource_CS_Equipment FOREIGN KEY
	(
	EquipmentID
	) REFERENCES dbo.CS_Equipment
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLogResource_CS_CallLogResourceType FOREIGN KEY
	(
	Type
	) REFERENCES dbo.CS_CallLogResourceType
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLog_Resource_CS_Contact FOREIGN KEY
	(
	ContactID
	) REFERENCES dbo.CS_Contact
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CreationEmployeeCallLogResource FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_ModificationEmployeeCallLogResource FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLog_Resource_CS_Job FOREIGN KEY
	(
	JobID
	) REFERENCES dbo.CS_Job
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLogResource ADD CONSTRAINT
	FK_CS_CallLog_Resource_CS_Employee FOREIGN KEY
	(
	EmployeeID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_CallLogResource', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_CallLogResource', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_CallLogResource', 'Object', 'CONTROL') as Contr_Per 