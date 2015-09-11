DROP VIEW CS_View_FirstAlertReport

GO

CREATE VIEW CS_View_FirstAlertReport  
AS  
SELECT
	e.Name AS [UnitNumber]
	,faVehicle.EquipmentID AS [EquipmentID]
	,e.Description AS [Description]
	,e.Make AS [HulcherEquipmentMake]
	,e.Model AS [HulcherEquipmentModel]
	,faVehicle.Make AS [VehicleMake]
	,faVehicle.Model AS [VehicleModel]
	,faVehicle.Year AS [Year]
	,faVehicle.Damage AS [Damage]
	,faVehicle.EstimatedCost AS [EstimatedCost]
	,faPerson.InsuranceCompany AS [InsuranceCompany]
	,faPerson.PolicyNumber AS [PolicyNumber]
	,fa.HasPoliceReport AS [HasPoliceReport]
	,fa.PoliceAgency AS [PoliceAgency]
	,fa.PoliceReportNumber AS [PoliceReportNumber]
	,faPerson.DrugScreenRequired AS [DrugScreenRequired]
	,faPerson.LastName AS [LastName]
	,faPerson.FirstName AS [FirstName]
	,faPerson.Address AS [Address]
	,faPerson.DriversLicenseNumber AS [DriversLicenseNumber]
	,faPerson.VehiclePosition AS [VehiclePosition]
	,faPerson.InjuryNature AS [InjuryNature]
	,faPerson.InjuryBodyPart AS [InjuryBodyPart]
	,fa.ID AS [FirstAlertId]
	,faVehicle.IsHulcherVehicle AS [IsHulcherVehicle]
	,faVehicle.Active AS [Active]
	,faVehicle.ID AS [FirstAlertVehicleID]
FROM
	CS_FirstAlertVehicle faVehicle
	INNER JOIN CS_FirstAlert fa ON faVehicle.FirstAlertID = fa.ID
	INNER JOIN CS_FirstAlertPerson faPerson ON faPerson.FirstAlertVehicleID = faVehicle.ID
	LEFT OUTER JOIN CS_Equipment e ON faVehicle.EquipmentID = e.ID  