DROP VIEW CS_View_TurnoverActiveReport

GO

CREATE VIEW dbo.CS_View_TurnoverActiveReport  
AS  
  
SELECT    
 Job.ID AS [JobID]
 ,Div.ID AS [DivisionID]
 ,Div.Name AS [DivisionName]
 ,Job.Number AS [JobNumber]
 ,ptype.Acronym + jtype.Description + Job.Number AS [PrefixedNumber]
 ,Cust.ID AS [CustomerID]
 ,Cust.Name AS [CustomerName]
 ,state.Acronym + ', ' + state.Name AS [Location]
 ,Res.ID AS [ResourceID]
 ,CASE
	WHEN Res.EquipmentID IS NOT NULL THEN LTRIM(RTRIM(Equ.Description)) + ' - ' + LTRIM(RTRIM(Equ.Name))    
	ELSE LTRIM(RTRIM(Emp.Name)) + ', ' + LTRIM(RTRIM(Emp.FirstName))
  END AS [ResourceName]
 ,ISNULL(Res.Active, 0) AS [IsAdded]
 ,ROW_NUMBER() OVER ( PARTITION BY Div.Name,Job.Number, Res.Active ORDER BY Res.ID ) AS [RowGroup] 
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
 INNER JOIN CS_State state ON li.StateID = state.ID  
 LEFT OUTER JOIN CS_Resource Res ON Job.ID = Res.JobID  
 LEFT OUTER JOIN CS_Equipment Equ ON Res.EquipmentID = Equ.ID   
 LEFT OUTER JOIN CS_Employee Emp ON Res.EmployeeID = Emp.ID     
WHERE rJobStats.Active = 1    
 AND rJobStats.JobStatusId = 1 --ACTIVE STATUS  
 AND JobDiv.PrimaryDivision = 1    
 AND Job.ID <> 1 -- GENERAL LOG  
 AND Job.Active = 1  