ALTER VIEW CS_View_SecondaryEquipmentInfo    
AS    
    
SELECT    
	Eq.ID AS [EquipmentID]
	,EqCombo.Name AS [ComboName]
	,CONVERT(BIT, 0) AS [IsCombo] -- IF FALSE, THIS UNIT IS PART OF A COMBO    
	,Eq.Name AS [UnitNumber]
	,Eq.Description AS [Descriptor]
	,Eq.Status AS [EquipmentStatus]
	,Div.ID AS [DivisionID]
	,CASE
		WHEN Eqcd.ID IS NULL THEN Div.Name
		ELSE 'C ' + Eqcd.Name + '/' + Div.Name
	 END AS [DivisionName]
	,Sta.Acronym AS [DivisionState]
	,Eq.Active AS [Active]
	,Job.ID AS [JobID]
	,CASE  
		WHEN Job.Number = '' THEN Job.Internal_Tracking            
		WHEN Job.Number IS NULL THEN Job.Internal_Tracking            
		ELSE Job.Number            
	END AS [JobNumber]
	,CASE  
		WHEN Job.Number = '' THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking            
		WHEN Job.Number IS NULL THEN ptype.Acronym + jtype.Description + Job.Internal_Tracking            
		ELSE ptype.Acronym + jtype.Description + Job.Number            
	END AS [PrefixedNumber]
	,CASE    
		WHEN Job.ID IS NULL THEN 'Available'    
		ELSE 'Assigned'    
	END AS [Status]
	,CASE    
		WHEN LInf.JobID IS NOT NULL THEN CASE City.CSRecord WHEN 1 THEN '* ' + City.Name ELSE City.Name END + ', ' + Sta2.Acronym    
		ELSE ''    
	END AS [JobLocation]
	,ISNULL(Call.ID, 0) AS [CallLogID]
	,CType.Description  AS [Type]
	,ComboID AS [ComboID]
	,CASE    
	WHEN Eq.ID = EqCombo.PrimaryEquipmentID THEN CONVERT(BIT, 1)    
	ELSE CONVERT(BIT, 0)    
	END AS [IsPrimary]    
 FROM CS_Equipment Eq (NOLOCK)
	LEFT OUTER JOIN CS_EquipmentCombo EqCombo (NOLOCK) ON Eq.ComboID = EqCombo.ID
	LEFT OUTER JOIN CS_Division Div (NOLOCK) ON Eq.DivisionID = Div.ID
	LEFT OUTER JOIN CS_EquipmentCoverage Eqc (NOLOCK) ON Eq.ID = Eqc.EquipmentID
	LEFT OUTER JOIN CS_Division Eqcd (NOLOCK) ON Eqc.DivisionID = Eqcd.ID
	LEFT OUTER JOIN CS_State Sta (NOLOCK) ON Div.StateID = Sta.ID
	LEFT OUTER JOIN CS_Resource Res (NOLOCK) ON Eq.ID = Res.EquipmentID
	LEFT OUTER JOIN CS_Job Job (NOLOCK) ON Res.JobID = Job.ID
	LEFT OUTER JOIN CS_LocationInfo LInf (NOLOCK) ON Job.ID = LInf.JobID
	LEFT OUTER JOIN CS_State Sta2 (NOLOCK) ON LInf.StateID = Sta2.ID
	LEFT OUTER JOIN CS_City City (NOLOCK) ON LInf.CityID = City.ID
	LEFT OUTER JOIN CS_JobInfo Inf (NOLOCK) ON Job.ID = Inf.JobID
	LEFT OUTER JOIN CS_Job_JobStatus JobStats (NOLOCK) ON Inf.JobID = JobStats.JobID AND JobStats.Active = 1
	LEFT OUTER JOIN CS_JobStatus Stats (NOLOCK) ON JobStats.JobStatusID = Stats.ID
		LEFT OUTER JOIN CS_JobType jtype (NOLOCK) ON Inf.JobTypeID = jtype.ID
	LEFT OUTER JOIN CS_PriceType ptype (NOLOCK) ON Inf.PriceTypeID = ptype.ID
	LEFT OUTER JOIN --SELECTS THE LAST ENTRY FOR EACH EQUIPMENT    
		(SELECT MAX(ID) AS ID, EquipmentID    
		FROM CS_CallLogResource    
		WHERE EquipmentID IS NOT NULL    
		GROUP BY EquipmentID ) TRes ON Eq.ID = TRes.EquipmentID
	LEFT OUTER JOIN CS_CallLogResource CRes ON TRes.ID = CRes.ID
	LEFT OUTER JOIN CS_CallLog Call (NOLOCK) ON CRes.CallLogID = Call.ID
	LEFT OUTER JOIN CS_CallType CType (NOLOCK) ON Call.CallTypeID = CType.ID    
WHERE
	Eq.ComboID IS NOT NULL  

