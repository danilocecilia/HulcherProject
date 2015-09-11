DROP VIEW CS_View_Resource_CallLogInfo

GO

CREATE VIEW CS_View_Resource_CallLogInfo
AS       

SELECT DISTINCT
 Res.ID AS ResourceID,
 Res.EmployeeID AS EmployeeID,
 Res.EquipmentID AS EquipmentID,
 CRes.Type AS Type,
 Emp.Name AS EmployeeName,
 Emp.FirstName AS EmployeeFirstName,
 Eq.Description AS EquipmentDescription,
 EqCombo.Name AS ComboName,
 CASE
	WHEN Res.EquipmentID IS NOT NULL THEN Div.ID
	WHEN Res.EmployeeID IS NOT NULL THEN Div2.ID
 END AS DivisionID,
 CASE
	WHEN Res.EquipmentID IS NOT NULL THEN Div.Name
	WHEN Res.EmployeeID IS NOT NULL THEN Div2.Name
 END AS DivisionName,
 Call.CallDate AS CallDate,
 Call.ModifiedBy AS ModifiedBy,
 CType.ID AS CAllTypeID,
 CType.Description AS CallTypeDescription,
 Res.JobID AS JobID,
 CASE
	WHEN Res.EquipmentID IS NOT NULL THEN Eq.Active
	WHEN Res.EmployeeID IS NOT NULL THEN Emp.Active
 END AS Active
 FROM CS_Resource Res (NOLOCK) LEFT OUTER JOIN CS_Equipment Eq (NOLOCK)
 ON Res.EquipmentID = Eq.ID LEFT OUTER JOIN CS_Employee Emp (NOLOCK)
 ON Res.EmployeeID = Emp.ID LEFT OUTER JOIN CS_EquipmentCombo EqCombo (NOLOCK)
 ON Eq.ComboID = EqCombo.ID LEFT OUTER JOIN CS_Division Div (NOLOCK)
 ON Eq.DivisionID = Div.ID LEFT OUTER JOIN CS_Division Div2 (NOLOCK)
 ON Emp.DivisionID = Div2.ID LEFT OUTER JOIN CS_Job Job (NOLOCK)
 ON Res.JobID = Job.ID LEFT OUTER JOIN CS_LocationInfo LInf (NOLOCK)
 ON Job.ID = LInf.JobID LEFT OUTER JOIN CS_State Sta2 (NOLOCK)
 ON LInf.StateID = Sta2.ID LEFT OUTER JOIN CS_City City (NOLOCK)
 ON LInf.CityID = City.ID LEFT OUTER JOIN CS_JobInfo Inf (NOLOCK)
 ON Job.ID = Inf.JobID LEFT OUTER JOIN CS_JobStatus Stats (NOLOCK)
 ON Inf.JobStatusID = Stats.ID LEFT OUTER JOIN
 --SELECTS THE LAST ENTRY FOR EACH EQUIPMENT
 (SELECT MAX(ID) AS ID, EquipmentID
	FROM CS_CallLogResource
	WHERE EquipmentID IS NOT NULL
	GROUP BY EquipmentID ) TRes 
 ON Eq.ID = TRes.EquipmentID LEFT OUTER JOIN
 --SELECTS THE LAST ENTRY FOR EACH EMPLOYEE
 (SELECT MAX(ID) AS ID, EmployeeID
	FROM CS_CallLogResource
	WHERE EmployeeID IS NOT NULL
	GROUP BY EmployeeID ) TRes2
 ON Emp.ID = TRes2.EmployeeID LEFT OUTER JOIN CS_CallLogResource CRes
 ON ISNULL(TRes.ID, TRes2.ID) = CRes.ID LEFT OUTER JOIN CS_CallLog Call (NOLOCK)
 ON CRes.CallLogID = Call.ID LEFT OUTER JOIN CS_CallType CType (NOLOCK)
 ON Call.CallTypeID = CType.ID
