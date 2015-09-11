CREATE PROCEDURE [dbo].[CS_SP_GetCallCriteriaValues]   
(@CallCriteriaID int)    
AS    
    
select     
 ccv.ID, 
 ccv.CallCriteriaTypeID, 
 coalesce(
   Ltrim(Rtrim(c.Name)) + 
   CASE WHEN ISNULL(c.Attn, '') = '' THEN '' ELSE ' - ' + Ltrim(Rtrim(c.Attn)) END + 
   CASE WHEN ISNULL(c.Country, '') = '' THEN '' ELSE ' - ' + Ltrim(Rtrim(c.Country)) END + 
   CASE WHEN ISNULL(c.CustomerNumber, '') = '' THEN '' ELSE ' - ' + Ltrim(Rtrim(c.CustomerNumber)) END, 
   d.Name, js.Description, pt.Description, jc.description, jt.description, ja.description, fr.description,    
   ct.Name, (st.Acronym + ' - ' + st.Name), cty.Name) as [Value]    
INTO ##CallCriteriaRealValues    
from    
 CS_CallCriteriaValue ccv (NOLOCK)    
 LEFT JOIN CS_Customer c (NOLOCK) on ccv.CallCriteriaTypeID = 1 AND ccv.Value = c.ID    
 LEFT JOIN CS_Division d (NOLOCK) on ccv.CallCriteriaTypeID = 2 AND ccv.Value = d.ID    
 LEFT JOIN CS_JobStatus js (NOLOCK) on ccv.CallCriteriaTypeID = 3 and ccv.Value = js.ID    
 LEFT JOIN CS_PriceType pt (NOLOCK) on ccv.CallCriteriaTypeID = 4 and ccv.Value = pt.ID    
 LEFT JOIN CS_JobCategory jc (NOLOCK) on ccv.CallCriteriaTypeID = 5 and ccv.Value = jc.ID    
 LEFT JOIN CS_JobType jt (NOLOCK) on ccv.CallCriteriaTypeID = 6 and ccv.Value = jt.ID    
 LEFT JOIN CS_JobAction ja (NOLOCK) on ccv.CallCriteriaTypeID = 7 and ccv.Value = ja.ID    
 LEFT JOIN CS_Frequency fr (NOLOCK) on ccv.CallCriteriaTypeID = 8 AND ccv.Value = fr.ID    
 LEFT JOIN CS_Country ct (NOLOCK) ON ccv.CallCriteriaTypeID = 10 AND ccv.Value = ct.ID    
 LEFT JOIN CS_State st (NOLOCK) on ccv.CallCriteriaTypeID = 11 AND ccv.Value = st.ID    
 LEFT JOIN CS_City cty (NOLOCK) on ccv.CallCriteriaTypeID = 12 AND ccv.Value = cty.ID    
where ccv.CallCriteriaTypeID not in (9,13,14,15,16,17,18,19,20) AND ccv.Active = 1    
UNION    
SELECT    
 ccv.ID, ccv.CallCriteriaTypeID, ccv.Value    
FROM    
 CS_CallCriteriaValue ccv (NOLOCK)     
where ccv.CallCriteriaTypeID in (9,13,14,15,16,17,18,20) AND ccv.Active = 1    
  
    
Select Distinct    
  ccv.CallCriteriaID ,cct.Description, STUFF(ISNULL((SELECT ', ' + y.value    
                      FROM CS_CallCriteriaValue x (NOLOCK)    
      INNER JOIN ##CallCriteriaRealValues y (NOLOCK) ON x.id = y.ID    
                     WHERE x.CallCriteriaTypeID = ccv.CallCriteriaTypeID    
      AND x.CallCriteriaID = ccv.CallCriteriaID    
      AND x.Active = ccv.Active    
                   FOR XML PATH ('')), ''), 1, 2, '') AS [Value]  
 from     
  CS_CallCriteriaValue ccv (NOLOCK)    
  INNER JOIN CS_CallCriteriaType cct (NOLOCK) ON ccv.CallCriteriaTypeID = cct.ID    
  INNER JOIN ##CallCriteriaRealValues t (NOLOCK) ON ccv.ID = t.ID    
where    
 ccv.CallCriteriaID = @CallCriteriaID
 AND ccv.Active = 1
     
DROP TABLE ##CallCriteriaRealValues    

