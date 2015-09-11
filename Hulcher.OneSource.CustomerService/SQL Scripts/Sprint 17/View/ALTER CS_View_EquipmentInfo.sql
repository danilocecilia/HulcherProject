USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_EquipmentInfo]    Script Date: 11/16/2011 11:57:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CS_View_EquipmentInfo]    
AS          
          
SELECT DISTINCT        
   combo.ID       AS [ComboID]        
  ,combo.Name      AS [ComboName]        
  ,combo.ComboType     AS [ComboType]        
  ,combo.CreationDate    AS [CreateDate]        
  ,equip.ID       AS [EquipmentID]        
  ,equip.EquipmentTypeID   AS [EquipmentTypeID]        
  ,equip.Name      AS [UnitNumber]        
  ,equip.[Description]    AS [Descriptor]        
  ,equip.[Status]     AS [EquipmentStatus]        
  ,Div.ID       AS [DivisionID]      
  ,regdiv.RegionID  AS [RegionID]    
  ,EcovDiv.ID      AS [CoverageDivisionID]        
  ,CASE         
   WHEN EcovDiv.Name IS NULL THEN Div.Name         
   ELSE 'C ' +  EcovDiv.Name + '/' + Div.Name        
  END        AS [DivisionName]        
  ,[state].Acronym      AS [DivisionState]      
  ,[state].Name			AS [DivisionStateName]
  ,EcovState.Acronym    AS [CoverageDivisionState]   
  ,EcovState.Name		AS [CoverageDivisionStateName]        
  ,equip.Active      AS [Active]        
  ,Job.ID       AS [JobID]        
  ,CASE        
   WHEN Job.Number = '' THEN Job.Internal_Tracking         
   WHEN Job.Number IS NULL THEN Job.Internal_Tracking         
   ELSE Job.Number         
  END        AS [JobNumber]     
  ,CASE        
   WHEN Job.Number = '' THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking         
   WHEN Job.Number IS NULL THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking         
   ELSE ptype.Acronym + jtype.Description + Job.Number         
  END        AS [PrefixedNumber]       
  ,Stats.Description    AS [JobStatus]        
  ,CASE        
   WHEN Job.ID IS NULL THEN 'Available'         
   ELSE 'Assigned'        
  END        AS [Status]        
  ,CASE         
   WHEN LInf.JobID IS NOT NULL THEN City.Name + ', ' + Sta2.Acronym         
   ELSE ''         
  END        AS [JobLocation]        
  ,ISNULL([Call].JobID, 0)    AS [CallLogJobID]        
  ,ISNULL([Call].ID, 0)    AS [CallLogID]        
  ,CType.[Description]    AS [Type]        
  ,CASE         
   WHEN equip.ID = combo.PrimaryEquipmentID THEN 1         
   ELSE 0         
  END        AS [IsPrimary]        
  ,ptype.Acronym     AS [PriceTypeAcronym]        
  ,jtype.[Description]    AS [JobTypeDescription]        
  ,equip.HeavyEquipment    AS [HeavyEquipment]  
  ,equip.DisplayInResourceAllocation AS [DisplayInResourceAllocation]        
  ,equip.Make      AS [Make]        
  ,equip.Model      AS [Model]        
  ,equip.Year      AS [Year]        
  ,CAST(      
 CASE       
  WHEN divConf.ComboID IS NULL       
  THEN 0       
  ELSE 1       
 END         
  AS BIT)       AS [DivisionConflicted]      
  ,CAST(      
 CASE       
  WHEN (permail.EquipmentID IS NOT NULL)      
  THEN 1       
  ELSE 0       
 END         
  AS BIT)       AS [PermitExpired],
  equip.Seasonal  
 FROM        
  CS_Equipment equip (NOLOCK)        
  LEFT OUTER JOIN CS_EquipmentCombo combo (NOLOCK) ON equip.ComboID = combo.ID AND combo.Active = 1        
  INNER JOIN CS_Division div (NOLOCK) ON equip.DivisionID = div.ID      
  LEFT OUTER JOIN CS_Region_Division regdiv (NOLOCK) ON regdiv.DivisionID = div.ID and regdiv.Active = 1    
  LEFT OUTER JOIN CS_State state (NOLOCK) ON div.StateID = state.ID        
  LEFT OUTER JOIN CS_Resource Res (NOLOCK) ON equip.ID = Res.EquipmentID AND Res.Active = 1        
  LEFT OUTER JOIN CS_Job Job (NOLOCK) ON Res.JobID = Job.ID AND Job.Active = 1        
  LEFT OUTER JOIN CS_LocationInfo LInf (NOLOCK) ON Job.ID = LInf.JobID        
  LEFT OUTER JOIN CS_State Sta2 (NOLOCK) ON LInf.StateID = Sta2.ID        
  LEFT OUTER JOIN CS_City City (NOLOCK) ON LInf.CityID = City.ID        
  LEFT OUTER JOIN CS_JobInfo Inf (NOLOCK) ON Job.ID = Inf.JobID        
  LEFT OUTER JOIN CS_Job_JobStatus JobStats (NOLOCK) ON Job.ID = JobStats.JobID AND JobStats.Active = 1        
  LEFT OUTER JOIN CS_JobStatus [Stats] (NOLOCK) ON JobStats.JobStatusID = [Stats].ID        
  LEFT OUTER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = Inf.PriceTypeID        
  LEFT OUTER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = Inf.JobTypeID        
  LEFT OUTER JOIN         
  (        
   SELECT MAX(ID) AS ID, EquipmentID        
   FROM CS_CallLogResource        
   WHERE EquipmentID IS NOT NULL AND Active = 1        
   GROUP BY EquipmentID         
  ) TRes ON equip.ID = TRes.EquipmentID        
  LEFT OUTER JOIN CS_CallLogResource CRes ON TRes.ID = CRes.ID AND CRes.Active = 1        
  LEFT OUTER JOIN CS_CallLog [Call] (NOLOCK) ON CRes.CallLogID = [Call].ID AND [Call].Active = 1        
  LEFT OUTER JOIN CS_CallType CType (NOLOCK) ON [Call].CallTypeID = CType.ID        
  LEFT OUTER JOIN CS_EquipmentCoverage Ecov (NOLOCK) ON equip.ID = Ecov.EquipmentID AND Ecov.Active = 1        
  LEFT OUTER JOIN CS_Division EcovDiv (NOLOCK) ON Ecov.DivisionID = EcovDiv.ID      
  LEFT OUTER JOIN CS_State EcovState (NOLOCK) ON EcovDiv.StateID = EcovState.ID        
  LEFT OUTER JOIN CS_View_ConflictedEquipmentCombos divConf ON combo.ID = divConf.ComboID      
  LEFT OUTER JOIN       
  (      
 SELECT      
  perm.EquipmentId AS EquipmentID       
 FROM     CS_EquipmentPermit perm      
 WHERE       
  ExpirationDate <= GETDATE()      
  AND perm.Active = 1      
 GROUP BY      
  perm.EquipmentId      
  ) AS permail      
  ON equip.ID = permail.EquipmentID    



GO


