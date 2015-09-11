ALTER VIEW [dbo].[CS_View_ConflictedEquipmentCombos]  
AS  

 SELECT   
  C.ComboID  
  , C.ComboName  
 FROM  
  (  
   SELECT   
    B.ID AS ComboID  
    , B.Name AS ComboName  
    , A.DivisionID  
   FROM   
    CS_Equipment A (NOLOCK)  
    INNER JOIN CS_EquipmentCombo B (NOLOCK)  
    ON A.ComboID = B.ID  
   WHERE  
    A.Active = 1  
    AND B.Active = 1
    AND A.Seasonal = 0  
   GROUP BY  
    B.ID  
    , B.Name  
    , A.DivisionID  
  ) C  
 GROUP BY  
  C.ComboID  
  , C.ComboName  
 HAVING  
  COUNT(C.ComboID) > 1