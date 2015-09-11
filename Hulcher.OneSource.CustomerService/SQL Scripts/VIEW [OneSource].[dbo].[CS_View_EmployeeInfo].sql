DROP VIEW CS_View_EmployeeInfo

GO

CREATE VIEW CS_View_EmployeeInfo    
AS    
    
SELECT    
 e.ID AS EmployeeId,    
 e.FirstName + ' ' + e.Name AS EmployeeName,    
 e.BusinessCardTitle AS Position,    
 e.Active AS Active,    
 d.ID AS DivisionId,    
 d.Name AS DivisionName,    
 s.ID AS StateId,    
 s.Name AS State,    
 r.JobID AS JobId,    
 CASE WHEN r.JobID is not null THEN 'Assigned' ELSE 'Available' END AS Assigned,    
 CASE WHEN js.Description = 'Active' THEN j.Number ELSE j.Internal_Tracking END AS JobNumber    
FROM    
 CS_Employee e (NOLOCK)    
 INNER JOIN CS_Division d (NOLOCK) ON e.DivisionID = d.ID AND e.Active = d.Active    
 LEFT OUTER JOIN CS_State s (NOLOCK) ON d.StateId = s.ID AND d.CountryId = s.CountryID AND d.Active = s.Active    
 LEFT OUTER JOIN CS_Resource r (NOLOCK) ON e.ID = r.EmployeeID AND e.Active = r.Active    
 LEFT OUTER JOIN CS_Job j (NOLOCK) ON r.JobID = j.ID AND r.Active = j.Active    
 LEFT OUTER JOIN CS_JobInfo ji (NOLOCK) ON j.ID = ji.JobID AND j.Active = ji.Active    
 LEFT OUTER JOIN CS_JobStatus js (NOLOCK) ON ji.JobStatusID = js.ID AND ji.Active = js.Active  
WHERE  
 e.Active = 1  
  