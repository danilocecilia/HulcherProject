ALTER VIEW CS_View_JobCallLog    
AS      
                
SELECT      
 clog.ID             AS [CallId]      
 ,ctype.Description          AS [CallType]      
 ,ctype.ID            AS [CallTypeId]      
 ,CASE      
  WHEN clog.UserCall = 1 THEN clog.CreatedBy      
  WHEN employee.ID IS NULL AND clog.CalledInByExternal IS NULL THEN contact.LastName + ', ' + contact.Name  
  WHEN contact.ID IS NULL AND clog.CalledInByExternal IS NULL THEN employee.Name + ', ' + employee.FirstName      
  ELSE clog.CalledInByExternal  
  END             AS [CalledInBy]      
 ,clog.CallDate           AS [CallDate]      
 ,clog.ModifiedBy          AS [ModifiedBy] --mod.Name + ', ' + mod.FirstName AS [ModifiedBy]      
 ,clog.Note            AS [Details]      
 ,clog.JobID            AS [JobId]      
 ,CASE      
  WHEN job.Number = '' THEN job.Internal_Tracking      
  WHEN job.Number IS NULL THEN job.Internal_Tracking      
  ELSE job.Number      
  END             AS [JobNumber]     
,CASE      
WHEN job.Number = '' THEN ptype.Acronym + jType.Description + job.Internal_Tracking      
WHEN job.Number IS NULL THEN ptype.Acronym + jType.Description + job.Internal_Tracking      
ELSE ptype.Acronym + jType.Description + job.Number      
END             AS [PrefixedNumber]     
 ,customer.Name           AS [Customer]      
 ,jdivision.DivisionID         AS [DivisionId]          
 ,1              AS [ModifiedById] --clog.ModificationID    AS [ModifiedById]          
 ,clog.Active           AS [Active]          
 ,js.JobStatusID           AS [JobStatusId]          
 ,js.JobStartDate          AS [JobStartDate]          
 ,clog.ModificationDate         AS [ModificationDate]          
 ,clog.ShiftTransferLog         AS [ShiftTransferLog]              
 ,CAST(      
  CASE              
   WHEN job.ID = 1 THEN 1      
   ELSE 0              
  END AS BIT)           AS [IsGeneralLog]          
 ,ptype.Acronym           AS [PriceTypeAcronym]          
 ,jType.[Description]         AS [JobTypeDescription]      
 ,job.CreationDate          AS [JobCreationDate]
 ,convert(bit,ctype.IsAutomaticProcess) as [IsAutomaticProcess]
FROM      
 CS_CallLog (NOLOCK) clog      
  INNER JOIN CS_CallType ctype (NOLOCK) ON clog.CallTypeID = ctype.ID      
  LEFT OUTER JOIN CS_Employee employee (NOLOCK) ON clog.CalledInByEmployee = employee.ID      
  LEFT OUTER JOIN CS_Contact contact (NOLOCK) ON clog.CalledInByCustomer = contact.ID AND contact.Active = clog.Active      
  LEFT OUTER JOIN CS_Employee mod (NOLOCK) ON clog.ModificationID = mod.ID      
  INNER JOIN CS_Job job (NOLOCK) ON clog.JobID = job.ID      
   INNER JOIN CS_JobInfo jinfo (NOLOCK) ON job.ID = jinfo.JobID      
    INNER JOIN CS_PriceType ptype (NOLOCK) ON ptype.ID = jinfo.PriceTypeID      
    INNER JOIN CS_JobType jType (NOLOCK) ON jType.ID = jinfo.JobTypeID      
   INNER JOIN CS_Job_JobStatus js (NOLOCK) ON job.ID = js.JobID AND js.Active = 1      
    INNER JOIN CS_JobStatus jstatus (NOLOCK) ON js.JobStatusId = jstatus.ID      
   INNER JOIN CS_JobDivision jdivision (NOLOCK) ON job.ID = jdivision.JobID AND jdivision.PrimaryDivision = 1      
   INNER JOIN CS_CustomerInfo cinfo (NOLOCK) ON job.ID = cinfo.JobId      
    INNER JOIN CS_Customer customer (NOLOCK) ON cinfo.CustomerId = customer.ID      