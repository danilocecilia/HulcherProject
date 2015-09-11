ALTER VIEW CS_View_Resource_CallLogInfo
AS         
  
SELECT DISTINCT  
	 Res.ID						AS [ResourceID]
	,Res.EmployeeID				AS [EmployeeID]
	,Res.EquipmentID			AS [EquipmentID] 
	,CRes.[Type]				AS [Type]
	,Emp.Name					AS [EmployeeName]
	,Emp.FirstName				AS [EmployeeFirstName]
	,Eq.Description				AS [EquipmentDescription]
	,Eq.Name					AS [EquipmentUnitNumber]
	,EqCombo.Name				AS [ComboName]
	,Div.ID						AS [DivisionID]
	,CASE
		WHEN ISNULL(Eqcd.ID, Empcd.ID) IS NULL THEN div.Name
		ELSE 'C ' + ISNULL(Eqcd.Name, Empcd.Name) + '/' + div.Name
	 END						AS [DivisionName] 
	,[Call].ID					AS [CallLogID]
	,[Call].CallDate			AS [CallDate]
	,[Call].ModifiedBy			AS [ModifiedBy]
	,CType.ID					AS [CallTypeID]
	,CType.[Description]        AS [CallTypeDescription]
	,Res.JobID					AS [JobID]
	,CAST(
		CASE  
			WHEN Res.EquipmentID IS NOT NULL AND Eq.Active = 1 AND Res.Active = 1 THEN 1  
			WHEN Res.EmployeeID IS NOT NULL AND Emp.Active = 1 AND Res.Active = 1 THEN 1   
			ELSE 0  
		END  
	 AS BIT)					AS [Active]  
FROM  
	CS_Resource Res (NOLOCK)  
	LEFT OUTER JOIN CS_Equipment Eq (NOLOCK) ON Res.EquipmentID = Eq.ID   
	LEFT OUTER JOIN CS_EquipmentCombo EqCombo (NOLOCK) ON Eq.ComboID = EqCombo.ID   
	LEFT OUTER JOIN CS_Employee Emp (NOLOCK) ON Res.EmployeeID = Emp.ID   
	LEFT OUTER JOIN CS_Division Div (NOLOCK) ON Div.ID = ISNULL(Eq.DivisionID, Emp.DivisionID)
	LEFT OUTER JOIN CS_EquipmentCoverage Eqc (NOLOCK) ON Eq.ID = Eqc.EquipmentID
	LEFT OUTER JOIN CS_Division Eqcd (NOLOCK) ON Eqc.DivisionID = Eqcd.ID
	LEFT OUTER JOIN CS_EmployeeCoverage Empc (NOLOCK) ON Emp.ID = Empc.EmployeeID
	LEFT OUTER JOIN CS_Division Empcd (NOLOCK) ON Empc.DivisionID = Empcd.ID
	LEFT OUTER JOIN CS_Job Job (NOLOCK) ON Res.JobID = Job.ID  
	LEFT OUTER JOIN CS_LocationInfo LInf (NOLOCK) ON Job.ID = LInf.JobID   
	LEFT OUTER JOIN CS_State Sta2 (NOLOCK) ON LInf.StateID = Sta2.ID   
	LEFT OUTER JOIN CS_City City (NOLOCK) ON LInf.CityID = City.ID   
	LEFT OUTER JOIN CS_Job_JobStatus JobStatus (NOLOCK) ON Job.ID = JobStatus.JobID AND JobStatus.Active = 1  
	LEFT OUTER JOIN CS_JobStatus [Stats] (NOLOCK) ON JobStatus.JobStatusID = [Stats].ID   
	LEFT OUTER JOIN  
	--SELECTS THE LAST ENTRY FOR EACH EQUIPMENT  
	(SELECT MAX(ID) AS ID, EquipmentID  
	FROM CS_CallLogResource  
	WHERE EquipmentID IS NOT NULL  
	GROUP BY EquipmentID ) TRes ON Eq.ID = TRes.EquipmentID   
	LEFT OUTER JOIN  
	--SELECTS THE LAST ENTRY FOR EACH EMPLOYEE  
	(SELECT MAX(ID) AS ID, EmployeeID  
	FROM CS_CallLogResource  
	WHERE EmployeeID IS NOT NULL  
	GROUP BY EmployeeID ) TRes2 ON Emp.ID = TRes2.EmployeeID   
	LEFT OUTER JOIN CS_CallLogResource CRes ON ISNULL(TRes.ID, TRes2.ID) = CRes.ID   
	LEFT OUTER JOIN CS_CallLog Call (NOLOCK) ON CRes.CallLogID = Call.ID   
	LEFT OUTER JOIN CS_CallType CType (NOLOCK) ON Call.CallTypeID = CType.ID  
  

