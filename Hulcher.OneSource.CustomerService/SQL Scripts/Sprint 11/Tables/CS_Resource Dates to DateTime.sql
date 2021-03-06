/*
   Tuesday, August 09, 20113:25:30 PM
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
ALTER TABLE dbo.CS_Resource
	DROP CONSTRAINT FK_CS_Resource_CS_Job
GO
ALTER TABLE dbo.CS_Job SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_Resource
	DROP CONSTRAINT FK_CreationEmployeeresource
GO
ALTER TABLE dbo.CS_Resource
	DROP CONSTRAINT FK_ModificationEmployeeresource
GO
ALTER TABLE dbo.CS_Resource
	DROP CONSTRAINT FK_CS_Resource_CS_Employee
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_Resource
	DROP CONSTRAINT FK_CS_Resource_CS_Equipment
GO
ALTER TABLE dbo.CS_Equipment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CS_Resource
	(
	ID int NOT NULL IDENTITY (1, 1),
	EmployeeID int NULL,
	EquipmentID int NULL,
	JobID int NOT NULL,
	Type int NOT NULL,
	Duration numeric(18, 2) NOT NULL,
	StartDateTime datetime NOT NULL,
	Description nchar(10) NULL,
	CreatedBy varchar(100) NOT NULL,
	CreationDate datetime NOT NULL,
	ModifiedBy varchar(100) NOT NULL,
	ModificationDate datetime NOT NULL,
	Active bit NOT NULL,
	CreationID int NULL,
	ModificationID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_Resource SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CS_Resource ON
GO
IF EXISTS(SELECT * FROM dbo.CS_Resource)
	 EXEC('INSERT INTO dbo.Tmp_CS_Resource (ID, EmployeeID, EquipmentID, JobID, Type, Duration, StartDateTime, Description, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, CreationID, ModificationID)
		SELECT ID, EmployeeID, EquipmentID, JobID, Type, Duration, StartDateTime, Description, CreatedBy, CONVERT(datetime, CreationDate), ModifiedBy, CONVERT(datetime, ModificationDate), Active, CreationID, ModificationID FROM dbo.CS_Resource WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_Resource OFF
GO
DROP TABLE dbo.CS_Resource
GO
EXECUTE sp_rename N'dbo.Tmp_CS_Resource', N'CS_Resource', 'OBJECT' 
GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	PK_CS_Resource PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	FK_CS_Resource_CS_Equipment FOREIGN KEY
	(
	EquipmentID
	) REFERENCES dbo.CS_Equipment
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	FK_CreationEmployeeresource FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	FK_ModificationEmployeeresource FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	FK_CS_Resource_CS_Job FOREIGN KEY
	(
	JobID
	) REFERENCES dbo.CS_Job
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_Resource ADD CONSTRAINT
	FK_CS_Resource_CS_Employee FOREIGN KEY
	(
	EmployeeID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Resource', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Resource', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Resource', 'Object', 'CONTROL') as Contr_Per 