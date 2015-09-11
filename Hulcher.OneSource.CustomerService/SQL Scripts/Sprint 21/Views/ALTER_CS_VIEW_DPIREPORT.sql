USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_DPIReport]    Script Date: 02/23/2012 14:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CS_View_DPIReport]          
AS  
  
SELECT  
 dpi.ID        AS [DPIID]          
 ,job.ID        AS [JobID]        
 ,dpi.IsContinuing     AS [IsContinuing]        
 ,dpi.Date       AS [Date]          
 ,division.Name      AS [Division]         
 ,SUM(CASE  
   WHEN equipment.ID IS NULL THEN 0  
   ELSE 1  
   END) AS [EQ]      
 ,CASE  
  WHEN job.Number = '' THEN job.Internal_Tracking            
  WHEN job.Number IS NULL THEN job.Internal_Tracking            
  ELSE job.Number            
  END        AS [JobNumber] 
 ,CASE  
  WHEN job.Number = '' THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  WHEN job.Number IS NULL THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  ELSE ptype.Acronym + jtype.Description + job.Number            
  END        AS [PrefixedNumber]          
 ,customer.Name      AS [Customer]          
 ,st.Acronym + ', ' + CASE cty.CSRecord WHEN 1 THEN '* ' + cty.Name ELSE cty.Name END AS [Location]          
 ,jAction.Description             AS [JobAction]          
 ,jDescription.NumberEngines      AS [Engine]          
 ,jDescription.NumberEmpties      AS [Empties]          
 ,jDescription.NumberLoads        AS [Loads]          
 ,CASE jjStatus.JobStatusID          
  WHEN 6 THEN 'DONE'          
  ELSE CASE dpi.CalculationStatus          
    WHEN 1 THEN 'INSF'          
    ELSE 'CONT'          
    END           
  END        AS [Status]      
 ,dpi.FirstETA      AS [ETA]       
 ,dpi.FirstATA      AS [ATA]          
 ,ptype.Acronym      AS [PriceTypeAcronym]            
 ,jtype.Description     AS [JobTypeDescription]          
 ,dpi.Total       AS [RevenueTotal]  
 , CAST(CASE WHEN jstaa.ID IS NULL THEN 0 ELSE 1 END AS BIT) as [WasActiveToday]  
 , jjStatus.JobStatusId 
FROM  
 cs_dpi dpi (NOLOCK)       
 INNER JOIN CS_Job job (NOLOCK) ON job.ID = dpi.JobID AND job.Active = 1    
 INNER JOIN CS_JobInfo jInfo (NOLOCK) ON jInfo.JobID = job.ID  AND job.Active = 1        
 INNER JOIN CS_JobAction jAction (NOLOCK) ON jAction.ID = jInfo.JobActionID      
 INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = jInfo.PriceTypeID    
 INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = jInfo.JobTypeID    
 INNER JOIN CS_JobDivision jobDiv (NOLOCK) ON job.ID = jobDiv.JobID AND jobDiv.PrimaryDivision = 1    
 INNER JOIN CS_Division division (NOLOCK) ON division.ID = jobDiv.DivisionID    
 INNER JOIN CS_CustomerInfo cInfo (NOLOCK) ON cInfo.JobId = job.ID    
 INNER JOIN CS_Customer customer (NOLOCK) ON customer.ID = cInfo.CustomerId             
 INNER JOIN CS_LocationInfo location (NOLOCK) ON location.JobID = job.ID          
 INNER JOIN CS_State st (NOLOCK) ON location.StateID = st.ID            
 INNER JOIN CS_City cty (NOLOCK) ON location.CityID = cty.ID            
 INNER JOIN CS_JobDescription jDescription (NOLOCK) ON jDescription.JobId = job.ID    
 INNER JOIN CS_Job_JobStatus jjStatus (NOLOCK) ON jjStatus.JobID = job.ID AND jjStatus.Active = 1  
 INNER JOIN CS_JobStatus jStatus (NOLOCK) ON jjStatus.JobStatusId = jStatus.ID    
 LEFT OUTER JOIN CS_Resource resource (NOLOCK) ON resource.JobID = job.ID AND resource.Active = 1 AND resource.EmployeeID IS NULL    
 LEFT OUTER JOIN CS_Equipment equipment (NOLOCK) ON resource.EquipmentID = equipment.ID AND equipment.HeavyEquipment = 1    
 LEFT JOIN CS_Job_JobStatus jstaa (NOLOCK) ON  
 jstaa.JobStatusId = 1 
 AND job.ID = jstaa.JobID 
 AND CONVERT(DATE, jstaa.JobStartDate) = CONVERT(DATE, GETDATE()) 
GROUP BY       
 dpi.ID      
 ,job.ID        
 ,dpi.IsContinuing      
 ,dpi.Date      
 ,division.Name      
 ,CASE  
  WHEN job.Number = '' THEN job.Internal_Tracking            
  WHEN job.Number IS NULL THEN job.Internal_Tracking            
  ELSE job.Number            
  END 
 ,CASE  
  WHEN job.Number = '' THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  WHEN job.Number IS NULL THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  ELSE ptype.Acronym + jtype.Description + job.Number            
  END      
 ,customer.Name       
 ,st.Acronym + ', ' + CASE cty.CSRecord WHEN 1 THEN '* ' + cty.Name ELSE cty.Name END
 ,jAction.Description          
 ,jDescription.NumberEngines         
 ,jDescription.NumberEmpties          
 ,jDescription.NumberLoads 
 ,CASE jjStatus.JobStatusID          
  WHEN 6 THEN 'DONE'          
  ELSE CASE dpi.CalculationStatus          
    WHEN 1 THEN 'INSF'          
    ELSE 'CONT'          
    END           
  END                        
 ,dpi.FirstETA      
 ,dpi.FirstATA          
 ,ptype.Acronym                   
 ,jtype.Description                
 ,dpi.Total
 , CAST(CASE WHEN jstaa.ID IS NULL THEN 0 ELSE 1 END AS BIT) 
 ,jjStatus.JobStatusId


GO


