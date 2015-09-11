USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_TurnoverNonActiveReport]    Script Date: 01/11/2012 19:48:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CS_View_TurnoverNonActiveReport]
AS        

SELECT          
	f.Name				AS [Division]        
	,f.ID				AS [DivisionId]        
	,f.Name				AS [DivisionName]        
	,CASE
	WHEN ISNULL(xcd.ID, icd.ID) IS NULL THEN div.Name
	ELSE 'C ' + ISNULL(xcd.Name, icd.Name) + '/' + div.Name
	END AS [ResourceDivision]    
	,div.ID				AS [ResourceDivisionId]        
	,a.ID				AS [JobID]        
	,CASE  
	WHEN a.Number = '' THEN a.Internal_Tracking            
	WHEN a.Number IS NULL THEN a.Internal_Tracking            
	ELSE a.Number            
	END					AS [JobNumber]
	,CASE  
	WHEN a.Number = '' THEN ptype.Acronym + jtype.Description + a.Internal_Tracking            
	WHEN a.Number IS NULL THEN ptype.Acronym + jtype.Description + a.Internal_Tracking            
	ELSE ptype.Acronym + jtype.Description + a.Number            
	END					AS [PrefixedNumber]        
	,CONVERT(bit, 1)    AS [IsResource]        
	,(x.Name + ', ' + x.FirstName)       AS [EmployeeName]        
	,ISNULL(i.Name, '') AS [EquipmentName]        
	,NULL				AS [ProjectManager]        
	,j.Description      AS [JobStatus]        
	,j.ID				AS [JobStatusId]        
	,CASE cty.CSRecord WHEN 1 THEN '* ' + cty.Name ELSE cty.Name END + ', ' + st.Acronym AS [Location]        
	,g.ModificationDate AS [LastModification]        
	,d.ModificationDate AS [ResouceLastModification]        
	,cl.ID				AS [CallLogId]        
	,ct.Description     AS [LastCallType]        
	,cl.CallDate        AS [LastCallDate]        
	,k.ID				AS [CustomerId]        
	,k.Name             AS [Customer]        
	,d.ModifiedBy       AS [ModifiedBy] --,resmod.Name + ', ' + resmod.FirstName     AS [ModifiedBy]        
	,g.InitialCallDate  AS [CallDate]        
	,d.CreationDate     AS [ResourceCallDate]        
	,jjs.JobStartDate   AS [StartDate]        
	,m.Date             AS [PresetDate]        
	,jjs.JobCloseDate   AS [ClosedDate]        
	,CONVERT(VARCHAR, d.Duration)       AS [Duration]        
	,0					AS [HasResources]        
	,ptype.Acronym      AS [PriceTypeAcronym]        
	,jtype.[Description] AS [JobTypeDescription]        
	,NULL				AS [EquipmentStatus]        
	,CAST(1 AS BIT)     AS [IsReserve]        
	,CAST(ISNULL(x.IsKeyPerson, 0) AS BIT)           AS [IsKeyPerson]        
FROM        
	CS_Job a (NOLOCK)        
	INNER JOIN CS_JobDivision b (NOLOCK) ON a.ID = b.JobID AND b.PrimaryDivision = 1        
	INNER JOIN CS_Division f (NOLOCK) ON f.ID = b.DivisionID AND f.IsGeneralLog = 0        
	INNER JOIN CS_LocationInfo li (NOLOCK) ON li.JobID = a.ID      
	LEFT OUTER JOIN CS_State st (NOLOCK) ON li.StateID = st.ID        
	LEFT OUTER JOIN CS_City cty (NOLOCK) ON li.CityID = cty.ID        
	INNER JOIN CS_JobInfo g (NOLOCK) ON g.JobID = a.ID        
	INNER JOIN CS_Job_JobStatus jjs ON jjs.JobID = g.JobID and jjs.Active = 1        
	INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = g.PriceTypeID        
	INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = g.JobTypeID        
	INNER JOIN CS_JobStatus j (NOLOCK) ON jjs.JobStatusID = j.ID        
	INNER JOIN CS_CustomerInfo (NOLOCK) c on g.JobID = c.JobId        
	INNER JOIN CS_Customer k (NOLOCK) ON c.CustomerId = k.ID AND k.IsGeneralLog = 0        
	LEFT OUTER JOIN CS_PresetInfo m (NOLOCK) ON g.JobID = m.JobId        
	LEFT OUTER JOIN CS_Reserve d (NOLOCK) ON a.ID = d.JobID AND d.Active = 1      
	LEFT OUTER JOIN CS_Employee resmod (NOLOCK) ON resmod.ID = d.ModificationID        
	LEFT OUTER JOIN CS_Employee x (NOLOCK) ON d.EmployeeID = x.ID        
	LEFT OUTER JOIN CS_EquipmentType i (NOLOCK) ON d.EquipmentTypeID = i.ID        
	LEFT OUTER JOIN CS_Division div (NOLOCK) ON div.ID = ISNULL(x.DivisionID, D.DivisionID)    
	LEFT OUTER JOIN CS_EmployeeCoverage xc (NOLOCK) ON x.ID = xc.EmployeeID
	LEFT OUTER JOIN CS_Division xcd (NOLOCK) ON xc.DivisionID = xcd.ID
	LEFT OUTER JOIN CS_EquipmentCoverage ic (NOLOCK) ON i.ID = ic.EquipmentID
	LEFT OUTER JOIN CS_Division icd (NOLOCK) ON ic.DivisionID = icd.ID       
	LEFT OUTER JOIN CS_CallLog cl (NOLOCK) ON cl.JobID = a.ID AND cl.ID in (SELECT CallLogID FROM CS_FN_LastCallTypeByResource(x.ID, i.ID))        
	LEFT OUTER JOIN CS_CallType ct (NOLOCK) ON ct.ID = cl.CallTypeID        
WHERE        
	x.ID IS NOT NULL                  
	OR i.ID IS NOT NULL
GO


