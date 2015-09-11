ALTER VIEW [dbo].[CS_View_ReserveEquipment]  
AS  
  
select  
 d.ID as [DivisionID],
 d.Name AS [Division],
 st.ID as [StateID],  
 st.Acronym as [State],  
 et.ID as [EquipmentTypeID],  
 et.Name as [EquipmentType],  
 count(DISTINCT reserve.id) as Reserve,
 COUNT(DISTINCT resource.ID) as Assigned,
 (select count(*) from CS_Equipment e2 (nolock) where
  e2.Active = 1 and   
  e2.EquipmentTypeID = et.ID and  
  e2.DivisionID = d.ID and  
  not exists ( select 1 from CS_Resource r3 (nolock) where r3.EquipmentID = e2.ID and r3.Active = 1 )  
 ) as [Available]  
from  
 CS_Equipment e (nolock)  
  inner join CS_Division d (nolock) on e.DivisionID = d.ID  
   left outer join CS_State st (nolock) on d.StateID = st.ID
   LEFT OUTER JOIN CS_EquipmentCoverage cover (NOLOCK) ON e.ID = cover.EquipmentID AND cover.Active = 1
   LEFT OUTER JOIN CS_Division DCover (NOLOCK) on cover.DivisionID = DCover.ID
  inner join CS_EquipmentType et (nolock) on e.EquipmentTypeID = et.ID
  LEFT OUTER JOIN CS_Reserve reserve (NOLOCK) ON et.ID = reserve.EquipmentTypeID AND e.DivisionID = reserve.DivisionID AND reserve.Active = 1 
  LEFT OUTER JOIN CS_Resource resource (NOLOCK) on e.ID = resource.EquipmentID AND resource.Active = 1
group by  
 d.ID,  
 d.Name,  
 st.ID,  
 st.Acronym,  
 et.ID,  
 et.Name