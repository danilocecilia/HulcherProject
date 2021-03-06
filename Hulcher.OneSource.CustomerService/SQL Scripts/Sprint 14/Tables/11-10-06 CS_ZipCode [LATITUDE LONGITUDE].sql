/*
   Thursday, October 06, 20112:24:04 PM
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
ALTER TABLE dbo.CS_ZipCode
	DROP CONSTRAINT FK_CS_ZipCode_CS_City
GO
ALTER TABLE dbo.CS_City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_City', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_City', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_City', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_ZipCode
	DROP CONSTRAINT FK_CreationEmployeezipcode
GO
ALTER TABLE dbo.CS_ZipCode
	DROP CONSTRAINT FK_ModificationEmployeezipcode
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CS_ZipCode
	(
	ID int NOT NULL IDENTITY (1, 1),
	Name varchar(10) NOT NULL,
	CityId int NOT NULL,
	Latitude float(53) NULL,
	Longitude float(53) NULL,
	CreatedBy varchar(100) NOT NULL,
	CreationDate datetime NOT NULL,
	ModifiedBy varchar(100) NOT NULL,
	ModificationDate datetime NOT NULL,
	Active bit NOT NULL,
	CreationID int NULL,
	ModificationID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_ZipCode SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CS_ZipCode ON
GO
IF EXISTS(SELECT * FROM dbo.CS_ZipCode)
	 EXEC('INSERT INTO dbo.Tmp_CS_ZipCode (ID, Name, CityId, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, CreationID, ModificationID)
		SELECT ID, Name, CityId, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, CreationID, ModificationID FROM dbo.CS_ZipCode WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_ZipCode OFF
GO
ALTER TABLE dbo.CS_FirstAlertPerson
	DROP CONSTRAINT FK_CS_FirstAlertPerson_CS_ZipCode
GO
ALTER TABLE dbo.CS_FirstAlertPerson
	DROP CONSTRAINT FK_CS_FirstAlertPerson_CS_ZipCode_Doctor
GO
ALTER TABLE dbo.CS_FirstAlertPerson
	DROP CONSTRAINT FK_CS_FirstAlertPerson_CS_ZipCode_Hospital
GO
ALTER TABLE dbo.CS_FirstAlertPerson
	DROP CONSTRAINT FK_CS_FirstAlertPerson_CS_ZipCode_DriversLicense
GO
DROP TABLE dbo.CS_ZipCode
GO
EXECUTE sp_rename N'dbo.Tmp_CS_ZipCode', N'CS_ZipCode', 'OBJECT' 
GO
ALTER TABLE dbo.CS_ZipCode ADD CONSTRAINT
	PK_CS_ZIP PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX nci_CS_ZipCode_CityId_Active ON dbo.CS_ZipCode
	(
	CityId,
	Active
	) INCLUDE (ID, Name, CreatedBy, CreationDate, ModifiedBy, ModificationDate) 
 WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.CS_ZipCode ADD CONSTRAINT
	FK_CreationEmployeezipcode FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_ZipCode ADD CONSTRAINT
	FK_ModificationEmployeezipcode FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_ZipCode ADD CONSTRAINT
	FK_CS_ZipCode_CS_City FOREIGN KEY
	(
	CityId
	) REFERENCES dbo.CS_City
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_ZipCode', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_ZipCode', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_ZipCode', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_FirstAlertPerson ADD CONSTRAINT
	FK_CS_FirstAlertPerson_CS_ZipCode FOREIGN KEY
	(
	ZipcodeID
	) REFERENCES dbo.CS_ZipCode
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_FirstAlertPerson ADD CONSTRAINT
	FK_CS_FirstAlertPerson_CS_ZipCode_Doctor FOREIGN KEY
	(
	DoctorsZipcodeID
	) REFERENCES dbo.CS_ZipCode
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_FirstAlertPerson ADD CONSTRAINT
	FK_CS_FirstAlertPerson_CS_ZipCode_Hospital FOREIGN KEY
	(
	HospitalZipcodeID
	) REFERENCES dbo.CS_ZipCode
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_FirstAlertPerson ADD CONSTRAINT
	FK_CS_FirstAlertPerson_CS_ZipCode_DriversLicense FOREIGN KEY
	(
	DriversLicenseZipcodeID
	) REFERENCES dbo.CS_ZipCode
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_FirstAlertPerson SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_FirstAlertPerson', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_FirstAlertPerson', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_FirstAlertPerson', 'Object', 'CONTROL') as Contr_Per 