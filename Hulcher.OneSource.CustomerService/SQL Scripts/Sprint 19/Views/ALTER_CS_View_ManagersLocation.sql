ALTER VIEW [dbo].[CS_View_ManagersLocation]    
AS 

SELECT DISTINCT        
	emp.ID  				  AS [EmployeeID]
	, emp.Name			  AS [EmployeeName]
	, emp.FirstName		  AS [EmployeeFirstName]
	, emp.Nickname		  AS [EmployeeNickName]
	, CASE
		WHEN Job.ID IS NULL THEN NULL 
		ELSE [HCall].Xml
	  END       		  AS [LastHotelXML]
	, CASE
		WHEN Job.ID IS NULL THEN NULL
		ELSE [HCall].Note
	  END       		  AS [LastHotelNote]
	, [Call].ID			  AS [LastCallLogID]
	, [Call].CallDate	  AS [LastCallLogDate]
	, [Call].JobID	      AS [LastCallJobID]
	, CType.ID			  AS [LastCallTypeID]
	, CType.Description   AS [LastCallTypeDescription]
	, Job.ID			  AS [JobID]
	, ISNULL(CASE        
		WHEN Job.Number = '' THEN Job.Internal_Tracking         
		WHEN Job.Number IS NULL THEN Job.Internal_Tracking         
		ELSE Job.Number         
	  END, 'Home/Shop')   AS [JobNumber]     
	, ISNULL(CASE        
	   WHEN Job.Number = '' THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking         
	   WHEN Job.Number IS NULL THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking         
	   ELSE ptype.Acronym + jtype.Description + Job.Number         
      END, 'Home/Shop')   AS [PrefixedNumber]
	, [Stats].Description AS [JobStatus]
 FROM        
	CS_Employee emp (NOLOCK) 
	LEFT OUTER JOIN CS_Resource Res (NOLOCK) 
	ON emp.ID = Res.EmployeeID AND Res.Active = 1        
	LEFT OUTER JOIN CS_Job Job (NOLOCK) 
	ON Res.JobID = Job.ID AND Job.Active = 1  
	LEFT OUTER JOIN CS_JobInfo Inf (NOLOCK) 
	ON Job.ID = Inf.JobID        
	LEFT OUTER JOIN CS_Job_JobStatus JobStats (NOLOCK) 
	ON Job.ID = JobStats.JobID AND JobStats.Active = 1 
	LEFT OUTER JOIN CS_JobStatus [Stats] (NOLOCK) 
	ON JobStats.JobStatusID = [Stats].ID    
	LEFT OUTER JOIN CS_PriceType ptype (NOLOCK) 
	ON ptype.ID = Inf.PriceTypeID        
	LEFT OUTER JOIN CS_JobType jtype (NOLOCK) 
	ON jtype.ID = Inf.JobTypeID   
	LEFT OUTER JOIN         
	(        
		SELECT MAX(ID) AS ID, EmployeeID        
		FROM CS_CallLogResource (NOLOCK)        
		WHERE EmployeeID IS NOT NULL AND Active = 1        
		GROUP BY EmployeeID         
	) TRes 
	ON emp.ID = TRes.EmployeeID        
	LEFT OUTER JOIN CS_CallLogResource (NOLOCK) CRes 
	ON TRes.ID = CRes.ID AND CRes.Active = 1        
	LEFT OUTER JOIN CS_CallLog [Call] (NOLOCK) 
	ON CRes.CallLogID = [Call].ID AND [Call].Active = 1        
	LEFT OUTER JOIN CS_CallType CType (NOLOCK) 
	ON [Call].CallTypeID = CType.ID 
	LEFT OUTER JOIN         
	(        
		SELECT MAX(CallLogID) AS CallLogID, EmployeeID        
		FROM CS_CallLogResource A (NOLOCK)
		LEFT JOIN CS_CallLog B (NOLOCK) ON A.CallLogID = B.ID
		WHERE EmployeeID IS NOT NULL AND A.Active = 1 AND B.CallTypeID = 32 AND B.Active = 1
		GROUP BY EmployeeID         
	) HRes 
	ON emp.ID = HRes.EmployeeID 
	LEFT OUTER JOIN CS_CallLog HCall (NOLOCK) 
	ON HRes.CallLogID = HCall.ID AND HCall.Active = 1
WHERE
	emp.IsKeyPerson = 1

