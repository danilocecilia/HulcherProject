/*
   Tuesday, September 27, 20114:11:11 PM
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
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CreationEmployeeJobInfo
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_ModificationEmployeeJobInfo
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_Employee
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_Job
GO
ALTER TABLE dbo.CS_Job SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_Frequency
GO
ALTER TABLE dbo.CS_Frequency SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Frequency', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Frequency', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Frequency', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_JobAction
GO
ALTER TABLE dbo.CS_JobAction SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_JobAction', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_JobAction', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_JobAction', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_JobType
GO
ALTER TABLE dbo.CS_JobType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_JobType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_JobType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_JobType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_JobCategory
GO
ALTER TABLE dbo.CS_JobCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_JobCategory', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_JobCategory', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_JobCategory', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_JobInfo
	DROP CONSTRAINT FK_CS_JobInfo_CS_PriceType
GO
ALTER TABLE dbo.CS_PriceType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_PriceType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_PriceType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_PriceType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CS_JobInfo
	(
	JobID int NOT NULL,
	InitialCallDate date NOT NULL,
	InitialCallTime time(7) NOT NULL,
	InitialCallDatetimeOffset datetimeoffset(7) NULL,
	PriceTypeID int NOT NULL,
	JobCategoryID int NOT NULL,
	InterimBill bit NOT NULL,
	EmployeeID int NULL,
	JobTypeID int NOT NULL,
	JobActionID int NOT NULL,
	FrequencyID int NULL,
	CustomerSpecificInfo varchar(MAX) NULL,
	CreatedBy varchar(50) NOT NULL,
	CreationDate datetime NOT NULL,
	ModifiedBy varchar(50) NOT NULL,
	ModificationDate datetime NOT NULL,
	Active bit NOT NULL,
	ProjectManager int NOT NULL,
	CreationID int NULL,
	ModificationID int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_JobInfo SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.CS_JobInfo)
	 EXEC('INSERT INTO dbo.Tmp_CS_JobInfo (JobID, InitialCallDate, InitialCallTime, InitialCallDatetimeOffset, PriceTypeID, JobCategoryID, InterimBill, EmployeeID, JobTypeID, JobActionID, FrequencyID, CustomerSpecificInfo, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, ProjectManager, CreationID, ModificationID)
		SELECT JobID, InitialCallDate, InitialCallTime, InitialCallDatetimeOffset, PriceTypeID, JobCategoryID, InterimBill, EmployeeID, JobTypeID, JobActionID, FrequencyID, CONVERT(varchar(MAX), CustomerSpecificInfo), CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active, ProjectManager, CreationID, ModificationID FROM dbo.CS_JobInfo WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.CS_Job_JobStatus
	DROP CONSTRAINT FK__CS_Job_Jo__JobID__7D046EF2
GO
DROP TABLE dbo.CS_JobInfo
GO
EXECUTE sp_rename N'dbo.Tmp_CS_JobInfo', N'CS_JobInfo', 'OBJECT' 
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	PK_CS_JobInfo PRIMARY KEY CLUSTERED 
	(
	JobID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	UQ__CS_JobIn__056690E35224328E UNIQUE NONCLUSTERED 
	(
	JobID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_PriceType FOREIGN KEY
	(
	PriceTypeID
	) REFERENCES dbo.CS_PriceType
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_JobCategory FOREIGN KEY
	(
	JobCategoryID
	) REFERENCES dbo.CS_JobCategory
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_JobType FOREIGN KEY
	(
	JobTypeID
	) REFERENCES dbo.CS_JobType
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_JobAction FOREIGN KEY
	(
	JobActionID
	) REFERENCES dbo.CS_JobAction
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_Frequency FOREIGN KEY
	(
	FrequencyID
	) REFERENCES dbo.CS_Frequency
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_Job FOREIGN KEY
	(
	JobID
	) REFERENCES dbo.CS_Job
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CreationEmployeeJobInfo FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_ModificationEmployeeJobInfo FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_JobInfo ADD CONSTRAINT
	FK_CS_JobInfo_CS_Employee FOREIGN KEY
	(
	EmployeeID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_JobInfo', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_JobInfo', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_JobInfo', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_Job_JobStatus ADD CONSTRAINT
	FK__CS_Job_Jo__JobID__7D046EF2 FOREIGN KEY
	(
	JobID
	) REFERENCES dbo.CS_JobInfo
	(
	JobID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_Job_JobStatus SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Job_JobStatus', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Job_JobStatus', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Job_JobStatus', 'Object', 'CONTROL') as Contr_Per 