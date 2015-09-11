-- WARNING
-- THIS SCRIPT RUNS BASED ON LINKED SERVERS
-- NEEDS TO CONFIGURE A LINKED SERVER BETWEEN ONESOURCE AND THE LEGACY CS DATABASE


INSERT INTO [172.16.252.90].[OneSource].[dbo].CS_City (Name, StateID, CreatedBy,CreationDate,ModifiedBy,ModificationDate,Active,CSRecord)
SELECT distinct a.loc_city, s.ID, 'System',GETDATE(),'System',GETDATE(),1,1
from [APPSERV\CS].[Customer_Service].[dbo].[rte_location] a
	INNER JOIN [172.16.252.90].[OneSource].[dbo].CS_State s (NOLOCK) on a.loc_state = s.Acronym
where not exists 
	(select 1 from [172.16.252.90].[OneSource].[dbo].CS_City b (nolock) where b.Name = a.loc_city and b.StateID in 
		( select c.ID from [172.16.252.90].[OneSource].[dbo].CS_State c (nolock) where c.Acronym = a.loc_state ) )
	 AND a.deleteflag is null
	 and a.loc_state not in ('MX','PQ','W')

-- if needed, clear up the table
--delete from CS_Route

insert into [172.16.252.90].[OneSource].[dbo].CS_Route (DivisionID, CityID, Miles, Hours, Fuel, Route, CityPermitOffice, CountyPermitOffice, Active, CreatedBy, CreationDate, ModifiedBy, ModificationDate)
select
	d.ID as DivisionID,
	c.ID as CityID,
	a.loc_miles as Miles,
	a.loc_hours as Hours,
	a.loc_fuel as Fuel,
	a.loc_rout as Route,
	a.city_permit_office,
	a.county_permit_office,
	1,
	a.loc_create_id as CreatedBy,
	a.loc_create_date as CreationDate,
	a.loc_modify_id as ModifiedBy,
	a.loc_mod_date as ModificationDate
from
	[APPSERV\CS].[Customer_Service].[dbo].[rte_location] a (nolock)
		INNER JOIN [172.16.252.90].[OneSource].[dbo].CS_City c (nolock) on a.loc_city = c.Name and c.StateID IN ( 
			select b.ID from [172.16.252.90].[OneSource].[dbo].CS_State b (nolock) where b.Acronym = a.loc_state )
		INNER JOIN [172.16.252.90].[OneSource].[dbo].CS_Division d (nolock) on
			case when a.loc_div = '018' then '18' else a.loc_div end = d.Name
where
	a.deleteflag is null