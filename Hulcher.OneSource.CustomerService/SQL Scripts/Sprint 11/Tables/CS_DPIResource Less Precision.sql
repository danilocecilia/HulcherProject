/*
   Thursday, August 11, 20116:00:01 PM
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
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_DPI
GO
ALTER TABLE dbo.CS_DPI SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPI', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_Employee
GO
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_Employee_Creation
GO
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_Employee_Modification
GO
ALTER TABLE dbo.CS_Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CS_DPIResource
	DROP CONSTRAINT FK_CS_DPIResource_CS_Equipment
GO
ALTER TABLE dbo.CS_Equipment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_Equipment', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CS_DPIResource
	(
	ID int NOT NULL IDENTITY (1, 1),
	DPIID int NOT NULL,
	EquipmentID int NULL,
	EmployeeID int NULL,
	CalculationStatus smallint NOT NULL,
	Hours numeric(18, 2) NOT NULL,
	ModifiedHours numeric(18, 2) NULL,
	IsContinuing bit NOT NULL,
	ContinuingHours numeric(18, 2) NULL,
	Rate numeric(18, 2) NOT NULL,
	ModifiedRate numeric(18, 2) NULL,
	PermitQuantity int NULL,
	PermitRate numeric(18, 2) NULL,
	MealQuantity int NULL,
	MealRate numeric(18, 2) NULL,
	HasHotel bit NOT NULL,
	HotelRate numeric(18, 2) NULL,
	ModifiedHotelRate numeric(18, 2) NULL,
	Discount int NULL,
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
ALTER TABLE dbo.Tmp_CS_DPIResource SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CS_DPIResource ON
GO
IF EXISTS(SELECT * FROM dbo.CS_DPIResource)
	 EXEC('INSERT INTO dbo.Tmp_CS_DPIResource (ID, DPIID, EquipmentID, EmployeeID, CalculationStatus, Hours, ModifiedHours, IsContinuing, ContinuingHours, Rate, ModifiedRate, PermitQuantity, PermitRate, MealQuantity, MealRate, HasHotel, HotelRate, ModifiedHotelRate, Discount, Total, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active)
		SELECT ID, DPIID, EquipmentID, EmployeeID, CalculationStatus, Hours, ModifiedHours, IsContinuing, ContinuingHours, Rate, ModifiedRate, PermitQuantity, PermitRate, MealQuantity, MealRate, HasHotel, HotelRate, ModifiedHotelRate, Discount, Total, CreatedBy, CreationID, CreationDate, ModifiedBy, ModificationID, ModificationDate, Active FROM dbo.CS_DPIResource WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CS_DPIResource OFF
GO
DROP TABLE dbo.CS_DPIResource
GO
EXECUTE sp_rename N'dbo.Tmp_CS_DPIResource', N'CS_DPIResource', 'OBJECT' 
GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	PK_CS_DPIResource PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	FK_CS_DPIResource_CS_Equipment FOREIGN KEY
	(
	EquipmentID
	) REFERENCES dbo.CS_Equipment
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	FK_CS_DPIResource_CS_Employee FOREIGN KEY
	(
	EmployeeID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	FK_CS_DPIResource_CS_Employee_Creation FOREIGN KEY
	(
	CreationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CS_DPIResource ADD CONSTRAINT
	FK_CS_DPIResource_CS_Employee_Modification FOREIGN KEY
	(
	ModificationID
	) REFERENCES dbo.CS_Employee
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
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
COMMIT
select Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CS_DPIResource', 'Object', 'CONTROL') as Contr_Per 