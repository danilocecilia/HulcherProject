DROP VIEW CS_View_DPIInformation

GO

CREATE VIEW CS_View_DPIInformation  
AS          
        
SELECT DISTINCT          
 dpi.ID            AS [DPIID]           
 ,job.ID           AS [JobID]          
 ,CASE             
   WHEN job.Number = '' THEN job.Internal_Tracking            
   WHEN job.Number IS NULL THEN job.Internal_Tracking            
   ELSE job.Number            
 END             AS [JobNumber]
 ,CASE             
  WHEN job.Number = '' THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  WHEN job.Number IS NULL THEN ptype.Acronym + jtype.Description + job.Internal_Tracking            
  ELSE ptype.Acronym + jtype.Description + job.Number            
 END             AS [PrefixedNumber]
 , ptype.Acronym           AS [PriceTypeAcronym]          
 , jtype.[Description]         AS [JobTypeDescription]          
 , primd.Name          AS [JobDivision]          
 , cust.Name           AS [JobCustomerName]          
 , st.Acronym + ', ' + st.Name + ', ' + cty.Name  AS [JobLocation]          
 , jact.Description         AS [JobAction]          
 , jsta.JobStatusId         AS [JobStatusID]          
 , jsta.JobStartDate         AS [JobStartDate]          
 , jdes.NumberEngines        AS [NumberEngines]          
 , jdes.NumberEmpties        AS [NumberEmpties]          
 , jdes.NumberLoads         AS [NumberLoads]          
 , dpi.ProcessStatus         AS [DPIProcessStatus]          
 , dpi.ProcessStatusDate        AS [DPIProcessStatusDate]    
 , dpi.ApprovedBy               AS [DPIApprovedById]    
 , appEmployee.Name + ', ' + appEmployee.FirstName AS [DPIApprovedByName]    
 , dpi.CalculationStatus        AS [DPICalculationStatus]          
 , dpi.IsContinuing         AS [DPIIsContinuing]          
 , dpi.Total           AS [DPITotal]        
 , dpi.Date           AS [DPIDate]          
 , equi.Name           AS [EquipmentUnitNumber]          
 , combo.Name          AS [EquipmentComboNumber]          
 , CAST(CASE          
  WHEN combo.PrimaryEquipmentID = equi.ID          
  THEN 1          
  ELSE 0          
 END AS BIT)           AS [EquipmentIsPrimaryEquipment]          
 , equi.ID           AS [EquipmentID]          
 , equi.Description         AS [EquipmentName]          
 , CASE --EQUIPMENT COVERAGE           
  WHEN ecovdiv.Name IS NULL THEN equidiv.Name             
  ELSE 'C ' +  ecovdiv.Name + '/' + equidiv.Name            
 END             AS [EquipmentDivision]          
 , empl.ID           AS [EmployeeID]          
 , (empl.Name + ', ' + empl.FirstName)    AS [EmployeeName]          
 ,CASE --EMPLOYEE COVERAGE
	WHEN empcovdiv.ID IS NULL THEN empldiv.Name
	ELSE 'C' + empcovdiv.Name + '/' + empldiv.Name
  END AS [EmployeeDivision]          
 , empl.BusinessCardTitle       AS [EmployeePosition]          
 , dres.Total          AS [DPIResourceTotal]          
FROM          
 CS_DPI dpi (NOLOCK)          
 INNER JOIN  CS_Job job (NOLOCK) ON dpi.JobID = job.ID  
  INNER JOIN CS_JobInfo jinf (NOLOCK) ON job.ID = jinf.JobID      
   INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = jinf.PriceTypeID          
   INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = jinf.JobTypeID          
   INNER JOIN CS_JobAction jact (NOLOCK) ON jinf.JobActionID = jact.ID          
  INNER JOIN CS_Job_JobStatus jsta (NOLOCK) ON job.ID = jsta.JobID          
  LEFT OUTER JOIN CS_JobDescription jdes (NOLOCK) ON job.ID = jdes.JobId  
  INNER JOIN CS_CustomerInfo cinfo (NOLOCK) ON job.ID = cinfo.JobId            
   INNER JOIN CS_Customer cust (NOLOCK) ON cinfo.CustomerId = cust.ID AND cust.IsGeneralLog = 0           
  INNER JOIN CS_LocationInfo loc (NOLOCK) ON job.ID = loc.JobID            
   LEFT OUTER JOIN CS_State st (NOLOCK) ON loc.StateID = st.ID  
   LEFT OUTER JOIN CS_City cty (NOLOCK) ON loc.CityID = cty.ID          
  LEFT JOIN            
   (          
    SELECT jpdiv.JobID, pdiv.Name FROM          
    CS_Division pdiv (NOLOCK)          
   INNER JOIN CS_JobDivision jpdiv (NOLOCK) ON jpdiv.DivisionID = pdiv.ID AND jpdiv.PrimaryDivision = 1          
   ) primd ON job.ID = primd.JobID        
 LEFT JOIN  CS_DPIResource dres (NOLOCK) ON dpi.ID = dres.DPIID           
  LEFT JOIN CS_Equipment equi (NOLOCK) ON dres.EquipmentID = equi.ID  
   LEFT JOIN  CS_Division equidiv (NOLOCK) ON equi.DivisionID = equidiv.ID          
   LEFT JOIN  CS_EquipmentCombo combo (NOLOCK) ON equi.ComboID = combo.ID          
   LEFT OUTER JOIN CS_EquipmentCoverage ecov (NOLOCK) ON equi.ID = ecov.EquipmentID AND ecov.Active = 1            
    LEFT OUTER JOIN CS_Division ecovdiv (NOLOCK) ON ecov.DivisionID = ecovdiv.ID    
  LEFT JOIN CS_Employee empl (NOLOCK) ON dres.EmployeeID = empl.ID  
   LEFT JOIN  CS_Division empldiv (NOLOCK) ON empl.DivisionID = empldiv.ID
   LEFT JOIN CS_EmployeeCoverage empcov (NOLOCK) ON empl.ID = empcov.EmployeeID AND empcov.Active = 1
   LEFT JOIN CS_Division empcovdiv (NOLOCK) ON empcov.DivisionID = empcovdiv.ID    
 LEFT OUTER JOIN cs_Employee appEmployee (NOLOCK) ON dpi.ApprovedBy = appEmployee.ID    
WHERE              
 ISNULL(dres.Active, 1) = 1          
 AND job.Active = 1          
 AND jsta.Active = 1          
 AND ISNULL(jdes.Active, 1) = 1