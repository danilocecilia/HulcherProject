CREATE VIEW [OneSource_Equipment]  
AS  
SELECT  
 [Unit].[ID] AS [EquipmentID]  
 ,[Unit].[UnitNumber] AS [EquipmentNumber]  
 ,SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber])) AS [EquipmentTypeNumber]  
 ,CASE SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber]))  
   WHEN 'J' THEN [Category].[Name]  
   ELSE RTRIM(LTRIM(REPLACE([Category].[Name], SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber])), '')))  
  END [Name]  
 ,CASE  LEFT([Site].[Name], 3)   
      WHEN '888' THEN LEFT([UnitSpecC].[SpecValue], 3)  
      ELSE LEFT([Site].[Name], 3)  
    END [DivisionName]  
 ,[Unit].[LicensePlate] AS [LicensePlate]
 ,[Unit].[UnitSerialNumber] AS [SerialNumber]
 ,[Unit].[MfgYear] AS [Year]
 ,[Unit].[Notes] AS [Notes]
 ,[UnitSpecA].[SpecValue] AS [Description]  
 ,[UnitSpecB].[SpecValue] AS [Status]
 ,[UnitSpecD].[SpecValue] AS [BodyType]
 ,[UnitSpecE].[SpecValue] AS [Make]
 ,[UnitSpecF].[SpecValue] AS [Model]
 ,[UnitSpecG].[SpecValue] AS [Function]
 ,[UnitSpecH].[SpecValue] AS [AssignedTo]
 ,[UnitSpecI].[SpecValue] AS [RegisteredState]
 ,[UnitSpecJ].[SpecValue] AS [AttachPanelBoss]
 ,[UnitSpecK].[SpecValue] AS [AttachPileDriver]
 ,[UnitSpecL].[SpecValue] AS [AttachSlipSheet]
 ,[UnitSpecM].[SpecValue] AS [AttachTieClamp]
 ,[UnitSpecN].[SpecValue] AS [AttachTieInserter]
 ,[UnitSpecO].[SpecValue] AS [AttachTieTamper]
 ,[UnitSpecP].[SpecValue] AS [AttachUndercutter]
 ,CHECKSUM(  
    [Unit].[ID]  
   ,[Unit].[UnitNumber]  
   ,SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber]))  
    ,CASE SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber]))  
     WHEN 'J' THEN [Category].[Name]  
     ELSE RTRIM(LTRIM(REPLACE([Category].[Name], SUBSTRING([Unit].[UnitNumber], 0, CHARINDEX('-', [Unit].[UnitNumber])), '')))  
    END  
   ,CASE  LEFT([Site].[Name], 3)   
        WHEN '888' THEN LEFT([UnitSpecC].[SpecValue], 3)  
        ELSE LEFT([Site].[Name], 3)  
       END
     ,[Unit].[LicensePlate]
	 ,[Unit].[UnitSerialNumber]
	 ,[Unit].[MfgYear]
	 ,CONVERT(VARCHAR(8000), [Unit].[Notes])
     ,[UnitSpecA].[SpecValue]  
	 ,[UnitSpecB].[SpecValue]
	 ,[UnitSpecD].[SpecValue]
	 ,[UnitSpecE].[SpecValue]
	 ,[UnitSpecF].[SpecValue]
	 ,[UnitSpecG].[SpecValue]
	 ,[UnitSpecH].[SpecValue]
	 ,[UnitSpecI].[SpecValue]
	 ,[UnitSpecJ].[SpecValue]
	 ,[UnitSpecK].[SpecValue]
	 ,[UnitSpecL].[SpecValue]
	 ,[UnitSpecM].[SpecValue]
	 ,[UnitSpecN].[SpecValue]
	 ,[UnitSpecO].[SpecValue]
	 ,[UnitSpecP].[SpecValue]) AS Checksum  
FROM   
 [CurrentHSI].[dbo].[Unit] INNER JOIN [CurrentHSI].[dbo].[Site]  
 ON [Unit].[SiteId] = [Site].[Id] LEFT JOIN [CurrentHSI].[dbo].[UnitSpec] UnitSpecA  
 ON [Unit].[Id] = [UnitSpecA].[UnitId]
 AND UnitSpecA.SpecName LIKE 'Equipment Func.' LEFT OUTER JOIN [CurrentHSI].[dbo].[UnitSpec] UnitSpecB  
 ON [Unit].[Id] = [UnitSpecB].[UnitId]
 AND UnitSpecB.SpecName LIKE 'Status' LEFT OUTER JOIN [CurrentHSI].[dbo].[Category]   
 ON [Unit].[Category] = [Category].[Name] LEFT OUTER JOIN  [UnitSpec] UnitSpecC  
 ON [Unit].[Id] = [UnitSpecC].[UnitId]  
 AND [UnitSpecC].[SpecName] = 'Location' LEFT OUTER JOIN [UnitSpec] UnitSpecD
 ON [Unit].[Id] = [UnitSpecD].[UnitId]
 AND [UnitSpecD].[SpecName] = 'Body Type' LEFT OUTER JOIN [UnitSpec] UnitSpecE
 ON [Unit].[Id] = [UnitSpecE].[UnitId]
 AND [UnitSpecE].[SpecName] = 'Vehicle Make' LEFT OUTER JOIN [UnitSpec] UnitSpecF
 ON [Unit].[Id] = [UnitSpecF].[UnitId]
 AND [UnitSpecF].[SpecName] = 'Vehicle Model' LEFT OUTER JOIN [UnitSpec] UnitSpecG
 ON [Unit].[Id] = [UnitSpecG].[UnitId]
 AND [UnitSpecG].[SpecName] = 'Equipment Func.' LEFT OUTER JOIN [UnitSpec] UnitSpecH
 ON [Unit].[Id] = [UnitSpecH].[UnitId]
 AND [UnitSpecH].[SpecName] = 'Assigned To' LEFT OUTER JOIN [UnitSpec] UnitSpecI
 ON [Unit].[Id] = [UnitSpecI].[UnitId]
 AND [UnitSpecI].[SpecName] = 'State Registered' LEFT OUTER JOIN [UnitSpec] UnitSpecJ
 ON [Unit].[Id] = [UnitSpecJ].[UnitId]
 AND [UnitSpecJ].[SpecName] = 'Attach Panel Boss' LEFT OUTER JOIN [UnitSpec] UnitSpecK
 ON [Unit].[Id] = [UnitSpecK].[UnitId]
 AND [UnitSpecK].[SpecName] = 'Attach Pile Driver' LEFT OUTER JOIN [UnitSpec] UnitSpecL
 ON [Unit].[Id] = [UnitSpecL].[UnitId]
 AND [UnitSpecL].[SpecName] = 'Attach Slip Sheet' LEFT OUTER JOIN [UnitSpec] UnitSpecM
 ON [Unit].[Id] = [UnitSpecM].[UnitId]
 AND [UnitSpecM].[SpecName] = 'Attach Tie Clamp' LEFT OUTER JOIN [UnitSpec] UnitSpecN
 ON [Unit].[Id] = [UnitSpecN].[UnitId]
 AND [UnitSpecN].[SpecName] = 'Attach Tie Inserter' LEFT OUTER JOIN [UnitSpec] UnitSpecO
 ON [Unit].[Id] = [UnitSpecO].[UnitId]
 AND [UnitSpecO].[SpecName] = 'Attach Tie Tamper' LEFT OUTER JOIN [UnitSpec] UnitSpecP
 ON [Unit].[Id] = [UnitSpecP].[UnitId]
 AND [UnitSpecP].[SpecName] = 'Attach Undercutter'
WHERE   
 [Deleted] = 0  
 AND LEFT([Site].[Name], 3) NOT IN ('999', '000')  

