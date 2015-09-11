USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_ProjectCalendar_Reserved]    Script Date: 01/03/2012 17:51:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[CS_View_ProjectCalendar_Reserved]
AS

SELECT DISTINCT
	job.ID											AS [JobID]
	, CASE      
		WHEN job.Number = '' THEN job.Internal_Tracking                
		WHEN job.Number IS NULL THEN job.Internal_Tracking                
		ELSE job.Number                
	  END											AS [JobNumber] 
	, CASE      
		WHEN job.Number = '' THEN ptype.Acronym + jtype.Description + job.Internal_Tracking                
		WHEN job.Number IS NULL THEN ptype.Acronym + jtype.Description + job.Internal_Tracking                
		ELSE ptype.Acronym + jtype.Description + job.Number                
	  END											AS [PrefixedNumber]
	, jinfo.InitialCallDate							AS [JobInitialCallDate]
	, pinfo.Date									AS [PresetInfoDate]
	, jjstatus.JobStatusId							AS [JobStatusId]
	, jstatus.Description							AS [JobStatusName]
	, jinfo.JobActionID								AS [JobActionID]
	, jact.Description								AS [JobActionName]
	, jjstatus.JobStartDate							AS [JobStartDate]
	, jinfo.ModificationDate						AS [LastModification]
	, div.ID										AS [JobDivisionID]
	, div.Name										AS [JobDivisionName]
	, city.Name										AS [JobCityName]
	, state.Name									AS [JobStateName]
	, cust.ID										AS [CustomerID]            
	, cust.Name										AS [Customer]
	, res.Duration									AS [ResourceDuration]
	, res.StartDateTime								AS [ResourceStartDateTime]  
	, DATEADD(d, res.Duration, res.StartDateTime)	AS [ResourceEndDateTime]  
	, res.EmployeeID								AS [EmployeeID]  
	, res.EquipmentTypeID							AS [EquipmentTypeID]
	, (ISNULL(emp.PersonID + ' - ', '') + ISNULL(emp.Name + ', ', '') + ISNULL(emp.FirstName, '') + ISNULL(' "-' + emp.Nickname + '"', ''))				
													AS [EmployeeName]                      
	, equip.Name									AS [EquipmentTypeName]   
FROM
	CS_Job job (NOLOCK)
	INNER JOIN CS_JobInfo jinfo (NOLOCK) ON jinfo.JobID = job.ID
	INNER JOIN CS_JobAction jact (NOLOCK) ON jinfo.JobActionID = jact.ID
	INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = jinfo.PriceTypeID
	INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = jinfo.JobTypeID
	INNER JOIN CS_JobDivision jdiv (NOLOCK) ON job.ID = jdiv.JobID AND jdiv.PrimaryDivision = 1
	INNER JOIN CS_Division div (NOLOCK) ON div.ID = jdiv.DivisionID AND div.IsGeneralLog = 0
	INNER JOIN CS_CustomerInfo  cinfo (NOLOCK) on jinfo.JobID = cinfo.JobId
	INNER JOIN CS_Customer cust (NOLOCK) ON cinfo.CustomerId = cust.ID AND cust.IsGeneralLog = 0
	INNER JOIN CS_Job_JobStatus jjstatus ON jjstatus.JobID = jinfo.JobID and jjstatus.Active = 1
	INNER JOIN CS_JobStatus jstatus ON jstatus.ID = jjstatus.JobStatusId
	INNER JOIN CS_LocationInfo jloc (NOLOCK) ON job.ID = jloc.JobID
	INNER JOIN CS_City city (NOLOCK) ON city.ID = jloc.CityID
	INNER JOIN CS_State state (NOLOCK) ON state.ID = jloc.StateID
	LEFT OUTER JOIN CS_PresetInfo pinfo (NOLOCK) ON jinfo.JobID = pinfo.JobId
	LEFT OUTER JOIN CS_Reserve res (NOLOCK) ON job.ID = res.JobID
	LEFT OUTER JOIN CS_Employee emp (NOLOCK) ON res.EmployeeID = emp.ID
	LEFT OUTER JOIN CS_EquipmentType equip (NOLOCK) ON res.EquipmentTypeID = equip.ID
	




GO


