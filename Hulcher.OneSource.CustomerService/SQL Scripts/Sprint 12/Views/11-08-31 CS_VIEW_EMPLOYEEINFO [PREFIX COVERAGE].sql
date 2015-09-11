DROP VIEW CS_View_EmployeeInfo

GO
  
CREATE VIEW CS_View_EmployeeInfo  
AS          
      
SELECT DISTINCT    
 e.ID      AS [EmployeeId]      
 ,e.Name + ', ' + e.FirstName   AS [EmployeeName]      
 ,e.BusinessCardTitle     AS [Position]      
 ,e.Active     AS [Active]      
 ,d.ID      AS [DivisionId]      
 ,CASE
	WHEN cd.ID IS NULL THEN d.Name
	ELSE 'C ' + cd.Name + '/' + d.Name
  END AS [DivisionName]    
 ,s.ID      AS [StateId]      
 ,s.Name      AS [State]      
 ,r.JobID     AS [JobId]      
 ,CASE  
  WHEN r.JobID IS NOT NULL THEN 'Assigned'       
  ELSE 'Available'       
  END       AS [Assigned]      
 ,CASE       
  WHEN j.Number = '' THEN j.Internal_Tracking       
  WHEN j.Number IS NULL THEN j.Internal_Tracking      
  ELSE j.Number       
 END       AS [JobNumber]   
 ,CASE       
  WHEN j.Number = '' THEN ptype.Acronym + jtype.Description + j.Internal_Tracking       
  WHEN j.Number IS NULL THEN ptype.Acronym + jtype.Description + j.Internal_Tracking      
  ELSE ptype.Acronym + jtype.Description + j.Number       
 END       AS [PrefixedNumber]   
 ,ptype.Acronym    AS [PriceTypeAcronym]      
 ,jtype.[Description]     AS [JobTypeDescription]      
FROM      
 CS_Employee e (NOLOCK)          
 INNER JOIN CS_Division d (NOLOCK) ON e.DivisionID = d.ID
 LEFT OUTER JOIN CS_EmployeeCoverage ec (NOLOCK) ON e.ID = ec.EmployeeID
 LEFT OUTER JOIN CS_Division cd (NOLOCK) ON ec.DivisionID = cd.ID     
 LEFT OUTER JOIN CS_State s (NOLOCK) ON d.StateId = s.ID AND d.CountryId = s.CountryID        
 LEFT OUTER JOIN CS_Resource r (NOLOCK) ON e.ID = r.EmployeeID AND r.Active = 1    
 LEFT OUTER JOIN CS_Job j (NOLOCK) ON r.JobID = j.ID      
 LEFT OUTER JOIN CS_JobInfo ji (NOLOCK) ON j.ID = ji.JobID      
 LEFT OUTER JOIN CS_PriceType ptype ON ptype.ID = ji.PriceTypeID      
 LEFT OUTER JOIN CS_JobType jtype ON jtype.ID = ji.JobTypeID      
WHERE      
 e.Active = 1 