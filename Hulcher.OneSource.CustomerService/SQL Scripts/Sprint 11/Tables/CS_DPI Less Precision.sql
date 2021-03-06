/*
   Thursday, August 11, 20116:04:04 PM
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
ALTER TABLE dbo.CS_DPI
	DROP CONSTRAINT FK_CS_DPI_CS_DPI
GO
ALTER TABLE dbo.CS_Job SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Job', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPI
	DROP CONSTRAINT FK_CS_DPI_CS_Employee_Creation
GO
ALTER TABLE dbo.CS_DPI
	DROP CONSTRAINT FK_CS_DPI_CS_Employee_Modification
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPI
	DROP CONSTRAINT DF_CS_DPI_IsContinuing
GO
ALTER TABLE dbo.CS_DPI
	DROP CONSTRAINT DF_CS_DPI_IsContinuing1
GO
CREATE TABLE dbo.Tmp_CS_DPI
	(
	ID int NOT NULL IDENTITY (1, 1),
	Date date NOT NULL,
	JobID int NOT NULL,
	ProcessStatus smallint NOT NULL,
	ProcessStatusDate datetime NOT NULL,
	CalculationStatus smallint NOT NULL,
	IsContinuing bit NOT NULL,
	Calculate bit NOT NULL,
	Total numeric(18, 2) NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	CreationID int NULL,
	CreationDate datetime NOT NULL,
	ModifiedBy varchar(50) NOT NULL,
	ModificationID int NULL,
	ModificationDate datetime NOT NULL,
	Active bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CS_DPI SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CS_DPI ADD CONSTRAINT
	DF_CS_DPI_IsContinuing DEFAULT ((0)) FOR IsContinuing
GO
ALTER TABLE dbo.Tmp_CS_DPI ADD CONSTRAINT
	DF_CS_DPI_IsContinuing1 DEFAULT ((0)) FOR Calculate
GO
SET IDENTITY_INSERT dbo.Tmp_CS_DPI ON
GO
IF EXISTS(SELECT * FROM dbo.CS_DPI)
	 EXEC('INSERT INTO dbo.Tmp_CS_DPI (ID, Date, JobID, ProcessStatus, ProcessStatusDate, CalculationStatus, IsContinuing, Calculate, Total, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active)
		SELECT ID, Date, JobID, ProcessStatus, ProcessStatusDate, CalculationStatus, IsContinuing, Calculate, Total, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active FROM dbo.CS_DPI WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_DPI OFF
GO
ALTER TABLE dbo.CS_DPIManualSpecialPricing
	DROP CONSTRAINT FK_CS_ManualSpecialPricing_CS_DPI
GO
ALTER TABLE dbo.CS_DPISpecialPricing
	DROP CONSTRAINT FK_CS_DPISpecialPricing_CS_DPI
GO
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_DPI
GO
DROP TABLE dbo.CS_DPI
GO
EXECUTE sp_rename N'dbo.Tmp_CS_DPI', N'CS_DPI', 'OBJECT' 
GO
ALTER TABLE dbo.CS_DPI ADD CONSTRAINT
	PK_CS_DPI PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_DPI ADD CONSTRAINT
	FK_CS_DPI_CS_Employee_Creation FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPI ADD CONSTRAINT
	FK_CS_DPI_CS_Employee_Modification FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPI ADD CONSTRAINT
	FK_CS_DPI_CS_DPI FOREIGN KEY
	(
	JobID
	) REFERENCES dbo.CS_Job
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	FK_CS_DPIResource_CS_DPI FOREIGN KEY
	(
	DPIID
	) REFERENCES dbo.CS_DPI
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPIResource SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPISpecialPricing ADD CONSTRAINT
	FK_CS_DPISpecialPricing_CS_DPI FOREIGN KEY
	(
	DPIID
	) REFERENCES dbo.CS_DPI
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPISpecialPricing SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPISpecialPricing', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPISpecialPricing', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPISpecialPricing', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPIManualSpecialPricing ADD CONSTRAINT
	FK_CS_ManualSpecialPricing_CS_DPI FOREIGN KEY
	(
	DPIId
	) REFERENCES dbo.CS_DPI
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPIManualSpecialPricing SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPIManualSpecialPricing', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPIManualSpecialPricing', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPIManualSpecialPricing', 'Object', 'CONTROL') as Contr_Per 