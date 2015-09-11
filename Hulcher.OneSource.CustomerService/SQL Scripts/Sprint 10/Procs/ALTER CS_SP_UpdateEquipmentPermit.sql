USE [OneSource]
GO

/****** Object:  StoredProcedure [dbo].[CS_SP_UpdateEquipmentPermit]    Script Date: 07/28/2011 13:50:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[CS_SP_UpdateEquipmentPermit]    
AS    
    
BEGIN    
    
 INSERT INTO CS_EquipmentPermit  
  (EquipmentId, LicensePermitId, Code, Type, LicenseNumber, IssueDate, ExpirationDate, BuiltDate, Checksum,   
   CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active)  
 SELECT  
  b.Id, a.ID, a.Code, a.Type, a.LicenseNumber, a.IssueDate, a.ExpirationDate, a.BuiltDate, a.Checksum,  
  'Load', GETDATE(), 'Load', GETDATE(), 1  
 FROM  
  CS_EquipmentPermit_Load a (NOLOCK)  
   INNER JOIN CS_Equipment b (NOLOCK) on a.UnitID = b.EquipmentID  
   LEFT OUTER JOIN CS_EquipmentPermit c (NOLOCK) on a.ID = c.LicensePermitId  
 WHERE  
  c.ID IS NULL  
  
 UPDATE Up SET  
   Up.EquipmentId = b.Id  
  ,Up.LicensePermitId = Load.ID  
  ,Up.Code = Load.Code  
  ,Up.LicenseNumber = Load.LicenseNumber  
  ,Up.IssueDate = Load.IssueDate  
  ,Up.ExpirationDate = Load.ExpirationDate  
  ,Up.BuiltDate = Load.BuiltDate  
  ,Up.Checksum = Load.Checksum  
 FROM  
   CS_EquipmentPermit Up (NOLOCK)  
   INNER JOIN CS_EquipmentPermit_Load Load (NOLOCK) on Up.LicensePermitId = Load.ID  
    INNER JOIN CS_Equipment b (NOLOCK) on Load.UnitID = b.EquipmentID  
    LEFT OUTER JOIN CS_EquipmentPermit c (NOLOCK) on Load.ID = c.LicensePermitId  
 WHERE  
   c.Checksum <> Load.Checksum 
 
 UPDATE a SET
	a.Active = 0
 FROM  
  CS_EquipmentPermit a (NOLOCK)  
   INNER JOIN CS_Equipment b (NOLOCK) on a.EquipmentId = b.EquipmentID  
   LEFT OUTER JOIN CS_EquipmentPermit_Load c (NOLOCK) on a.LicensePermitId = c.ID
 WHERE  
  c.ID IS NULL   
  
END  
GO


