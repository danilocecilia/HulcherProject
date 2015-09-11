USE [usa]
GO

/****** Object:  View [dbo].[OneSource_Contract]    Script Date: 04/01/2011 11:50:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OneSource_Contract]
AS

select 
     contract, 
     contract_desc,
     Crtd_DateTime, 
     Crtd_User, 
     LUpd_DateTime, 
     LUpd_User,
     Date_Start_Org,
     date_end_org, 
     customer,
     text_contract1,
     text_contract2, 
     status1,
     checksum(contract,contract_desc,Crtd_DateTime,Crtd_User,LUpd_DateTime,LUpd_User,Date_Start_Org,date_end_org,customer,text_contract1,text_contract2,status1) AS CheckSum,
     1 as CountryId
     
 from	
     PJCont c (NOLOCK)
 where
     LTRIM(RTRIM(customer)) <> ''
     

GO


