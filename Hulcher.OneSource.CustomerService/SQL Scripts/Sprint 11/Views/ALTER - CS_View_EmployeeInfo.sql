ALTER VIEW [dbo].[CS_View_EmployeeInfo]  
AS      
  
SELECT DISTINCT
	 e.ID									AS [EmployeeId]  
	,e.Name + ', ' + e.FirstName			AS [EmployeeName]  
	,e.BusinessCardTitle					AS [Position]  
	,e.Active								AS [Active]  
	,d.ID									AS [DivisionId]  
	,d.Name									AS [DivisionName]  
	,s.ID									AS [StateId]  
	,s.Name									AS [State]  
	,r.JobID								AS [JobId]  
	,CASE  
		WHEN r.JobID is not null THEN   
		CASE   
			WHEN cl.ID IS NOT NULL THEN 'Transfer Available'   
			ELSE 'Assigned'   
		END   
		ELSE 'Available'   
	 END									AS [Assigned]  
	,CASE   
		WHEN j.Number = '' THEN j.Internal_Tracking   
		WHEN j.Number IS NULL THEN j.Internal_Tracking  
		ELSE j.Number   
	 END									AS [JobNumber]  
	,ptype.Acronym							AS [PriceTypeAcronym]  
	,jtype.[Description]					AS [JobTypeDescription]  
FROM  
	CS_Employee e (NOLOCK)      
		INNER JOIN CS_Division d (NOLOCK) ON e.DivisionID = d.ID  
			LEFT OUTER JOIN CS_State s (NOLOCK) ON d.StateId = s.ID AND d.CountryId = s.CountryID    
		LEFT OUTER JOIN CS_Resource r (NOLOCK) ON e.ID = r.EmployeeID AND r.Active = 1
			LEFT OUTER JOIN CS_CallLogResource cr (NOLOCK) ON (r.EmployeeID = cr.EmployeeID AND (r.EmployeeID IS NOT NULL AND cr.EmployeeID IS NOT NULL))  
				LEFT OUTER JOIN CS_CallLog cl (NOLOCK) ON cl.ID = cr.CallLogID AND cl.CallTypeID = 20  
			LEFT OUTER JOIN CS_Job j (NOLOCK) ON r.JobID = j.ID  
				LEFT OUTER JOIN CS_JobInfo ji (NOLOCK) ON j.ID = ji.JobID  
					LEFT OUTER JOIN CS_PriceType ptype ON ptype.ID = ji.PriceTypeID  
					LEFT OUTER JOIN CS_JobType jtype ON jtype.ID = ji.JobTypeID  
WHERE  
	e.Active = 1

