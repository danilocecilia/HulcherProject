ALTER VIEW dbo.CS_View_FirstAlertReportHulcherVehicles
AS

SELECT
	 eq.Name					AS [Name]
	,eq.Name					AS [Description]
	,eq.Make					AS [Make]
	,eq.Model					AS [Model]
	,fav.Damage					AS [Damage]
	,fav.EstimatedCost			AS [EstimatedCost]
	,''							AS [InsuranceCompany]
	,fav.FirstAlertID			AS [FirstAlertId]
	,fav.Active					AS [Active]
FROM
	 dbo.CS_FirstAlertVehicle fav (NOLOCK)
		INNER JOIN dbo.CS_Equipment eq (NOLOCK) ON fav.EquipmentID = eq.ID AND eq.Active = 1

