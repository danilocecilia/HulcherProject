USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_GetJobData]    Script Date: 11/07/2011 13:20:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CS_View_GetJobData] 
AS

SELECT
	  job.ID AS [JobID]
	, job.Number AS [JobNumber]
	, job.Internal_Tracking AS [JobInternalTracking]
	, job.EmergencyResponse AS [JobEmergencyResponse]
	, cust.ID AS [CustomerID]
	, cust.Name AS [CustomerName]
	, cust.Country AS [CustomerCountry]
	, cust.Attn AS [CustomerAttn]
	, cust.CustomerNumber AS [CustomerNumber]
	, primarycontact.ID AS [PrimaryContactID]
	, primarycontact.LastName AS [PrimaryContactLastName]
	, primarycontact.Name AS [PrimaryContactFirstName]
	, primarycontact.DynamicsContact AS [PrimaryContactDynamicsContact]
	, primarycontact.Attn AS [PrimaryContactAttn]
	, primarycontact.State AS [PrimaryContactState]
	, primarycontact.Phone AS [PrimaryContactPhone]
	, pocemployee.ID AS [POCEmployeeID]
	, pocemployee.Name AS [POCEmployeeLastName]
	, pocemployee.FirstName AS [POCEmployeeFirstName]
	, pocemployee.Nickname AS [POCEmployeeNickname]
	, cinfodiv.ID as [DivisionID]
	, cinfodiv.Name as [DivisionName]
	, onsitecontact.ID AS [OnSiteContactID]
	, onsitecontact.LastName AS [OnSiteContactLastName]
	, onsitecontact.Name AS [OnSiteContactFirstName]
	, onsitecontact.DynamicsContact AS [OnSiteContactDynamicsContact]
	, onsitecontact.Attn AS [OnSiteContactAttn]
	, onsitecontact.State AS [OnSiteContactState]
	, onsitecontact.Phone AS [OnSiteContactPhone]
	, secondarycontact.ID AS [SecondaryContactID]
	, secondarycontact.LastName AS [SecondaryContactLastName]
	, secondarycontact.Name AS [SecondaryContactFirstName]
	, secondarycontact.DynamicsContact AS [SecondaryContactDynamicsContact]
	, secondarycontact.Attn AS [SecondaryContactAttn]
	, secondarycontact.State AS [SecondaryContactState]
	, secondarycontact.Phone AS [SecondaryContactPhone]
	, billtocontact.ID AS [BillToContactID]
	, billtocontact.LastName AS [BillToContactLastName]
	, billtocontact.Name AS [BillToContactFirstName]
	, billtocontact.DynamicsContact AS [BillToContactDynamicsContact]
	, billtocontact.Attn AS [BillToContactAttn]
	, billtocontact.State AS [BillToContactState]
	, billtocontact.Phone AS [BillToContactPhone]
	, jinfo.InitialCallDate AS [InitialCallDate]
	, jinfo.InitialCallTime AS [InitialCallTime]
	, jstatus.JobStatusId AS [JobStatusID]
	, jinfo.PriceTypeID AS [PriceTypeID]
	, ptype.Acronym AS [PriceTypeAcronym]
	, jstatus.JobStartDate AS [JobStartDate]
	, jinfo.JobActionID AS [JobActionID]
	, jinfo.JobCategoryID AS [JobCategoryID]
	, jinfo.JobTypeID AS [JobTypeID]
	, jtype.Description AS [JobTypeDescription]
	, jstatus.JobCloseDate AS [JobCloseDate]
	, jinfo.ProjectManager AS [ProjectManager]
	, jinfo.InterimBill AS [InterimBill]
	, jinfo.EmployeeID AS [RequestedByEmployeeID]
	, requestedby.Name AS [RequestedByEmployeeLastName]
	, requestedby.FirstName AS [RequestedByEmployeeFirstName]
	, requestedby.Nickname AS [RequestedByEmployeeNickname]
	, jinfo.FrequencyID AS [FrequencyID]
	, spricing.Type AS [SpecialPricingType]
	, spricing.OverallJobDiscount AS [OverallJobDiscount]
	, spricing.LumpsumValue AS [LumpsumValue]
	, spricing.LumpsumDuration AS [LumpsumDuration]
	, spricing.Notes AS [SpecialPricingNotes]
	, spricing.ApprovingRVPEmployeeID AS [ApprovingRVPEmployeeID]
	, CASE 
		WHEN jinfo.CustomerSpecificInfo IS NULL THEN cust.Xml
		ELSE jinfo.CustomerSpecificInfo
	  END AS [CustomerSpecificInfo]
	, ljinfo.Instructions AS [LostJobNotes]
	, ljinfo.ReasonID AS [LostJobReasonID]
	, ljinfo.CompetitorID AS [LostJobCompetitorID]
	, ljinfo.EmployeeID AS [POCFollowUpEmployeeID]
	, pocfollowup.Name AS [POCFollowUpEmployeeLastName]
	, pocfollowup.FirstName AS [POCFollowUpEmployeeFirstName]
	, pocfollowup.Nickname AS [POCFollowUpEmployeeNickname]
	, ljinfo.HSIRepEmployeeID AS [HSIRepEmployeeID]  
	, hsirep.Name AS [HSIRepEmployeeLastName]
	, hsirep.FirstName AS [HSIRepEmployeeFirstName]
	, hsirep.Nickname AS [HSIRepEmployeeNickname]
	, linfo.CountryID AS [CountryID]
	, linfo.StateID AS [StateID]
	, state.Acronym AS [StateAcronym]
	, state.Name AS [StateName]
	, linfo.CityID AS [CityID]
	, city.Name AS [CityName]
	, linfo.ZipCodeId AS [ZipCodeID]
	, zipcode.Name AS [ZipCodeName]
	, linfo.SiteName AS [SiteName]
	, linfo.AlternateName AS [AlternateName]
	, linfo.Directions AS [Directions]
	, jdesc.NumberEngines AS [NumberEngines]
	, jdesc.NumberLoads AS [NumberLoads]
	, jdesc.NumberEmpties AS [NumberEmpties]
	, jdesc.Lading AS [Lading]
	, jdesc.UnNumber AS [UnNumber]
	, jdesc.STCCInfo AS [STCCInfo]
	, jdesc.Hazmat AS [Hazmat]
	,(select top 1 JobStatusId from CS_Job_JobStatus jstatus where jstatus.JobID = job.ID and jstatus.Active=1 order by jstatus.ModificationDate desc) AS [LastJobStatusId]
FROM
	CS_Job job (NOLOCK)
		INNER JOIN CS_CustomerInfo cinfo (NOLOCK) on cinfo.JobId = job.ID
			INNER JOIN CS_Customer cust (NOLOCK) on cinfo.CustomerId = cust.ID
			LEFT OUTER JOIN CS_Contact primarycontact (NOLOCK) on cinfo.InitialCustomerContactId = primarycontact.ID
			LEFT OUTER JOIN CS_Contact onsitecontact (NOLOCK) on cinfo.EicContactId = onsitecontact.ID
			LEFT OUTER JOIN CS_Contact secondarycontact (NOLOCK) on cinfo.AdditionalContactId = secondarycontact.ID
			LEFT OUTER JOIN CS_Contact billtocontact (NOLOCK) on cinfo.BillToContactId = billtocontact.ID
			LEFT OUTER JOIN CS_Employee pocemployee (NOLOCK) on cinfo.PocEmployeeId = pocemployee.ID
			LEFT OUTER JOIN CS_Division cinfodiv (NOLOCK) on cinfo.DivisionId = cinfodiv.ID
			INNER JOIN CS_JobInfo jinfo (NOLOCK) on jinfo.JobID = job.ID
				INNER JOIN CS_Job_JobStatus jstatus (NOLOCK) on jstatus.JobID = jinfo.JobID and jstatus.Active = 1
				LEFT OUTER JOIN CS_Employee requestedby (NOLOCK) on jinfo.EmployeeID = requestedby.ID
			INNER JOIN CS_PriceType ptype (NOLOCK) on jinfo.PriceTypeID = ptype.ID
			INNER JOIN CS_JobType jtype (NOLOCK) on jinfo.JobTypeID = jtype.ID
			LEFT OUTER JOIN CS_SpecialPricing spricing (NOLOCK) on spricing.JobId = job.ID
			LEFT OUTER JOIN CS_LostJobInfo ljinfo (NOLOCK) on ljinfo.JobId = job.ID
				LEFT OUTER JOIN CS_Employee pocfollowup (NOLOCK) on ljinfo.EmployeeID = pocfollowup.ID
				LEFT OUTER JOIN CS_Employee hsirep (NOLOCK) on ljinfo.HSIRepEmployeeID = hsirep.ID
			LEFT OUTER JOIN CS_LocationInfo linfo (NOLOCK) on linfo.JobID = job.ID
				LEFT OUTER JOIN CS_State state (NOLOCK) on linfo.StateID = state.ID
				LEFT OUTER JOIN CS_City city (NOLOCK) on linfo.CityID = city.ID
				LEFT OUTER JOIN CS_ZipCode zipcode (NOLOCK) on linfo.ZipCodeId = zipcode.ID
			LEFT OUTER JOIN CS_JobDescription jdesc (NOLOCK) on jdesc.JobId = job.ID

GO


