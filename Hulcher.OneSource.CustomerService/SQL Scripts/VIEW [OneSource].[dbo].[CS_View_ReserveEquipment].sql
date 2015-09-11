DROP VIEW CS_View_ReserveEquipment

GO

CREATE VIEW CS_View_ReserveEquipment
AS  
  
select  
 d.ID as [DivisionID],  
 d.Name as [Division],  
 st.ID as [StateID],  
 st.Acronym as [State],  
 et.ID as [EquipmentTypeID],  
 et.Name as [EquipmentType],  
 (select count(distinct li.JobID) from CS_Reserve r (nolock)   
   inner join CS_LocationInfo li (nolock) on r.JobID = li.JobID  
  where r.EquipmentTypeID = et.ID and r.Active = 1 and li.StateID = st.ID  
 ) as [Reserve],  
 (select count(*) from CS_Resource r2 (nolock)  
   inner join CS_Equipment e3 (nolock) on r2.EquipmentID = e3.ID  
  where e3.EquipmentTypeID = et.ID and r2.Active = 1 and e3.DivisionID = d.ID  
 ) as [Assigned],  
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
  inner join CS_EquipmentType et (nolock) on e.EquipmentTypeID = et.ID  
group by  
 d.ID,  
 d.Name,  
 st.ID,  
 st.Acronym,  
 et.ID,  
 et.Name  
   
  
  
  
