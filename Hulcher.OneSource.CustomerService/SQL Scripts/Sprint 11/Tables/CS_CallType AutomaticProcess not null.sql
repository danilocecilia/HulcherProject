/*
   Thursday, August 04, 20113:14:23 PM
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
ALTER TABLE dbo.CS_CallType
	DROP CONSTRAINT FK_CreationEmployeecalltype
GO
ALTER TABLE dbo.CS_CallType
	DROP CONSTRAINT FK_ModificationEmployeecalltype
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallType
	DROP CONSTRAINT DF__CS_CallTy__Resou__55CAA640
GO
CREATE TABLE dbo.Tmp_CS_CallType
	(
	ID int NOT NULL IDENTITY (1, 1),
	Description varchar(100) NULL,
	Xml varchar(MAX) NULL,
	CallCriteria bit NOT NULL,
	IsAutomaticProcess bit NOT NULL,
	DpiStatus int NULL,
	CreatedBy varchar(100) NOT NULL,
	CreationDate date NOT NULL,
	CreationID int NULL,
	ModifiedBy varchar(100) NOT NULL,
	ModificationDate date NOT NULL,
	ModificationID int NULL,
	Active bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_CallType SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CS_CallType ADD CONSTRAINT
	DF__CS_CallTy__Resou__55CAA640 DEFAULT ((0)) FOR CallCriteria
GO
SET IDENTITY_INSERT dbo.Tmp_CS_CallType ON
GO
IF EXISTS(SELECT * FROM dbo.CS_CallType)
	 EXEC('INSERT INTO dbo.Tmp_CS_CallType (ID, Description, Xml, CallCriteria, IsAutomaticProcess, DpiStatus, CreatedBy, CreationDate, CreationID, ModifiedBy, ModificationDate, ModificationID, Active)
		SELECT ID, Description, Xml, CallCriteria, IsAutomaticProcess, DpiStatus, CreatedBy, CreationDate, CreationID, ModifiedBy, ModificationDate, ModificationID, Active FROM dbo.CS_CallType WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_CallType OFF
GO
ALTER TABLE dbo.CS_CallLog
	DROP CONSTRAINT FK_CS_CallLog_CS_CallType
GO
ALTER TABLE dbo.CS_PrimaryCallType_CallType
	DROP CONSTRAINT FK_CS_PrimaryCallType_CallType_CS_CallType
GO
DROP TABLE dbo.CS_CallType
GO
EXECUTE sp_rename N'dbo.Tmp_CS_CallType', N'CS_CallType', 'OBJECT' 
GO
ALTER TABLE dbo.CS_CallType ADD CONSTRAINT
	PK_CS_CallType PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_CallType ADD CONSTRAINT
	FK_CreationEmployeecalltype FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallType ADD CONSTRAINT
	FK_ModificationEmployeecalltype FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_PrimaryCallType_CallType ADD CONSTRAINT
	FK_CS_PrimaryCallType_CallType_CS_CallType FOREIGN KEY
	(
	CallTypeID
	) REFERENCES dbo.CS_CallType
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_PrimaryCallType_CallType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_CallLog ADD CONSTRAINT
	FK_CS_CallLog_CS_CallType FOREIGN KEY
	(
	CallTypeID
	) REFERENCES dbo.CS_CallType
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_CallLog SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
