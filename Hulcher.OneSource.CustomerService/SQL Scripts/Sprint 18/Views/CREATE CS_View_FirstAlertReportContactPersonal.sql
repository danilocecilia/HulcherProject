CREATE VIEW CS_View_FirstAlertReportContactPersonal
AS

SELECT
	 a.FirstAlertID					AS [FirstAlertID]
	,a.EmployeeID					AS [EmployeeID]
	,b.Name + ', ' + b.FirstName	AS [EmployeeName]
	,a.ContactID					AS [ContactID]
	,c.LastName + ', ' + c.Name		AS [ContactName]
	,a.EmailAdviseDate				AS [EmailAdviseDate]
	,a.VMXAdviseDate				AS [VMXAdviseDate]
	,a.InPersonAdviseDate			AS [InPersonADviseDate]
FROM
	 CS_FirstAlertContactPersonal a (NOLOCK)
		LEFT OUTER JOIN CS_Employee b (NOLOCK) on a.EmployeeID = b.ID
		LEFT OUTER JOIN CS_Contact c (NOLOCK) on a.ContactID = c.ID
WHERE
	a.Active = 1