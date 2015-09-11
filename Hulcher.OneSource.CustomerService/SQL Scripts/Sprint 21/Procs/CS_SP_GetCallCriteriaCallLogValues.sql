CREATE PROCEDURE [dbo].[CS_SP_GetCallCriteriaCallLogValues]  
(@CallCriteriaID int)  
AS  

Select Distinct  
  ccv.CallCriteriaID ,pct.Type As Description, STUFF(ISNULL((SELECT ', ' + y.Description  
                      FROM CS_CallCriteriaValue x (NOLOCK)  
							INNER JOIN CS_CallType y (NOLOCK) ON x.Value = y.ID
								LEFT JOIN CS_PrimaryCallType_CallType z (NOLOCK) on y.ID = z.CallTypeID
								LEFT JOIN CS_PrimaryCallType h (nolock) on z.PrimaryCallTypeID = h.ID
                     WHERE x.CallCriteriaTypeID = ccv.CallCriteriaTypeID  
						AND x.CallCriteriaID = ccv.CallCriteriaID  
						AND x.Active = ccv.Active
						AND h.ID = pct.ID
                   FOR XML PATH ('')), ''), 1, 2, '') AS [Value]
 from   
  CS_CallCriteriaValue ccv (NOLOCK)  
	 LEFT JOIN CS_CallType cType (NOLOCK) on ccv.CallCriteriaTypeID = 19 AND ccv.Value = cType.ID
	 LEFT JOIN CS_PrimaryCallType_CallType pc (NOLOCK) on cType.ID = pc.CallTypeID
	 LEFT JOIN CS_PrimaryCallType pct (nolock) on pc.PrimaryCallTypeID = pct.ID
where
 ccv.CallCriteriaTypeID = 19
 AND ccv.CallCriteriaID = @CallCriteriaID
 AND pct.ID != 8  /* EXCEÇÃO NON-JOB UPDATE */
 AND ccv.Active = 1


  