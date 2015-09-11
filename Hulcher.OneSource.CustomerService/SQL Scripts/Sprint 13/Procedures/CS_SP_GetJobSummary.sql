ALTER PROCEDURE dbo.CS_SP_GetJobSummary  
(@jobStatusId int,@jobId int,@divisionId int,  
 @customerId int,@dateFilterType int,@beginDate DateTime,  
 @endDate DateTime,@personName varchar(255))  
AS  
  
SELECT DISTINCT           
 f.Name           AS [Division]            
 ,f.ID             AS [DivisionId]            
 ,NULL             AS [ResourceDivision]            
 ,NULL             AS [ResourceDivisionId]            
 ,g.ID             AS [JobID]            
 ,CASE      
 WHEN g.Number = '' THEN g.Internal_Tracking                
 WHEN g.Number IS NULL THEN g.Internal_Tracking                
 ELSE g.Number                
 END             AS [JobNumber]       
 ,CASE      
 WHEN g.Number = '' THEN ptype.Acronym + jtype.Description + g.Internal_Tracking                
 WHEN g.Number IS NULL THEN ptype.Acronym + jtype.Description + g.Internal_Tracking                
 ELSE ptype.Acronym + jtype.Description + g.Number                
 END             AS [PrefixedNumber]         
 ,CONVERT(BIT, 0)          AS [IsResource]            
 ,''              AS [EmployeeName]            
 ,''              AS [EquipmentName]            
 ,(h.Name + ', ' + h.FirstName)       AS [ProjectManager]            
 ,j.Description           AS [JobStatus]            
 ,j.ID             AS [JobStatusId]            
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name  AS [Location]            
 ,a.ModificationDate          AS [LastModification]            
 ,NULL             AS [ResouceLastModification]            
 ,cl.ID             AS [CallLogId]            
 ,ct.Description           AS [LastCallType]            
 ,cl.CallDate           AS [LastCallDate]            
 ,k.ID             AS [CustomerId]            
 ,k.Name             AS [Customer]            
 ,a.ModifiedBy           AS [ModifiedBy] --,jobmod.Name + ', ' + jobmod.FirstName     AS [ModifiedBy]            
 ,a.InitialCallDate          AS [CallDate]            
 ,NULL             AS [ResourCallDate]            
 ,jjs.JobStartDate          AS [StartDate]            
 ,m.Date             AS [PresetDate]            
 ,jjs.JobCloseDate          AS [ClosedDate]            
 ,''              AS [Duration]            
 ,CONVERT(bit,             
 CASE             
 WHEN COUNT(rs.ID) > 0 THEN 1             
 ELSE 0             
 END)            AS [HasResources]            
 ,ptype.Acronym           AS [PriceTypeAcronym]            
 ,jtype.[Description]         AS [JobTypeDescription]            
 ,NULL             AS [EquipmentStatus]            
 ,CAST(0 AS BIT)           AS [IsReserve]            
FROM            
 CS_Job g (NOLOCK)            
 LEFT OUTER JOIN cs_Resource rs (NOLOCK) ON g.ID = rs.JobId AND rs.Active = 1          
 INNER JOIN CS_JobInfo a (NOLOCK) ON a.JobID = g.ID            
 INNER JOIN CS_Job_JobStatus jjs ON jjs.JobID = a.JobID and jjs.Active = 1            
 LEFT OUTER JOIN CS_Employee jobmod (NOLOCK) ON jobmod.ID = a.ModificationID            
 INNER JOIN CS_JobStatus j (NOLOCK) ON jjs.JobStatusID = j.ID            
 INNER JOIN CS_JobDivision b (NOLOCK) ON a.JobID = b.JobID AND b.PrimaryDivision = 1            
 INNER JOIN CS_Division f (NOLOCK) ON f.ID = b.DivisionID AND f.IsGeneralLog = 0            
 INNER JOIN CS_CustomerInfo (NOLOCK) c on a.JobID = c.JobId            
 INNER JOIN CS_Customer k (NOLOCK) ON c.CustomerId = k.ID AND k.IsGeneralLog = 0            
 LEFT OUTER JOIN CS_PresetInfo m (NOLOCK) ON a.JobID = m.JobId            
 LEFT OUTER JOIN CS_Employee h (NOLOCK) ON a.ProjectManager = h.ID            
 INNER JOIN CS_LocationInfo e (NOLOCK) ON a.JobID = e.JobID            
 LEFT OUTER JOIN CS_State st (NOLOCK) ON e.StateID = st.ID            
 LEFT OUTER JOIN CS_City cty (NOLOCK) ON e.CityID = cty.ID            
 LEFT OUTER JOIN CS_CallLog cl (NOLOCK) ON a.JobID = cl.JobID and cl.ID = (SELECT CallLogID FROM CS_FN_LastCallType(a.JobID))            
 LEFT OUTER JOIN CS_CallType ct (NOLOCK) ON cl.CallTypeID = ct.ID            
 INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = a.PriceTypeID            
 INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = a.JobTypeID
 
 LEFT JOIN CS_Employee resEmp (NOLOCK) ON rs.EmployeeID = resEmp.ID
 LEFT JOIN CS_CallLog clog (NOLOCK) ON g.ID = clog.JobID AND clog.Active = 1
 LEFT JOIN CS_Employee EmpCalled (NOLOCK) ON clog.CalledInByEmployee = EmpCalled.ID
 LEFT JOIN CS_CallLogResource clRes (NOLOCK) ON clog.ID = clRes.CallLogID AND clRes.Active = 1
 LEFT JOIN CS_Employee clResEmp (NOLOCK) ON clRes.EmployeeID = clResEmp.ID
 LEFT JOIN CS_Contact clResContact (NOLOCK) ON clRes.ContactID = clResContact.ID  
    
WHERE  
 (jjs.JobStatusId = @jobStatusId OR @jobStatusId IS NULL)  
 AND (g.ID = @jobId OR @jobId IS NULL)  
 AND (b.DivisionID = @divisionId OR @divisionId IS NULL)  
 AND (c.CustomerId = @customerId OR @customerId IS NULL)  
 AND (CASE WHEN @dateFilterType = 1 THEN a.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN a.ModificationDate  
           ELSE null END >= @beginDate  
      AND  
      CASE WHEN @dateFilterType = 1 THEN a.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN a.ModificationDate  
           ELSE null END <= @endDate)
  AND (((resEmp.Name + ', ' + resEmp.FirstName like '%' + @PersonName + '%')
     OR (EmpCalled.Name + ', ' + EmpCalled.FirstName like '%' + @PersonName + '%')
     OR (clResEmp.Name + ', ' + clResEmp.FirstName like '%' + @PersonName + '%')
     OR (clResContact.LastName + ', ' + clResContact.Name like '%' + @PersonName + '%'))
   OR @PersonName = '')  
GROUP BY  
 f.Name            
 ,f.ID            
 ,g.ID            
 ,g.Number            
 ,g.Internal_Tracking            
 ,(h.Name + ', ' + h.FirstName)            
 ,j.Description            
 ,j.ID            
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name            
 ,a.ModificationDate            
 ,cl.ID            
 ,ct.Description            
 ,cl.CallDate            
 ,k.ID            
 ,k.Name            
 ,a.ModifiedBy --,jobmod.Name + ', ' + jobmod.FirstName            
 ,a.InitialCallDate            
 ,jjs.JobStartDate            
 ,m.Date            
 ,jjs.JobCloseDate            
 ,ptype.Acronym    
 ,jtype.[Description]            
            
UNION ALL            
                       
SELECT DISTINCT              
 f.Name            AS [Division]            
 ,f.ID             AS [DivisionId]            
 ,CASE    
 WHEN ISNULL(xcd.ID, icd.ID) IS NULL THEN div.Name    
 ELSE 'C ' + ISNULL(xcd.Name, icd.Name) + '/' + div.Name    
 END AS [DivisionName]        
 ,div.ID           AS [ResourceDivisionId]            
 ,a.ID             AS [JobID]            
 ,CASE      
 WHEN a.Number = '' THEN a.Internal_Tracking                
 WHEN a.Number IS NULL THEN a.Internal_Tracking                
 ELSE a.Number                
 END             AS [JobNumber]    
 ,CASE      
 WHEN a.Number = '' THEN ptype.Acronym + jtype.Description + a.Internal_Tracking                
 WHEN a.Number IS NULL THEN ptype.Acronym + jtype.Description + a.Internal_Tracking                
 ELSE ptype.Acronym + jtype.Description + a.Number                
 END             AS [PrefixedNumber]            
 ,CONVERT(bit, 1)          AS [IsResource]            
 ,(x.Name + ', ' + x.FirstName)       AS [EmployeeName]            
 ,i.Description           AS [EquipmentName]            
 ,NULL             AS [ProjectManager]            
 ,j.Description           AS [JobStatus]            
 ,j.ID             AS [JobStatusId]            
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name   AS [Location]            
 ,g.ModificationDate          AS [LastModification]            
 ,d.ModificationDate          AS [ResouceLastModification]            
 ,cl.ID             AS [CallLogId]            
 ,ct.Description           AS [LastCallType]            
 ,cl.CallDate           AS [LastCallDate]            
 ,k.ID             AS [CustomerId]            
 ,k.Name             AS [Customer]            
 ,d.ModifiedBy           AS [ModifiedBy] --,resmod.Name + ', ' + resmod.FirstName     AS [ModifiedBy]            
 ,g.InitialCallDate          AS [CallDate]            
 ,d.CreationDate           AS [ResourceCallDate]            
 ,jjs.JobStartDate          AS [StartDate]            
 ,m.Date             AS [PresetDate]            
 ,jjs.JobCloseDate          AS [ClosedDate]            
 ,CONVERT(VARCHAR, d.Duration)       AS [Duration]            
 ,0              AS [HasResources]            
 ,ptype.Acronym           AS [PriceTypeAcronym]            
 ,jtype.[Description]         AS [JobTypeDescription]            
 ,i.Status            AS [EquipmentStatus]            
 ,CAST(0 AS BIT)           AS [IsReserve]            
FROM            
 CS_Job a (NOLOCK)            
 INNER JOIN CS_JobDivision b (NOLOCK) ON a.ID = b.JobID AND b.PrimaryDivision = 1            
 INNER JOIN CS_Division f (NOLOCK) ON f.ID = b.DivisionID AND f.IsGeneralLog = 0            
 INNER JOIN CS_LocationInfo li (NOLOCK) ON li.JobID = a.ID          
 LEFT OUTER JOIN CS_State st (NOLOCK) ON li.StateID = st.ID            
 LEFT OUTER JOIN CS_City cty (NOLOCK) ON li.CityID = cty.ID            
 INNER JOIN CS_JobInfo g (NOLOCK) ON g.JobID = a.ID            
 INNER JOIN CS_Job_JobStatus jjs ON jjs.JobID = g.JobID and jjs.Active = 1            
 INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = g.PriceTypeID            
 INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = g.JobTypeID            
 INNER JOIN CS_JobStatus j (NOLOCK) ON jjs.JobStatusID = j.ID            
 INNER JOIN CS_CustomerInfo (NOLOCK) c on g.JobID = c.JobId            
 INNER JOIN CS_Customer k (NOLOCK) ON c.CustomerId = k.ID AND k.IsGeneralLog = 0            
 LEFT OUTER JOIN CS_PresetInfo m (NOLOCK) ON g.JobID = m.JobId            
 LEFT OUTER JOIN CS_Resource d (NOLOCK) ON a.ID = d.JobID AND d.Active = 1          
 LEFT OUTER JOIN CS_Employee resmod (NOLOCK) ON resmod.ID = d.ModificationID            
 LEFT OUTER JOIN CS_Employee x (NOLOCK) ON d.EmployeeID = x.ID            
 LEFT OUTER JOIN CS_Equipment i (NOLOCK) ON d.EquipmentID = i.ID            
 LEFT OUTER JOIN CS_Division div (NOLOCK) ON div.ID = ISNULL(x.DivisionID, i.DivisionID)    
 LEFT OUTER JOIN CS_EmployeeCoverage xc (NOLOCK) ON x.ID = xc.EmployeeID    
 LEFT OUTER JOIN CS_Division xcd (NOLOCK) ON xc.DivisionID = xcd.ID    
 LEFT OUTER JOIN CS_EquipmentCoverage ic (NOLOCK) ON i.ID = ic.EquipmentID    
 LEFT OUTER JOIN CS_Division icd (NOLOCK) ON ic.DivisionID = icd.ID        
 LEFT OUTER JOIN CS_CallLog cl (NOLOCK) ON cl.JobID = a.ID AND cl.ID in (SELECT CallLogID FROM CS_FN_LastCallTypeByResource(x.ID, i.ID))            
 LEFT OUTER JOIN CS_CallType ct (NOLOCK) ON ct.ID = cl.CallTypeID
 
 LEFT JOIN CS_Employee resEmp (NOLOCK) ON d.EmployeeID = resEmp.ID
 LEFT JOIN CS_CallLog clog (NOLOCK) ON a.ID = clog.JobID AND clog.Active = 1
 LEFT JOIN CS_Employee EmpCalled (NOLOCK) ON clog.CalledInByEmployee = EmpCalled.ID
 LEFT JOIN CS_CallLogResource clRes (NOLOCK) ON clog.ID = clRes.CallLogID AND clRes.Active = 1
 LEFT JOIN CS_Employee clResEmp (NOLOCK) ON clRes.EmployeeID = clResEmp.ID
 LEFT JOIN CS_Contact clResContact (NOLOCK) ON clRes.ContactID = clResContact.ID  
             
WHERE            
 (x.ID IS NOT NULL OR i.ID IS NOT NULL)  
 AND (jjs.JobStatusId = @jobStatusId OR @jobStatusId IS NULL)  
 AND (a.ID = @jobId OR @jobId IS NULL)  
 AND (b.DivisionID = @divisionId OR @divisionId IS NULL)  
 AND (c.CustomerId = @customerId OR @customerId IS NULL)  
 AND (CASE WHEN @dateFilterType = 1 THEN g.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN g.ModificationDate  
           ELSE null END >= @beginDate  
      AND  
      CASE WHEN @dateFilterType = 1 THEN g.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN g.ModificationDate  
           ELSE null END <= @endDate)
AND (((resEmp.Name + ', ' + resEmp.FirstName like '%' + @PersonName + '%')
     OR (EmpCalled.Name + ', ' + EmpCalled.FirstName like '%' + @PersonName + '%')
     OR (clResEmp.Name + ', ' + clResEmp.FirstName like '%' + @PersonName + '%')
     OR (clResContact.LastName + ', ' + clResContact.Name like '%' + @PersonName + '%'))
   OR @PersonName = '')    
GROUP BY
 f.Name
 ,f.ID
 ,CASE WHEN ISNULL(xcd.ID, icd.ID) IS NULL THEN div.Name ELSE 'C ' + ISNULL(xcd.Name, icd.Name) + '/' + div.Name END
 ,div.ID
 ,a.ID
 ,CASE WHEN a.Number = '' THEN a.Internal_Tracking WHEN a.Number IS NULL THEN a.Internal_Tracking ELSE a.Number END
 ,CASE WHEN a.Number = '' THEN ptype.Acronym + jtype.Description + a.Internal_Tracking WHEN a.Number IS NULL THEN ptype.Acronym + jtype.Description + a.Internal_Tracking ELSE ptype.Acronym + jtype.Description + a.Number END
 ,(x.Name + ', ' + x.FirstName)
 ,i.Description
 ,j.Description
 ,j.ID
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name
 ,g.ModificationDate
 ,d.ModificationDate
 ,cl.ID
 ,ct.Description
 ,cl.CallDate
 ,k.ID
 ,k.Name
 ,d.ModifiedBy
 ,g.InitialCallDate
 ,d.CreationDate
 ,jjs.JobStartDate
 ,m.Date
 ,jjs.JobCloseDate
 ,CONVERT(VARCHAR, d.Duration)
 ,ptype.Acronym
 ,jtype.[Description]
 ,i.Status
    
UNION ALL              
     
SELECT DISTINCT              
 f.Name             AS [Division]            
 ,f.ID            AS [DivisionId]            
 ,CASE    
 WHEN ISNULL(xcd.ID, icd.ID) IS NULL THEN div.Name    
 ELSE 'C ' + ISNULL(xcd.Name, icd.Name) + '/' + div.Name    
 END AS [DivisionName]        
 ,div.ID          AS [ResourceDivisionId]            
 ,a.ID            AS [JobID]            
 ,CASE      
 WHEN a.Number = '' THEN a.Internal_Tracking                
 WHEN a.Number IS NULL THEN a.Internal_Tracking                
 ELSE a.Number                
 END             AS [JobNumber]    
 ,CASE      
 WHEN a.Number = '' THEN ptype.Acronym + jtype.Description + a.Internal_Tracking                
 WHEN a.Number IS NULL THEN ptype.Acronym + jtype.Description + a.Internal_Tracking                
 ELSE ptype.Acronym + jtype.Description + a.Number                
 END             AS [PrefixedNumber]            
 ,CONVERT(bit, 1)          AS [IsResource]            
 ,(x.Name + ', ' + x.FirstName)       AS [EmployeeName]            
 ,i.Name             AS [EquipmentType]            
 ,NULL             AS [ProjectManager]            
 ,j.Description           AS [JobStatus]            
 ,j.ID             AS [JobStatusId]            
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name   AS [Location]            
 ,g.ModificationDate          AS [LastModification]            
 ,d.ModificationDate          AS [ResouceLastModification]            
 ,cl.ID             AS [CallLogId]            
 ,ct.Description           AS [LastCallType]            
 ,cl.CallDate           AS [LastCallDate]            
 ,k.ID             AS [CustomerId]            
 ,k.Name             AS [Customer]            
 ,d.ModifiedBy           AS [ModifiedBy] --,resmod.Name + ', ' + resmod.FirstName     AS [ModifiedBy]            
 ,g.InitialCallDate          AS [CallDate]            
 ,d.CreationDate           AS [ResourceCallDate]            
 ,jjs.JobStartDate          AS [StartDate]            
 ,m.Date             AS [PresetDate]            
 ,jjs.JobCloseDate          AS [ClosedDate]            
 ,CONVERT(VARCHAR, d.Duration)       AS [Duration]            
 ,0              AS [HasResources]            
 ,ptype.Acronym           AS [PriceTypeAcronym]            
 ,jtype.[Description]         AS [JobTypeDescription]            
 ,NULL             AS [EquipmentStatus]            
 ,CAST(1 AS BIT)           AS [IsReserve]            
FROM            
 CS_Job a (NOLOCK)            
 INNER JOIN CS_JobDivision b (NOLOCK) ON a.ID = b.JobID AND b.PrimaryDivision = 1            
 INNER JOIN CS_Division f (NOLOCK) ON f.ID = b.DivisionID AND f.IsGeneralLog = 0            
 INNER JOIN CS_LocationInfo li (NOLOCK) ON li.JobID = a.ID          
 LEFT OUTER JOIN CS_State st (NOLOCK) ON li.StateID = st.ID            
 LEFT OUTER JOIN CS_City cty (NOLOCK) ON li.CityID = cty.ID            
 INNER JOIN CS_JobInfo g (NOLOCK) ON g.JobID = a.ID            
 INNER JOIN CS_Job_JobStatus jjs ON jjs.JobID = g.JobID and jjs.Active = 1            
 INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = g.PriceTypeID            
 INNER JOIN CS_JobType jtype (NOLOCK) ON jtype.ID = g.JobTypeID            
 INNER JOIN CS_JobStatus j (NOLOCK) ON jjs.JobStatusID = j.ID            
 INNER JOIN CS_CustomerInfo (NOLOCK) c on g.JobID = c.JobId            
 INNER JOIN CS_Customer k (NOLOCK) ON c.CustomerId = k.ID AND k.IsGeneralLog = 0            
 LEFT OUTER JOIN CS_PresetInfo m (NOLOCK) ON g.JobID = m.JobId            
 LEFT OUTER JOIN CS_Reserve d (NOLOCK) ON a.ID = d.JobID AND d.Active = 1          
 LEFT OUTER JOIN CS_Employee resmod (NOLOCK) ON resmod.ID = d.ModificationID            
 LEFT OUTER JOIN CS_Employee x (NOLOCK) ON d.EmployeeID = x.ID            
 LEFT OUTER JOIN CS_EquipmentType i (NOLOCK) ON d.EquipmentTypeID = i.ID            
 LEFT OUTER JOIN CS_Division div (NOLOCK) ON div.ID = ISNULL(x.DivisionID, D.DivisionID)        
 LEFT OUTER JOIN CS_EmployeeCoverage xc (NOLOCK) ON x.ID = xc.EmployeeID    
 LEFT OUTER JOIN CS_Division xcd (NOLOCK) ON xc.DivisionID = xcd.ID    
 LEFT OUTER JOIN CS_EquipmentCoverage ic (NOLOCK) ON i.ID = ic.EquipmentID    
 LEFT OUTER JOIN CS_Division icd (NOLOCK) ON ic.DivisionID = icd.ID           
 LEFT OUTER JOIN CS_CallLog cl (NOLOCK) ON cl.JobID = a.ID AND cl.ID in (SELECT CallLogID FROM CS_FN_LastCallTypeByResource(x.ID, i.ID))            
 LEFT OUTER JOIN CS_CallType ct (NOLOCK) ON ct.ID = cl.CallTypeID            
 
 LEFT JOIN CS_Employee resEmp (NOLOCK) ON d.EmployeeID = resEmp.ID
 LEFT JOIN CS_CallLog clog (NOLOCK) ON a.ID = clog.JobID AND clog.Active = 1
 LEFT JOIN CS_Employee EmpCalled (NOLOCK) ON clog.CalledInByEmployee = EmpCalled.ID
 LEFT JOIN CS_CallLogResource clRes (NOLOCK) ON clog.ID = clRes.CallLogID AND clRes.Active = 1
 LEFT JOIN CS_Employee clResEmp (NOLOCK) ON clRes.EmployeeID = clResEmp.ID
 LEFT JOIN CS_Contact clResContact (NOLOCK) ON clRes.ContactID = clResContact.ID
 
WHERE            
 (x.ID IS NOT NULL OR i.ID IS NOT NULL)  
 AND (jjs.JobStatusId = @jobStatusId OR @jobStatusId IS NULL)  
 AND (a.ID = @jobId OR @jobId IS NULL)  
 AND (b.DivisionID = @divisionId OR @divisionId IS NULL)  
 AND (c.CustomerId = @customerId OR @customerId IS NULL)  
 AND (CASE WHEN @dateFilterType = 1 THEN g.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN g.ModificationDate  
           ELSE null END >= @beginDate  
      AND  
      CASE WHEN @dateFilterType = 1 THEN g.InitialCallDate  
           WHEN @dateFilterType = 2 THEN m.Date  
           WHEN @dateFilterType = 3 THEN jjs.JobStartDate  
           WHEN @dateFilterType = 4 THEN g.ModificationDate  
           ELSE null END <= @endDate)
AND (((resEmp.Name + ', ' + resEmp.FirstName like '%' + @PersonName + '%')
     OR (EmpCalled.Name + ', ' + EmpCalled.FirstName like '%' + @PersonName + '%')
     OR (clResEmp.Name + ', ' + clResEmp.FirstName like '%' + @PersonName + '%')
     OR (clResContact.LastName + ', ' + clResContact.Name like '%' + @PersonName + '%'))
   OR @PersonName = '')
GROUP BY
 f.Name
 ,f.ID
 ,CASE WHEN ISNULL(xcd.ID, icd.ID) IS NULL THEN div.Name ELSE 'C ' + ISNULL(xcd.Name, icd.Name) + '/' + div.Name END
 ,div.ID
 ,a.ID
 ,CASE WHEN a.Number = '' THEN a.Internal_Tracking WHEN a.Number IS NULL THEN a.Internal_Tracking ELSE a.Number END
 ,CASE WHEN a.Number = '' THEN ptype.Acronym + jtype.Description + a.Internal_Tracking WHEN a.Number IS NULL THEN ptype.Acronym + jtype.Description + a.Internal_Tracking ELSE ptype.Acronym + jtype.Description + a.Number END
 ,(x.Name + ', ' + x.FirstName)
 ,i.Name
 ,j.Description
 ,j.ID
 ,st.Acronym + ', ' + st.Name + ', ' + cty.Name
 ,g.ModificationDate
 ,d.ModificationDate
 ,cl.ID
 ,ct.Description
 ,cl.CallDate
 ,k.ID
 ,k.Name
 ,d.ModifiedBy
 ,g.InitialCallDate
 ,d.CreationDate
 ,jjs.JobStartDate
 ,m.Date
 ,jjs.JobCloseDate
 ,CONVERT(VARCHAR, d.Duration)
 ,ptype.Acronym
 ,jtype.[Description]
