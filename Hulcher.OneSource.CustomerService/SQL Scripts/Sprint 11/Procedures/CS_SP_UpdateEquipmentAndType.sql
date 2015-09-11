ALTER PROC CS_SP_UpdateEquipmentAndType    
AS      
      
BEGIN    
      
INSERT INTO CS_EquipmentType      
(Number, Name, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active)      
SELECT DISTINCT      
 Load.EquipmentTypeNumber AS Number,      
 Load.Name AS Name,      
 'Load' AS CreatedBy,      
 GETDATE() AS CreationDate,      
 'Load' AS ModifiedBy,      
 GETDATE() AS ModificationDate,      
 1 AS Active      
FROM CS_Equipment_Load Load LEFT OUTER JOIN CS_EquipmentType Type      
ON Load.EquipmentTypeNumber = Type.Number      
WHERE Type.ID IS NULL      
      
INSERT INTO CS_Equipment      
(EquipmentID,    
 Name,    
 Description,    
 ComboID,    
 EquipmentTypeID,    
 DivisionID,    
 LicensePlate,    
 SerialNumber,    
 Year,    
 Notes,    
 BodyType,    
 Make,    
 Model,    
 EquipmentFunction,    
 AssignedTo,    
 RegisteredState,    
 AttachPanelBoss,    
 AttachPileDriver,    
 AttachSlipSheet,    
 AttachTieClamp,    
 AttachTieInserter,    
 AttachTieTamper,    
 AttachUndercutter,    
 Checksum,    
 Status,    
 CreateBy,    
 CreationDate,    
 ModifiedBy,    
 ModificationDate,    
 Active)      
SELECT      
 Load.EquipmentID AS EquipmentID,      
 Load.EquipmentNumber AS Name,      
 Load.Description AS Description,      
 NULL AS ComboID,      
 Type.ID AS EquipmentTypeID,      
 Div.ID AS DivisionID,    
 Load.LicensePlate,    
 Load.SerialNumber,    
 Load.Year,    
 Load.Notes,    
 Load.BodyType,    
 Load.Make,    
 Load.Model,    
 Load.EquipmentFunction,    
 Load.AssignedTo,    
 Load.RegisteredState,    
 CASE WHEN Load.AttachPanelBoss = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachPileDriver = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachSlipSheet = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachTieClamp = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachTieInserter = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachTieTamper = 'Y' THEN 1 ELSE 0 END,    
 CASE WHEN Load.AttachUndercutter = 'Y' THEN 1 ELSE 0 END,    
 Load.Checksum AS Checksum,      
 'Up' as Status,      
 'Load' AS CreatedBy,      
 GETDATE() AS CreationDate,      
 'Load' AS ModifiedBy,      
 GETDATE() AS ModificationDate,      
 CASE WHEN Load.Status = 'A' THEN 1 ELSE 0 END AS Active      
 FROM CS_Equipment_Load Load LEFT OUTER JOIN CS_Division Div      
 ON Load.DivisionName = Div.Name LEFT OUTER JOIN CS_EquipmentType Type      
 ON Load.EquipmentTypeNumber = Type.Number LEFT OUTER JOIN CS_Equipment Eq      
 ON Load.EquipmentID = Eq.EquipmentID      
 WHERE Eq.ID IS NULL      
       
Update Up      
SET      
 Up.Name = Load.Name      
FROM CS_EquipmentType Up      
 INNER JOIN ( SELECT DISTINCT Name, EquipmentTypeNumber FROM CS_Equipment_Load ) Load on Up.Number = Load.EquipmentTypeNumber      

INSERT INTO CS_JobDivision (JobID, DivisionID, PrimaryDivision, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active)
SELECT DISTINCT
 Res.JobID AS [JobID],
 Div.ID AS [DivisionID],
 0 AS [PrimaryDivision],
 'System' AS [CreatedBy],
 GETDATE() AS [CreationDate],
 'System' AS [ModifiedBy],
 GETDATE() AS [ModificationDate],
 1 AS Active
FROM CS_Equipment Up INNER JOIN CS_Equipment_Load Load      
ON Up.EquipmentID = Load.EquipmentID INNER JOIN CS_Resource Res
ON Up.ID = Res.EquipmentID AND Res.Active = 1 INNER JOIN CS_Division Div
ON Load.DivisionName = Div.Name LEFT OUTER JOIN CS_JobDivision JobDiv
ON Div.ID = JobDiv.DivisionID AND JobDiv.JobID = Res.JobID
WHERE Up.Checksum <> Load.Checksum
AND JobDiv.DivisionID IS NULL
       
UPDATE Up      
SET      
 Up.EquipmentID = Load.EquipmentID,      
 Up.Name = Load.EquipmentNumber,      
 Up.Description = Load.Description,      
 Up.EquipmentTypeID = Type.ID,      
 Up.DivisionID = Div.ID,    
 Up.LicensePlate = Load.LicensePlate,    
 Up.SerialNumber = Load.SerialNumber,    
 Up.Year = Load.Year,    
 Up.Notes = Load.Notes,    
 Up.BodyType = Load.BodyType,    
 Up.Make = Load.Make,    
 Up.Model = Load.Model,    
 Up.EquipmentFunction = Load.EquipmentFunction,    
 Up.AssignedTo = Load.AssignedTo,    
 Up.RegisteredState = Load.RegisteredState,    
 Up.AttachPanelBoss = CASE WHEN Load.AttachPanelBoss = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachPileDriver = CASE WHEN Load.AttachPileDriver = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachSlipSheet = CASE WHEN Load.AttachSlipSheet = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachTieClamp = CASE WHEN Load.AttachTieClamp = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachTieInserter = CASE WHEN Load.AttachTieInserter = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachTieTamper = CASE WHEN Load.AttachTieTamper = 'Y' THEN 1 ELSE 0 END,    
 Up.AttachUndercutter = CASE WHEN Load.AttachUndercutter = 'Y' THEN 1 ELSE 0 END,    
 Up.Checksum = Load.Checksum,      
 Up.Active = CASE WHEN Load.Status = 'A' THEN 1 ELSE 0 END      
FROM CS_Equipment Up INNER JOIN CS_Equipment_Load Load      
ON Up.EquipmentID = Load.EquipmentID LEFT OUTER JOIN CS_Division Div      
ON Load.DivisionName = Div.Name LEFT OUTER JOIN CS_EquipmentType Type      
ON Load.EquipmentTypeNumber = Type.Number LEFT OUTER JOIN CS_Equipment Eq      
ON Load.EquipmentID = Eq.EquipmentID      
WHERE Eq.Checksum <> Load.Checksum      
      
END 