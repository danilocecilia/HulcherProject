
  
alter VIEW CS_View_DPIReport      
      
as       
      
      
select  
		dpi.ID											as [DPIID]      
       ,job.ID											as [JobID]    
       ,dpi.IsContinuing								as [IsContinuing]    
       ,dpi.Date										as [Date]      
       ,division.Name									as [Division]     
      ,count(*)											as [EQ]  
      ,case         
  when job.Number = '' then job.Internal_Tracking        
  when job.Number IS NULL then job.Internal_Tracking        
  else job.Number        
   end													as [JobNumber]       
     ,customer.Name as [Customer]      
     ,st.Acronym + ', ' + st.Name + ', ' + cty.Name		as [Location]      
     ,jAction.Description								as [JobAction]      
     ,jDescription.NumberEngines						as [Engine]      
     ,jDescription.NumberEmpties						as [Empties]      
     ,jDescription.NumberLoads							as [Loads]      
  ,case jjStatus.JobStatusID      
   when 6 then 'DONE'      
   else      
    case dpi.CalculationStatus      
    when 1 then 'INSF'      
    else      
     'CONT'      
    end       
  end as [Status]  
  ,dpi.FirstETA											as [ETA]   
  ,dpi.FirstATA											as [ATA]      
  ,ptype.Acronym										as [PriceTypeAcronym]        
  ,jtype.Description									as [JobTypeDescription]      
  ,dpi.Total											as [RevenueTotal]      
        
from 
	cs_dpi dpi (NOLOCK)   
	 inner join CS_Job job (NOLOCK) on job.ID = dpi.JobID and job.Active = 1
		inner join CS_JobInfo jInfo (NOLOCK) on jInfo.JobID = job.ID  and job.Active = 1    
			inner join CS_JobAction jAction (NOLOCK) on jAction.ID = jInfo.JobActionID  
			inner join CS_PriceType ptype (NOLOCK) on ptype.ID = jInfo.PriceTypeID
			inner join CS_JobType jtype (NOLOCK) on jtype.ID = jInfo.JobTypeID
		inner join CS_JobDivision jobDiv (NOLOCK) on job.ID = jobDiv.JobID and jobDiv.PrimaryDivision = 1
			inner join CS_Division division (NOLOCK) on division.ID = jobDiv.DivisionID
		inner join CS_CustomerInfo cInfo (NOLOCK) on cInfo.JobId = job.ID
			inner join CS_Customer customer (NOLOCK) on customer.ID = cInfo.CustomerId         
		inner join CS_LocationInfo location (NOLOCK) on location.JobID = job.ID      
			inner join CS_State st (NOLOCK) on location.StateID = st.ID        
			inner join CS_City cty (NOLOCK) on location.CityID = cty.ID        
		inner join CS_JobDescription jDescription (NOLOCK) on jDescription.JobId = job.ID
		inner join CS_Job_JobStatus jjStatus (NOLOCK) on jjStatus.JobID = job.ID and jjStatus.Active = 1
		left outer join CS_Resource resource (NOLOCK) on resource.JobID = job.ID  and resource.Active = 1 and resource.EmployeeID is null
			left outer join CS_Equipment equipment (NOLOCK) on resource.EquipmentID = equipment.ID and equipment.HeavyEquipment = 1
			
group by   
   dpi.ID  
  ,job.ID    
  ,dpi.IsContinuing  
  ,dpi.Date  
  ,division.Name  
    ,case         
  when job.Number = '' then job.Internal_Tracking        
  when job.Number IS NULL then job.Internal_Tracking        
  else job.Number        
   end   
     ,customer.Name   
     ,st.Acronym + ', ' + st.Name + ', ' + cty.Name      
     ,jAction.Description      
     ,jDescription.NumberEngines     
     ,jDescription.NumberEmpties      
     ,jDescription.NumberLoads      
  ,case jjStatus.JobStatusID      
   when 6 then 'DONE'      
   else      
    case dpi.CalculationStatus      
    when 1 then 'INSF'      
    else      
     'CONT'      
    end       
  end                    
  ,dpi.FirstETA  
  ,dpi.FirstATA      
  ,ptype.Acronym               
  ,jtype.Description            
  ,dpi.Total