CREATE VIEW OneSource_EquipmentPermit
AS

SELECT
	 [Permit].[ID]
	,[Permit].[UnitID]
	,[State].[Code]
	,[Permit].[Type]
	,[Permit].[LicenseNumber]
	,[Permit].[IssueDate]
	,[Permit].[ExpirationDate]
	,[Permit].[BuiltDate]
	,CHECKSUM(
		 [Permit].[ID]
		,[Permit].[UnitID]
		,[State].[Code]
		,[Permit].[Type]
		,[Permit].[LicenseNumber]
		,[Permit].[IssueDate]
		,[Permit].[ExpirationDate]
		,[Permit].[BuiltDate]
	 ) AS [Checksum]
FROM
	 [CurrentHSI].[dbo].[LicensePermit] AS [Permit]
	 LEFT OUTER JOIN [CurrentHSI].[dbo].[State] AS [State] ON [Permit].[StateID] = [State].[ID]