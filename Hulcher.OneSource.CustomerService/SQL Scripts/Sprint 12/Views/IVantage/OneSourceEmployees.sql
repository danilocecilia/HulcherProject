ALTER VIEW [dbo].[OneSource_Employees]
AS
SELECT     a.PersonGUID, REPLICATE('0', 5 - LEN(a.PersonID)) + a.PersonID AS PersonID, a.LastName, a.FirstName, a.DayAreaCode, a.DayPhone, a.FaxAreaCode, a.FaxPhone, 
                      a.HomeAreaCode, a.HomePhone, a.MobileAreaCode, a.MobilePhone, a.OtherPhoneAreaCode, a.OtherPhone, a.Address, a.Address2, a.City, a.StateProvinceCode, 
                      a.CountryCode, a.PostalCode, a.CanadaAvailableFlag, b.StatusCode, c.DivisionCode, c.LocationCode, d.JobCode, d.BusinessCardTitle, d.PersonJobStartDate, e.BirthDate, 
                      e.DriversLicenseNumber, e.DriversLicenseClass, e.DriversLicenseStateProvinceCode, e.DriversLicenseExpireDate, e.PassportNumber, e.PassportCountryCode, 
                      e.PassportIssueDate, e.PassportExpireDate, f.UserId, CHECKSUM(a.PersonGUID, REPLICATE('0', 5 - LEN(a.PersonID)) + a.PersonID, a.LastName, a.FirstName, a.DayAreaCode,
                       a.DayPhone, a.FaxAreaCode, a.FaxPhone, a.HomeAreaCode, a.HomePhone, a.MobileAreaCode, a.MobilePhone, a.OtherPhoneAreaCode, a.OtherPhone, a.Address, 
                      a.Address2, a.City, a.StateProvinceCode, a.CountryCode, a.PostalCode, a.CanadaAvailableFlag, b.StatusCode, c.DivisionCode, c.LocationCode, d.JobCode, 
                      d.BusinessCardTitle,d.PersonJobStartDate, e.BirthDate, e.DriversLicenseNumber, e.DriversLicenseClass, e.DriversLicenseStateProvinceCode, e.DriversLicenseExpireDate, 
                      e.PassportNumber, e.PassportCountryCode, e.PassportIssueDate, e.PassportExpireDate, f.UserId) AS Checksum
FROM         dbo.tPerson AS a WITH (NOLOCK) LEFT OUTER JOIN
                      dbo.tPersonStatusHist AS b WITH (NOLOCK) ON a.PersonGUID = b.PersonGUID AND b.PersonStatusCurrentFlag = 1 LEFT OUTER JOIN
                      dbo.tPersonLocationHist AS c WITH (NOLOCK) ON a.PersonGUID = c.PersonGUID AND c.PersonLocationCurrentFlag = 1 LEFT OUTER JOIN
                      dbo.tPersonJobHist AS d WITH (NOLOCK) ON a.PersonGUID = d.PersonGUID AND d.PersonJobCurrentFlag = 1 LEFT OUTER JOIN
                      dbo.tPersonal AS e WITH (NOLOCK) ON a.PersonGUID = e.PersonGUID LEFT OUTER JOIN
                      dbo.USysWebUser AS f WITH (NOLOCK) ON a.PersonGUID = f.PersonGUID