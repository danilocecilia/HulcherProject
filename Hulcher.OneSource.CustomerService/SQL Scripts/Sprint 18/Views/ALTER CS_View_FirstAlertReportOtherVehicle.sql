ALTER VIEW dbo.CS_View_FirstAlertReportOtherVehicle  
AS  

SELECT  
	 fav.Make					AS [Make]
	,fav.Model					AS [Model]
	,fav.ID						AS [FirstAlertVehicleId]
	,fav.[Year]					AS [Year]
	,fav.Damage					AS [Damage]
	,fap.LastName				AS [LastName]
    ,fap.FirstName				AS [FirstName]
    ,fap.VehiclePosition		AS [VehiclePosition]
    ,fap.DriversLicenseNumber	AS [DriversLicenseNumber]
    ,fa.ID						AS [FirstAlertId]
    ,fav.Active					AS [Active]
    ,fav.IsHulcherVehicle		AS [IsHulcherVehicle]
    ,fap.[Address]				AS [Address]
FROM  
	dbo.CS_FirstAlertVehicle fav (NOLOCK)  
		INNER JOIN dbo.CS_FirstAlert fa (NOLOCK) ON fav.FirstAlertID = fa.ID AND fa.Active = 1   
			LEFT JOIN dbo.CS_FirstAlertPerson fap (NOLOCK) ON fav.ID = fap.FirstAlertVehicleID AND fa.ID = fap.FirstAlertID AND fap.Active = 1   AND fap.VehiclePosition = 2

