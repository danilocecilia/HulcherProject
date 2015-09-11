USE [usa]
GO

/****** Object:  View [dbo].[OneSource_Contact]    Script Date: 04/01/2011 11:50:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OneSource_Contact]
AS
select
 ltrim(rtrim(cast(custId as varchar))) AS custId, Attn, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
 BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
 Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User, User2, 
 Checksum
 (
	CustId, Attn, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
	 BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
	 Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User, User2
 ) AS Checksum,
 1 AS CountryId
from
customer (NOLOCK)
where 
CASE WHEN len(ltrim(rtrim(CustId))) = 7 THEN 1 ELSE right(ltrim(rtrim(cast(custId as varchar))), 2) END 
<> 
CASE WHEN len(ltrim(rtrim(CustId))) = 7 THEN 0 ELSE '00' END  and
 custId not like ' %'

GO


