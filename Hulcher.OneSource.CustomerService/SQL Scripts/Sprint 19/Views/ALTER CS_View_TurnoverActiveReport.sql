USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_TurnoverActiveReport]    Script Date: 01/12/2012 15:52:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[CS_View_TurnoverActiveReport]  
AS  

SELECT    
 Job.ID AS [JobID]
 ,Div.ID AS [DivisionID]
 ,Div.Name AS [DivisionName]
 ,Job.Number AS [JobNumber]
 ,ptype.Acronym + jtype.Description + Job.Number AS [PrefixedNumber]
 ,Cust.ID AS [CustomerID]
 ,Cust.Name AS [CustomerName]
 , CASE cty.CSRecord WHEN 1 THEN '* ' + cty.Name ELSE cty.Name END + ', ' + state.Acronym AS [Location]
 ,Res.ID AS [ResourceID]
 ,CASE
	WHEN Res.EquipmentID IS NOT NULL THEN 
		CASE WHEN combo.ID IS NOT NULL 
			THEN LTRIM(RTRIM(combo.Name)) + ' - ' + LTRIM(RTRIM(Equ.Name))
			ELSE LTRIM(RTRIM(Equ.Name)) 
		END
	ELSE 
		LTRIM(RTRIM(Emp.Name)) + ', ' + LTRIM(RTRIM(Emp.FirstName))
  END AS [ResourceName]
 , 
	CAST (CASE WHEN [Call].ID IS NULL THEN 1 ELSE
	CASE WHEN [Call].CallTypeID = 20 THEN 0 ELSE 1 END
	END AS BIT)AS [IsAdded] 
 ,ISNULL(Emp.IsKeyPerson, 0) AS [IsKeyPerson]
 ,ROW_NUMBER() OVER ( PARTITION BY Div.Name,Job.Number, Emp.IsKeyPerson ORDER BY Res.ID ) AS [RowGroup]
FROM   
 CS_Job Job  
 INNER JOIN CS_Job_JobStatus rJobStats ON Job.ID = rJobStats.JobID   
 INNER JOIN CS_JobDivision JobDiv ON Job.ID = JobDiv.JobID   
 INNER JOIN CS_Division Div ON JobDiv.DivisionID = Div.ID   
 INNER JOIN CS_JobInfo Info ON Job.ID = Info.JobID
 INNER JOIN CS_PriceType ptype ON Info.PriceTypeID = ptype.ID
 INNER JOIN CS_JobType jtype ON Info.JobTypeID = jtype.ID   
 INNER JOIN CS_CustomerInfo JobCust ON Job.ID = JobCust.JobID   
 INNER JOIN CS_Customer Cust ON JobCust.CustomerId = Cust.ID  
 INNER JOIN CS_LocationInfo li ON Job.ID = li.JobID  
 INNER JOIN CS_City cty ON li.CityID = cty.ID  
 INNER JOIN CS_State state ON li.StateID = state.ID  
 INNER JOIN CS_Resource Res ON Job.ID = Res.JobID AND Res.Active = 1
 LEFT OUTER JOIN CS_Equipment Equ ON Res.EquipmentID = Equ.ID   
 LEFT OUTER JOIN CS_EquipmentCombo combo ON Equ.ComboID = combo.ID
 LEFT OUTER JOIN CS_Employee Emp ON Res.EmployeeID = Emp.ID 
 LEFT OUTER JOIN         
  (        
   SELECT MAX(ID) AS ID, EquipmentID, EmployeeID
   FROM CS_CallLogResource        
   WHERE (EquipmentID IS NOT NULL OR EmployeeID IS NOT NULL)AND Active = 1        
   GROUP BY EquipmentID, EmployeeID
  ) TRes ON (Emp.ID IS NULL AND Equ.ID = TRes.EquipmentID) OR (Equ.ID IS NULL AND Emp.ID = TRes.EmployeeID)
 LEFT OUTER JOIN CS_CallLogResource CRes ON TRes.ID = CRes.ID AND CRes.Active = 1
 LEFT OUTER JOIN CS_CallLog [Call] (NOLOCK) ON CRes.CallLogID = [Call].ID AND [Call].Active = 1 AND (Call.CallTypeID IN (20, 13))
WHERE rJobStats.Active = 1    
 AND rJobStats.JobStatusId = 1 --ACTIVE STATUS  
 AND JobDiv.PrimaryDivision = 1    
 AND Job.ID <> 1 -- GENERAL LOG  
 AND Job.Active = 1
 AND (
		(Res.EmployeeID IS NOT NULL AND Emp.IsKeyPerson = 1) 
		OR (Res.EquipmentID IS NOT NULL AND Equ.HeavyEquipment = 1)
	 ) 


GO


