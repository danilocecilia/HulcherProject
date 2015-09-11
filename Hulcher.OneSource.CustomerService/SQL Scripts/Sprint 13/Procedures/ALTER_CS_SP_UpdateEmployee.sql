USE [OneSource]
GO

ALTER PROCEDURE [dbo].[CS_SP_UpdateEmployee]           
AS          
BEGIN   

	-- changed employees phones
	 UPDATE CS_Employee set          
		  [HomeAreaCode] = a.HomeAreaCode          
		  ,[HomePhone] = a.HomePhone          
		  ,[MobileAreaCode] = a.MobileAreaCode           
		  ,[MobilePhone] = a.MobilePhone          
		  ,[OtherPhoneAreaCode] = a.OtherPhoneAreaCode          
		  ,[OtherPhone] = a.OtherPhone     
		  ,[HasPhoneChanges] = 0      
	 FROM CS_Employee_Load a (NOLOCK)          
	  INNER JOIN CS_Employee b (NOLOCK) on a.PersonGUID = b.PersonGUID        
	  LEFT OUTER JOIN CS_Division c (NOLOCK) on a.DivisionCode = c.Name        
	  LEFT OUTER JOIN CS_Division d (NOLOCK) on a.LocationCode = d.Name        
	 WHERE          
	  a.Checksum <> b.Checksum  
	  AND 
	  (
		  (
			b.HasPhoneChanges = 0
			AND
			(
				ISNULL(b.HomeAreaCode,'') <> ISNULL(a.HomeAreaCode,'')          
				OR ISNULL(b.HomePhone,'') <> ISNULL(a.HomePhone,'')          
				OR ISNULL(b.MobileAreaCode,'') <> ISNULL(a.MobileAreaCode,'')           
				OR ISNULL(b.MobilePhone,'') <> ISNULL(a.MobilePhone,'')          
				OR ISNULL(b.OtherPhoneAreaCode,'') <> ISNULL(a.OtherPhoneAreaCode,'')          
				OR ISNULL(b.OtherPhone,'') <> ISNULL(a.OtherPhone,'')
			)
		  )
		  OR
		  (
			b.HasPhoneChanges = 1
			AND b.HomeAreaCode = a.HomeAreaCode          
			AND b.HomePhone = a.HomePhone          
			AND b.MobileAreaCode = a.MobileAreaCode           
			AND b.MobilePhone = a.MobilePhone          
			AND b.OtherPhoneAreaCode = a.OtherPhoneAreaCode          
			AND b.OtherPhone = a.OtherPhone
		  )
	  )
  
	-- changed employees address
	 UPDATE CS_Employee set          
		  [Address] = a.Address          
		  ,[Address2] = a.Address2          
		  ,[City] = a.City          
		  ,[StateProvinceCode] = a.StateProvinceCode          
		  ,[CountryCode] = a.CountryCode          
		  ,[PostalCode] = a.PostalCode      
		  ,[HasAddressChanges] = 0      
	 FROM CS_Employee_Load a (NOLOCK)          
	  INNER JOIN CS_Employee b (NOLOCK) on a.PersonGUID = b.PersonGUID        
	  LEFT OUTER JOIN CS_Division c (NOLOCK) on a.DivisionCode = c.Name        
	  LEFT OUTER JOIN CS_Division d (NOLOCK) on a.LocationCode = d.Name        
	 WHERE          
	  a.Checksum <> b.Checksum  
	  AND 
	  (
		  (
			b.HasAddressChanges = 0
			AND
			(
				ISNULL(b.Address,'') <> ISNULL(a.Address,'')          
				OR ISNULL(b.Address2, '') <> ISNULL(a.Address2,'')          
				OR ISNULL(b.City,'') <> ISNULL(a.City,'')          
				OR ISNULL(b.StateProvinceCode,'') <> ISNULL(a.StateProvinceCode,'')          
				OR ISNULL(b.CountryCode,'') <> ISNULL(a.CountryCode,'')          
				OR ISNULL(b.PostalCode,'') <> ISNULL(a.PostalCode,'')
			)
		  )
		  OR
		  (
			b.HasAddressChanges = 1
			AND b.Address = a.Address          
			AND b.Address2 = a.Address2          
			AND b.City = a.City          
			AND b.StateProvinceCode = a.StateProvinceCode          
			AND b.CountryCode = a.CountryCode          
			AND b.PostalCode = a.PostalCode 
		  )
	  )
  
  
	 INSERT INTO CS_JobDivision (JobID, DivisionID, PrimaryDivision, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active)
	  SELECT DISTINCT
		r.JobID AS [JobID],
		d.ID AS [DivisionID],
		0 AS [PrimaryDivision],
		'System' AS [CreatedBy],
		GETDATE() AS [CreationDate],
		'System' AS [ModifiedBy],
		GETDATE() AS [ModificationDate],
		1 AS Active
	  FROM 
		CS_Employee_Load a (NOLOCK)          
		INNER JOIN CS_Employee b (NOLOCK) on a.PersonGUID = b.PersonGUID
		INNER JOIN CS_Resource r (NOLOCK) on b.ID = r.EmployeeID AND r.Active = 1
		INNER JOIN CS_Division d (NOLOCK) on a.DivisionCode = d.Name
		LEFT OUTER JOIN CS_JobDivision jd (NOLOCK) on d.ID = jd.DivisionID AND jd.JobID = r.JobID        
	  WHERE 
		b.Checksum <> a.Checksum
		AND jd.DivisionID IS NULL
	
	-- changed employees          
	 UPDATE CS_Employee set          
		[PersonGUID] = a.PersonGUID          
		  ,[PersonID] = a.PersonID          
		  ,[Name] = a.LastName          
		  ,[FirstName] = a.FirstName        
		  ,[Nickname] = a.Nickname  
		  ,[DayAreaCode] = a.DayAreaCode          
		  ,[DayPhone] = a.DayPhone          
		  ,[FaxAreaCode] = a.FaxAreaCode          
		  ,[FaxPhone] = a.FaxPhone          
		  ,[CanadaAvailableFlag] = a.CanadaAvailableFlag          
		  ,[DivisionID] = c.ID        
		  ,[LocatedDivisionID] = d.ID        
		  ,[JobCode] = a.JobCode    
		  ,[BusinessCardTitle] = a.BusinessCardTitle
		  ,[HireDate] = a.PersonJobStartDate
		  ,[BirthDate] = a.BirthDate          
		  ,[DriversLicenseNumber] = a.DriversLicenseNumber          
		  ,[DriversLicenseClass] = a.DriversLicenseClass          
		  ,[DriversLicenseStateProvinceCode] = a.DriversLicenseStateProvinceCode          
		  ,[DriversLicenseExpireDate] = a.DriversLicenseExpireDate          
		  ,[PassportNumber] = a.PassportNumber          
		  ,[PassportCountryCode] = a.PassportCountryCode          
		  ,[PassportIssueDate] = a.PassportIssueDate          
		  ,[PassportExpireDate] = a.PassportExpireDate          
		  ,[UserLogin] = a.UserId
		  ,[Checksum] = a.Checksum          
		  ,[ModifiedBy] = 'Load'          
		  ,[ModificationDate] = GETDATE()          
		  ,[ACTIVE] = (CASE a.StatusCode WHEN 'A' THEN 1 ELSE 0 END)     
	 FROM CS_Employee_Load a (NOLOCK)          
	  INNER JOIN CS_Employee b (NOLOCK) on a.PersonGUID = b.PersonGUID        
	  LEFT OUTER JOIN CS_Division c (NOLOCK) on a.DivisionCode = c.Name        
	  LEFT OUTER JOIN CS_Division d (NOLOCK) on a.LocationCode = d.Name        
	 WHERE          
	  a.Checksum <> b.Checksum
          
-- new employees          
INSERT INTO [CS_Employee]          
           ([PersonGUID]          
           ,[PersonID]          
           ,[Name]          
           ,[FirstName] 
           ,[Nickname]         
           ,[DayAreaCode]          
           ,[DayPhone]          
           ,[FaxAreaCode]          
           ,[FaxPhone]          
           ,[HomeAreaCode]          
           ,[HomePhone]          
           ,[MobileAreaCode]          
           ,[MobilePhone]          
           ,[OtherPhoneAreaCode]          
           ,[OtherPhone]          
           ,[Address]          
           ,[Address2]          
           ,[City]          
           ,[StateProvinceCode]          
           ,[CountryCode]          
           ,[PostalCode]          
           ,[CanadaAvailableFlag]          
           ,[DivisionID]          
           ,[LocatedDivisionID]        
           ,[JobCode]      
           ,[BusinessCardTitle]
           ,[HireDate]
           ,[BirthDate]          
           ,[DriversLicenseNumber]          
           ,[DriversLicenseClass]          
           ,[DriversLicenseStateProvinceCode]          
           ,[DriversLicenseExpireDate]          
           ,[PassportNumber]          
           ,[PassportCountryCode]          
           ,[PassportIssueDate]          
           ,[PassportExpireDate]          
           ,[UserLogin]
           ,[Checksum]        
           ,[CreatedBy]        
           ,[CreationDate]        
           ,[ModifiedBy]          
           ,[ModificationDate]          
           ,[Active]
           ,[HasAddressChanges]          
           ,[HasPhoneChanges])          
     SELECT a.PersonGUID,           
   a.PersonID,           
   a.LastName,           
   a.FirstName,         
   a.Nickname,  
   a.DayAreaCode,           
   a.DayPhone,           
   a.FaxAreaCode,           
   a.FaxPhone,           
   a.HomeAreaCode,           
   a.HomePhone,           
   a.MobileAreaCode,           
   a.MobilePhone,          
   a.OtherPhoneAreaCode,          
   a.OtherPhone,           
   a.Address,           
   a.Address2,           
   a.City,           
   a.StateProvinceCode,           
   a.CountryCode,           
   a.PostalCode,           
   a.CanadaAvailableFlag,           
   b.ID,          
   c.ID,           
   a.JobCode,    
   a.BusinessCardTitle,
   a.PersonJobStartDate,           
   a.BirthDate,          
   a.DriversLicenseNumber,           
   a.DriversLicenseClass,           
   a.DriversLicenseStateProvinceCode,           
   a.DriversLicenseExpireDate,          
   a.PassportNumber,           
   a.PassportCountryCode,           
   a.PassportIssueDate,          
   a.PassportExpireDate,          
   a.UserId,
   a.Checksum,           
   'Load',           
   GETDATE(),          
   'Load',           
   GETDATE(),          
   CASE a.StatusCode WHEN 'A' THEN 1 ELSE 0 END,
   0,
   0
     FROM CS_Employee_Load a (NOLOCK)        
      left outer join CS_Division b (NOLOCK) on a.DivisionCode = b.Name        
      left outer join CS_Division c (NOLOCK) on a.LocationCode = c.Name        
     WHERE NOT EXISTS(SELECT 1 FROM CS_Employee b where b.PersonGUID = a.PersonGUID)  
     
END