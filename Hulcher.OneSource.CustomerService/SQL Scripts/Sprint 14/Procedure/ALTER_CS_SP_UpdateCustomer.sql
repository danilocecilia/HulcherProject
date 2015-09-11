-- Author:  Fabio Justo  
-- Create date: 01/12/2011  
-- Last Modification: 04/26/2011 (Fabio)  
-- Description: Execute the updated for Customer Info                          
ALTER PROCEDURE [dbo].[CS_SP_UpdateCustomer]  
AS                          
BEGIN                          

BEGIN TRANSACTION Trans
              
-- SET NOCOUNT ON added to prevent extra result sets from      
-- interfering with SELECT statements.      
SET NOCOUNT ON;

DECLARE @Table table (ID int, CustomerNumber varchar(50))
DECLARE @ClosedID int
DECLARE @ClosedHoldID int

set @ClosedHoldID = 9
set @ClosedID = 6

INSERT INTO @Table
SELECT a.User2 AS ID, a.CustId
FROM CS_Customer_Load a (NOLOCK) 
 INNER JOIN CS_Customer b (NOLOCK) ON a.User2 = b.ID AND b.CustomerNumber IS NULL
WHERE a.User2 is not null

UPDATE CS_Customer
SET CustomerNumber = a.CustomerNumber
FROM @Table a INNER JOIN CS_Customer b (NOLOCK)        
 ON a.ID = b.ID
 AND b.CustomerNumber IS NULL
 
UPDATE CS_Job_JobStatus
SET Active = 0
WHERE 
	JobID IN (select c.JobId from @Table a INNER JOIN CS_CustomerInfo C (NOLOCK) ON a.ID = c.CustomerId AND c.Active = 1)
	AND JobStatusId = @ClosedHoldID
	AND Active = 1

INSERT INTO CS_Job_JobStatus
SELECT
	c.JobId,
	@ClosedID,
	null,
	GETDATE(),
	GETDATE(),
	GETDATE(),
	null,
	null,
	'System',
	'System',
	1
FROM
	@Table a
	INNER JOIN CS_CustomerInfo C (NOLOCK) ON a.ID = c.CustomerId AND c.Active = 1
                                  
update CS_Customer        
set                          
[CustomerNumber] = a.CustId                
,[Name] = a.Name                
,[Attn] = a.Attn      
,[Address1] = a.Addr1                
,[Address2] = a.Addr2                
,[State] = a.State                
,[City] = a.City                
,[Country] = a.Country                
,[Zip] = a.Zip                
,[Phone] = a.Phone                
,[Fax] = a.Fax                
,[Email] = a.EMailAddr                
,[BillName] = a.BillName                
,[BillAddress1] = a.BillAddr1                
,[BillAddress2] = a.BillAddr2                
,[BillAttn] = a.BillAttn                
,[BillState] = a.BillState                
,[BillCity] = a.BillCity                
,[BillCountry] = a.BillCountry                
,[BillPhone] = a.BillPhone                
,[BillFax] = a.BillFax                
,[BillSalutation] = a.BillSalut                
,[BillThruProject] = a.BillThruProject                
,[BillZip] = a.BillZip                
,[CreationDate] = a.Crtd_DateTime                
,[ModificationDate] = a.LUpd_DateTime                
,[CreatedBy] = a.Crtd_User                
,[Active] = CASE WHEN a.Status = 'A' THEN 1 ELSE 0 END                
,[ModifiedBy] = a.LUpd_User                
,[CheckSum] = a.Checksum          
,[CountryID] = a.CountryID        
from CS_Customer_Load a (NOLOCK) INNER JOIN CS_Customer b (NOLOCK)        
 ON a.CustId = b.CustomerNumber        
 AND a.CountryID = b.CountryID                           
where a.Checksum <> ISNULL(b.Checksum, 0)
 AND b.IsGeneralLog = 0 and a.User2 is null  
   
update CS_Customer        
set                          
[CustomerNumber] = a.CustId                
,[Name] = a.Name                
,[Attn] = a.Attn      
,[Address1] = a.Addr1                
,[Address2] = a.Addr2                
,[State] = a.State                
,[City] = a.City                
,[Country] = a.Country                
,[Zip] = a.Zip                
,[Phone] = a.Phone                
,[Fax] = a.Fax                
,[Email] = a.EMailAddr                
,[BillName] = a.BillName                
,[BillAddress1] = a.BillAddr1                
,[BillAddress2] = a.BillAddr2                
,[BillAttn] = a.BillAttn                
,[BillState] = a.BillState                
,[BillCity] = a.BillCity                
,[BillCountry] = a.BillCountry                
,[BillPhone] = a.BillPhone                
,[BillFax] = a.BillFax                
,[BillSalutation] = a.BillSalut                
,[BillThruProject] = a.BillThruProject                
,[BillZip] = a.BillZip                
,[CreationDate] = a.Crtd_DateTime                
,[ModificationDate] = a.LUpd_DateTime                
,[CreatedBy] = a.Crtd_User                
,[Active] = CASE WHEN a.Status = 'A' THEN 1 ELSE 0 END                
,[ModifiedBy] = a.LUpd_User                
,[CheckSum] = a.Checksum          
,[CountryID] = a.CountryID        
from CS_Customer_Load a (NOLOCK) INNER JOIN CS_Customer b (NOLOCK)        
 ON a.CustId = b.CustomerNumber        
 AND a.CountryID = b.CountryID                           
where a.Checksum <> ISNULL(b.Checksum, 0)
 AND b.IsGeneralLog = 0 and a.User2 is not null

INSERT INTO CS_Customer                  
([CustomerNumber]              
,[Name]        
,[Attn]              
,[Address1]                
,[Address2]                
,[State]                
,[City]                
,[Country]                
,[Zip]                
,[Phone]                
,[Fax]                
,[Email]                
,[BillName]                
,[BillAddress1]                
,[BillAddress2]                
,[BillAttn]                
,[BillState]                
,[BillCity]                
,[BillCountry]                
,[BillPhone]                
,[BillFax]                
,[BillSalutation]                
,[BillThruProject]                
,[BillZip]  
,[IsGeneralLog]  
,[CreationDate]                
,[ModificationDate]                
,[CreatedBy]                
,[Active]                 
,[ModifiedBy]                
,[CheckSum]          
,[CountryID])                          
SELECT                   
a.[CustId]                 
,a.[Name]    
,a.[Attn]                 
,a.[Addr1]                
,a.[Addr2]                
,a.[State]                
,a.[City]                
,a.[Country]                
,a.[Zip]                
,a.[Phone]                
,a.[Fax]                
,a.[EMailAddr]                
,a.[BillName]                
,a.[BillAddr1]                
,a.[BillAddr2]                
,a.[BillAttn]                
,a.[BillState]                
,a.[BillCity]                
,a.[BillCountry]                
,a.[BillPhone]                
,a.[BillFax]                
,a.[BillSalut]                
,a.[BillThruProject]                
,a.[BillZip]  
,0 AS IsGeneralLog             
,a.[Crtd_DateTime]                
,a.[LUpd_DateTime]                
,a.[Crtd_User]                
,CASE WHEN a.Status = 'A' THEN 1 ELSE 0 END  AS [Status]          
,a.[LUpd_User]                
,a.[Checksum]          
,a.CountryID           
FROM CS_Customer_Load a (NOLOCK) LEFT OUTER JOIN CS_Customer b (NOLOCK)        
 ON a.CustId = b.CustomerNumber                          
 AND a.CountryID = b.CountryID        
where                          
b.ID IS NULL

COMMIT TRANSACTION Trans                
        
END 