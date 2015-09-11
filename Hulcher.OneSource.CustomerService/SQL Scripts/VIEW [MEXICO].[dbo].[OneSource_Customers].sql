USE [mexico]
GO

/****** Object:  View [dbo].[OneSource_Customers]    Script Date: 04/01/2011 11:53:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[OneSource_Customers]
AS
select
 ltrim(rtrim(cast(custId as varchar))) AS CustId, Attn, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
 BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
 Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User, User2,
 Checksum
 (
	CustId, Attn, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
	 BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
	 Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User, User2
 ) AS Checksum, 3 as CountryId
from
 Customer (nolock)
where
 len(ltrim(rtrim(cast(custId as varchar)))) = 6 and
 right(rtrim(cast(custId as varchar)), 2) = '00' and
 custId not like ' %'


GO


